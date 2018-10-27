namespace _6502sharp
{
    public class IndirectAddressAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 2;

        public override int Resolve(ICpu cpu, ref byte[] raw)
        {
            int firstLoc = LEHelper.From(raw);

            // 6502 bug (https://everything2.com/node/868510)
            raw[0] = (byte)((raw[0] + 1) % 256);

            int secondLoc = LEHelper.From(raw);

            byte[] target = { cpu.Memory.Get(firstLoc), cpu.Memory.Get(secondLoc) };

            return LEHelper.From(target);
        }
    }
}
