namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        private IMachine _machine;
        private CPUType _type;

        protected delegate void InstructionDelegate();

        private Instruction[] _instructions = new Instruction[byte.MaxValue];

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
