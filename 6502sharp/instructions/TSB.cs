namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TSB : InstructionBase
    {
        public TSB(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x04, 5, CPUType.CMOS), ZeroPage]
        [CPUInstruction(0x0C, 6, CPUType.CMOS), Absolute]
        public void TSB_Memory(int address)
        {
            byte value = cpu.Memory.Get(address);

            int res = value | cpu.A.Value;

            cpu.Memory.Set(address, (byte)res);
            flags.SetZero(value & cpu.A.Value);
        }
    }
}