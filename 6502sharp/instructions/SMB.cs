using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class SMB : InstructionBase
    {
        public SMB(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x87, 5, CPUType.CMOS)]
        public void SMB0([ZeroPage] int address) { process(0, address); }
        [CPUInstruction(0x97, 5, CPUType.CMOS)]
        public void SMB1([ZeroPage] int address) { process(1, address); }
        [CPUInstruction(0xA7, 5, CPUType.CMOS)]
        public void SMB2([ZeroPage] int address) { process(2, address); }
        [CPUInstruction(0xB7, 5, CPUType.CMOS)]
        public void SMB3([ZeroPage] int address) { process(3, address); }
        [CPUInstruction(0xC7, 5, CPUType.CMOS)]
        public void SMB4([ZeroPage] int address) { process(4, address); }
        [CPUInstruction(0xD7, 5, CPUType.CMOS)]
        public void SMB5([ZeroPage] int address) { process(5, address); }
        [CPUInstruction(0xE7, 5, CPUType.CMOS)]
        public void SMB6([ZeroPage] int address) { process(6, address); }
        [CPUInstruction(0xF7, 5, CPUType.CMOS)]
        public void SMB7([ZeroPage] int address) { process(7, address); }


        private void process(int bit, int address)
        {
            byte value = cpu.Memory.Get(address);
            int mask = 1 << bit;

            cpu.Memory.Set(address, (byte)(value | mask));
        }
    }
}