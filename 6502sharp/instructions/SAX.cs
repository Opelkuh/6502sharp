using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SAX : InstructionBase
    {
        public SAX(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x87, 3), ZeroPage]
        [CPUInstruction(0x97, 4), ZeroPageY]
        [CPUInstruction(0x8F, 4), Absolute]
        [CPUInstruction(0x83, 6), IndirectX]
        public void SAX_Memory(int address)
        {
            int res = cpu.X.Value & cpu.A.Value;

            cpu.Memory.Set(address, (byte)res);
            flags.SetNegativeAndZero(res);
        }
    }
}