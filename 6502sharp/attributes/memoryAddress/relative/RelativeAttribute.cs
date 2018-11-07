namespace _6502sharp
{
    public class RelativeAttribute : MemoryAddressAttributeBase
    {
        public override int RequiredBytes => 1;

        public override int Resolve(ICpu cpu, ref byte[] rawAddress)
        {
            sbyte offset = (sbyte)rawAddress[0];

            return cpu.PC.Value + offset;
        }
    }
}
