using NES.Mappers;

namespace NES
{
    static class MapperFactory
    {
        public static IMapper GetMapper(NESMachine mach, RomData rom)
        {
            switch (rom.MapperNum)
            {
                case 0:
                    return new NROM(mach, rom);
                default:
                    return null;
            }
        }
    }
}