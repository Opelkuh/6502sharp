using System;
using System.IO;
using System.Linq;

namespace NES
{
    static class RomParser
    {
        // "NES" followed by MS-DOS end-of-file
        private static readonly byte[] INES_FINGERPRINT = { 0x4E, 0x45, 0x53, 0x1A };
        private const int INES_HEADER_LENGTH = 16;
        private const int TRAINER_SIZE = 512;

        public static RomData? ParseFile(string path)
        {
            RomData ret;
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                byte[] header = reader.ReadBytes(INES_HEADER_LENGTH);
                if (
                    header.Length != INES_HEADER_LENGTH ||
                    !header.Take(INES_FINGERPRINT.Length).SequenceEqual(INES_FINGERPRINT)
                ) return null;

                // check if rom is in NES 2.0 format
                if (((header[7] >> 2) & 3) == 2)
                {
                    ret = parseNes2(header);
                }
                else
                {
                    ret = parseNes1(header);
                }

                if (ret.HasTrainer)
                    ret.Trainer = reader.ReadBytes(TRAINER_SIZE);
                ret.PRGRom = reader.ReadBytes(ret.PRGRomSize);
                ret.CHRRom = reader.ReadBytes(ret.CHRRomSize);
            }

            // size check
            if (ret.HasTrainer && ret.Trainer.Length != TRAINER_SIZE) return null;
            if (ret.PRGRom.Length != ret.PRGRomSize) return null;
            if (ret.CHRRom.Length != ret.CHRRomSize) return null;

            return ret;
        }

        private static RomData parseNes1(byte[] header)
        {
            RomData data = new RomData();
            data.Type = RomType.INES;

            data.PRGRomSize = header[4] * 16384;
            data.CHRRomSize = header[5] * 8192;

            // ------------
            // FLAGS 6
            // ------------
            parseFlags6(ref data, header[6]);

            // ------------
            // FLAGS 7
            // ------------
            byte flags7 = header[7];

            // mapper num hi
            data.MapperNum |= flags7 & 0xF0;

            // check machine type
            if (getFlag(flags7, 0)) data.Machine = MachineType.VsSystem;
            else if (getFlag(flags7, 1)) data.Machine = MachineType.PlayChoice;
            else data.Machine = MachineType.NES;

            // ------------
            // FLAGS 9
            // ------------
            data.TVSystem = getFlag(header[9], 0) ? TVSystem.PAL : TVSystem.NTSC;

            return data;
        }

        private static RomData parseNes2(byte[] header)
        {
            RomData data = new RomData();
            data.Type = RomType.INES2;

            // ------------
            // FLAGS 6
            // ------------
            parseFlags6(ref data, header[6]);

            // ------------
            // FLAGS 7
            // ------------

            // mapper num hi
            data.MapperNum |= header[7] & 0xF0;

            // check machine type
            int type = header[7] & 3;
            switch (type)
            {
                case 0: data.Machine = MachineType.NES; break;
                case 1: data.Machine = MachineType.VsSystem; break;
                case 2: data.Machine = MachineType.PlayChoice; break;
                case 3: data.Machine = MachineType.FamicloneWithDecimal; break;
                default: data.Machine = MachineType.Other; break;
            }

            // ------------
            // FLAGS 8
            // ------------
            data.MapperNum |= (header[8] & 0xF) << 8;
            data.SubmapperNum = header[8] >> 4;

            // ------------
            // FLAGS 9
            // ------------
            int prgLsb = header[4];
            int chrLsb = header[5];
            int prgMsb = header[9] & 0x0F;
            int chrMsb = header[9] >> 4;

            // PRG-ROM
            if (prgMsb <= 0xE)
                data.PRGRomSize = (prgLsb | (prgMsb << 8)) * 16384;
            else
                data.PRGRomSize = exponentMultiplier(prgLsb);
            // CHR-ROM
            if (chrMsb <= 0xE)
                data.CHRRomSize = (chrLsb | (chrMsb << 8)) * 8192;
            else
                data.CHRRomSize = exponentMultiplier(chrLsb);

            // ------------
            // FLAGS 10
            // ------------
            int prgShift = header[10] & 0x0F;
            int prgNVShift = header[10] >> 4;

            if (prgShift > 0) data.PRGRamSize = 64 << prgShift;
            if (prgNVShift > 0) data.PRGNVRamSize = 64 << prgNVShift;

            // ------------
            // FLAGS 11
            // ------------
            int chrShift = header[11] & 0x0F;
            int chrNVShift = header[11] >> 4;

            if (chrShift > 0) data.CHRRamSize = 64 << chrShift;
            if (chrNVShift > 0) data.CHRNVRamSize = 64 << chrNVShift;

            // ------------
            // FLAGS 12
            // ------------
            data.TVSystem = (TVSystem)(header[12] & 3);

            // ------------
            // FLAGS 15
            // ------------
            int controller = header[15] & 0x3F;
            if (Enum.IsDefined(typeof(ControllerType), controller))
                data.DefaultController = (ControllerType)controller;
            else
                data.DefaultController = ControllerType.Other;


            return data;
        }

        private static int exponentMultiplier(int lsb)
        {
            int multiplier = (lsb & 3) * 2 + 1;
            int exponent = lsb >> 2;

            return (int)Math.Pow(2, exponent) * multiplier;
        }

        private static void parseFlags6(ref RomData data, byte flags)
        {
            // mirroring
            if (getFlag(flags, 3)) data.PPUMirroring = Mirroring.FourScreen;
            else
            {
                data.PPUMirroring = getFlag(flags, 0) ? Mirroring.Vertical : Mirroring.Horizontal;
            }

            // has battery
            data.HasBattery = getFlag(flags, 1);

            // has trainer
            data.HasTrainer = getFlag(flags, 2);

            // mapper num lo
            data.MapperNum = flags >> 4;
        }

        private static bool getFlag(byte val, byte bit)
        {
            return (val & (1 << bit)) > 0;
        }
    }
}
