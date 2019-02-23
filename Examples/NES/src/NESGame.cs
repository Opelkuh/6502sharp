using System.Threading;
using NES.Rendering;
using OpenTK.Graphics;

namespace NES
{
    static class NESGame
    {
        public static void Create(RomData rom)
        {
            NESMachine mach = new NESMachine();
            IMapper mapper = MapperFactory.GetMapper(mach, rom);

            mach.MapperMemory.Mapper = mapper;
            mach.PPU.Memory.Mirroring = rom.PPUMirroring;

            var thread = new Thread(() =>
            {
                GameWindow window = new GameWindow(mach);
                window.Run(60);
            });
            thread.Start();

            mach.CPU.Reset();
            while (true)
            {
                mach.CPU.NextTick();
                for (int i = 0; i < 3; i++) mach.PPU.NextTick();
            }
        }
    }
}