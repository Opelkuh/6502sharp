namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        public int SleepCycles { get => _sleepFor; set => _sleepFor = value; }
        public int FinishedCycles { get => _finishedCycles; }

        private int _finishedCycles = 0;

        private int _sleepFor = 0;

        public void Tick()
        {
            if (--_sleepFor <= 0)
            {
                Instruction inst = getCurrentOpcode();
                invokeInstruction(inst);

                OnInstruction(inst);

                _sleepFor = inst.Cycles;
            }

            OnCycle();

            _finishedCycles++;
        }

        private void invokeInstruction(Instruction instruction)
        {
            if (instruction.Delegate == null) return;

            instruction.Delegate.Invoke();
        }

        private Instruction getCurrentOpcode()
        {
            byte opcode = FetchNext();
            return _instructions[opcode];
        }
    }
}