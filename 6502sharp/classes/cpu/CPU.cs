namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        public CPUType Type => _type;

        private IMachine _machine;
        private CPUType _type;
        private Instruction[] _instructions = new Instruction[byte.MaxValue];

        protected delegate void InstructionDelegate();

        public CPU(IMachine machine, CPUType type)
        {
            _machine = machine;
            _type = type;

            FindInjectables();
        }

        private void RegisterInstruction(Instruction instruction)
        {
            _instructions[instruction.OpCode] = instruction;
        }
    }
}
