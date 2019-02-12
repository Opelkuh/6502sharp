using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class RMB : InstructionBase
    {
        public RMB(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x07, 5, CPUType.CMOS)]
        public void RMB0([ZeroPage] int address) { process(0, address); }
        [CPUInstruction(0x17, 5, CPUType.CMOS)]
        public void RMB1([ZeroPage] int address) { process(1, address); }
        [CPUInstruction(0x27, 5, CPUType.CMOS)]
        public void RMB2([ZeroPage] int address) { process(2, address); }
        [CPUInstruction(0x37, 5, CPUType.CMOS)]
        public void RMB3([ZeroPage] int address) { process(3, address); }
        [CPUInstruction(0x47, 5, CPUType.CMOS)]
        public void RMB4([ZeroPage] int address) { process(4, address); }
        [CPUInstruction(0x57, 5, CPUType.CMOS)]
        public void RMB5([ZeroPage] int address) { process(5, address); }
        [CPUInstruction(0x67, 5, CPUType.CMOS)]
        public void RMB6([ZeroPage] int address) { process(6, address); }
        [CPUInstruction(0x77, 5, CPUType.CMOS)]
        public void RMB7([ZeroPage] int address) { process(7, address); }


        private void process(int bit, int address)
        {
            byte value = cpu.Memory.Get(address);
            int mask = 1 << bit;

            cpu.Memory.Set(address, (byte)(value & ~mask));
        }
    }
}