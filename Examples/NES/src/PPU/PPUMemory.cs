using _6502sharp;

namespace NES.PPU
{
    class PPUMemory : Memory
    {
        public Mirroring Mirroring = Mirroring.Horizontal;

        #region Nametables
        private static readonly Range Nametable0 = new Range(0x2000, 0x23FF);
        private static readonly Range Nametable1 = new Range(0x2400, 0x27FF);
        private static readonly Range Nametable2 = new Range(0x2800, 0x2BFF);
        private static readonly Range Nametable3 = new Range(0x2C00, 0x2FFF);
        #endregion

        #region Mirrors
        private static readonly Mirror[][] MIRRORS = {
            // Horizontal
            new Mirror[] {
                new Mirror(Nametable0, Nametable1),
                new Mirror(Nametable2, Nametable3),
            },
            // Vertical
            new Mirror[] {
                new Mirror(Nametable0, Nametable2),
                new Mirror(Nametable1, Nametable3),
            },
            // Single-Screen
            new Mirror[] {
                new Mirror(Nametable0, new Range(Nametable1.Start, Nametable3.End))
            },
            new Mirror[0],
        };

        #endregion

        public PPUMemory() : base(16384)
        {
        }

        public override byte Get(int address)
        {
            mirrorAddr(ref address);

            return base.Get(address);
        }

        public override void Set(int address, byte value)
        {
            mirrorAddr(ref address);

            base.Set(address, value);
        }

        private void mirrorAddr(ref int address)
        {
            Mirror[] current = MIRRORS[(int)Mirroring];

            foreach (var mir in current)
            {
                mir.Transform(ref address);
            }
        }
    }
}
