using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class RLA : InstructionBase
    {
        private ROL rol;

        public RLA(ICpu cpu) : base(cpu)
        {
            this.rol = new ROL(cpu);
        }

        [CPUInstruction(0x27, 5), ZeroPage]
        [CPUInstruction(0x37, 6), ZeroPageX]
        [CPUInstruction(0x2F, 6), Absolute]
        [CPUInstruction(0x3F, 7), AbsoluteX]
        [CPUInstruction(0x3B, 7), AbsoluteY]
        [CPUInstruction(0x23, 8), IndirectX]
        [CPUInstruction(0x33, 8), IndirectY]
        public void RLA_Memory(int address)
        {
            rol.ROL_Memory(address);

            cpu.A.Value &= cpu.Memory.Get(address);

            flags.SetNegativeAndZero(cpu.A.Value);
        }
    }
}