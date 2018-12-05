namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class STX : InstructionBase
    {
        public STX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x86, 3), ZeroPage]
        [CPUInstruction(0x96, 4), ZeroPageY]
        [CPUInstruction(0x8E, 4), Absolute]
        public void STX_Memory(int address)
        {
            cpu.Memory.Set(address, cpu.X.Value);
        }
    }
}