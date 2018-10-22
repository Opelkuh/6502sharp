namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        private IMachine _machine;
        protected delegate void InstructionDelegate();

        private Instruction[] _instructions = new Instruction[byte.MaxValue];

        public CPU(IMachine machine)
        {
            _machine = machine;

            FindInjectables();
        }

        private void RegisterInstruction(Instruction instruction)
        {
            _instructions[instruction.OpCode] = instruction;
        }
    }
}
