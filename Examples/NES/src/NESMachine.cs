using _6502sharp;
using System;
using NES.PPU;

namespace NES
{
    class NESMachine : IMachine
    {
        public ICpu CPU => cpu;
        public GoPPU PPU => ppu;
        public IReadable Memory => ram;
        public MapperMemory MapperMemory => ram;

        private CPU cpu;
        private GoPPU ppu;
        private MapperMemory ram;

        public NESMachine()
        {
            // ppu = new PictureProcessingUnit(this);
            ppu = new GoPPU(this);
            ram = new MapperMemory(ppu);
            cpu = new CPU(this, CPUType.NMOS);

            // permanently disable decimal mode
            cpu.DecimalMode = false;
        }
    }
}
