using _6502sharp;
using System;

namespace NES
{
    class NESMachine : IMachine
    {
        public ICpu CPU => cpu;
        public IReadable Memory => ram;

        private CPU cpu;
        private PPU ppu;
        private MapperMemory ram;
        private PPUMemory vram;

        public NESMachine()
        {
            ram = new MapperMemory();
            vram = new PPUMemory();
            
            cpu = new CPU(this, CPUType.NMOS);
            ppu = new PPU(this);

            // permanently disable decimal mode
            cpu.DecimalMode = false;
        }
    }
}
