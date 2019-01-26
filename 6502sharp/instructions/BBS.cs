using _6502sharp.Helpers;

namespace _6502sharp.Instructions
{
    [DefaultInstruction]
    public class BBS : InstructionBase
    {
        public BBS(ICpu cpu) : base(cpu)
        {
        }

        [CPUInstruction(0x8F, 5, CPUType.CMOS)]
        public void BBS0([ZeroPage] int address, [Relative] int target) { process(0, address, target); }
        [CPUInstruction(0x9F, 5, CPUType.CMOS)]
        public void BBS1([ZeroPage] int address, [Relative] int target) { process(1, address, target); }
        [CPUInstruction(0xAF, 5, CPUType.CMOS)]
        public void BBS2([ZeroPage] int address, [Relative] int target) { process(2, address, target); }
        [CPUInstruction(0xBF, 5, CPUType.CMOS)]
        public void BBS3([ZeroPage] int address, [Relative] int target) { process(3, address, target); }
        [CPUInstruction(0xCF, 5, CPUType.CMOS)]
        public void BBS4([ZeroPage] int address, [Relative] int target) { process(4, address, target); }
        [CPUInstruction(0xDF, 5, CPUType.CMOS)]
        public void BBS5([ZeroPage] int address, [Relative] int target) { process(5, address, target); }
        [CPUInstruction(0xEF, 5, CPUType.CMOS)]
        public void BBS6([ZeroPage] int address, [Relative] int target) { process(6, address, target); }
        [CPUInstruction(0xFF, 5, CPUType.CMOS)]
        public void BBS7([ZeroPage] int address, [Relative] int target) { process(7, address, target); }


        private void process(int bit, int address, int branchAddress)
        {
            byte value = cpu.Memory.Get(address);
            int mask = 1 << bit;

            if ((value & mask) > 0) cpu.PC.Value = (ushort)branchAddress;
        }
    }
}