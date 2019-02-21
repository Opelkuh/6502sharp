using _6502sharp;
using System;
using NES.PPU;

namespace NES
{
    class NESMachine : IMachine
    {
        public ICpu CPU => cpu;
        public PictureProcessingUnit PPU => ppu;
        public IReadable Memory => ram;

        private CPU cpu;
        private PictureProcessingUnit ppu;
        private MapperMemory ram;

        public NESMachine()
        {
            ram = new MapperMemory();

            cpu = new CPU(this, CPUType.NMOS);
            ppu = new PictureProcessingUnit(this);

            // permanently disable decimal mode
            cpu.DecimalMode = false;
        }
    }
}
