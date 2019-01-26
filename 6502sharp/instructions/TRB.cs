namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class TRB : InstructionBase
    {
        public TRB(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x14, 3, CPUType.CMOS), ZeroPage]
        [CPUInstruction(0x1C, 4, CPUType.CMOS), Absolute]
        public void TRB_Memory(int address)
        {
            byte value = cpu.Memory.Get(address);

            int res = value & (cpu.A.Value ^ 0xFF);

            cpu.Memory.Set(address, (byte)res);
            if (res == value) cpu.SR.Zero = true;
        }
    }
}