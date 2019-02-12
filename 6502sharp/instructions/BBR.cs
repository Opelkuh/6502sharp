using _6502sharp.Reflection;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BBR : InstructionBase
    {
        public BBR(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x0F, 5, CPUType.CMOS)]
        public void BBR0([ZeroPage] int address, [Relative] int target) { process(0, address, target); }
        [CPUInstruction(0x1F, 5, CPUType.CMOS)]
        public void BBR1([ZeroPage] int address, [Relative] int target) { process(1, address, target); }
        [CPUInstruction(0x2F, 5, CPUType.CMOS)]
        public void BBR2([ZeroPage] int address, [Relative] int target) { process(2, address, target); }
        [CPUInstruction(0x3F, 5, CPUType.CMOS)]
        public void BBR3([ZeroPage] int address, [Relative] int target) { process(3, address, target); }
        [CPUInstruction(0x4F, 5, CPUType.CMOS)]
        public void BBR4([ZeroPage] int address, [Relative] int target) { process(4, address, target); }
        [CPUInstruction(0x5F, 5, CPUType.CMOS)]
        public void BBR5([ZeroPage] int address, [Relative] int target) { process(5, address, target); }
        [CPUInstruction(0x6F, 5, CPUType.CMOS)]
        public void BBR6([ZeroPage] int address, [Relative] int target) { process(6, address, target); }
        [CPUInstruction(0x7F, 5, CPUType.CMOS)]
        public void BBR7([ZeroPage] int address, [Relative] int target) { process(7, address, target); }


        private void process(int bit, int address, int branchAddress)
        {
            byte value = cpu.Memory.Get(address);
            int mask = 1 << bit;

            if ((value & mask) == 0) cpu.PC.Value = (ushort)branchAddress;
        }
    }
}