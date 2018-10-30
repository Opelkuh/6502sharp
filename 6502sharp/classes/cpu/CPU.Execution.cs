namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        public int SleepCycles { get => _sleepFor; set => _sleepFor = value; }
        
        private int _sleepFor = 0;
        private Instruction _nextInstruction;

        public void Tick()
        {
            if (--_sleepFor <= 0)
            {
                invokeInstruction(_nextInstruction);

                _nextInstruction = getCurrentOpcode();
                _sleepFor = _nextInstruction.Cycles;
            }
        }

        private void invokeInstruction(Instruction instruction)
        {
            if (instruction.Delegate == null) return;

            instruction.Delegate.Invoke();
        }

        private Instruction getCurrentOpcode()
        {
            byte opcode = _machine.Memory.Get(this.PC.Value);
            return _instructions[opcode];
        }
    }
}