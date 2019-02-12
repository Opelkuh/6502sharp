namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        public int SleepCycles { get => _sleepFor; set => _sleepFor = value; }
        public int FinishedCycles { get => _finishedCycles; }

        private int _finishedCycles = 0;
        private int _sleepFor = 0;
        private bool _irqQueued = false;

        public void NextTick()
        {
            if (--_sleepFor <= 0)
            {
                bool irqChecked = false;

                Instruction inst = getCurrentOpcode();

                // check queued irq for 2 cycle instructions
                if (_irqQueued && inst.Cycles <= 2)
                {
                    InterruptIRQ(true);
                    irqChecked = true;
                }

                // invoke instruction
                invokeInstruction(inst);

                OnInstruction(inst);

                _sleepFor = inst.Cycles;

                // check queued irq interrupt
                if (_irqQueued && !irqChecked) InterruptIRQ(true);
            }

            OnCycle();

            _finishedCycles++;
        }

        public void NextInstruction()
        {
            SleepCycles = 0;
            NextTick();
        }

        public void Reset()
        {
            loadPC(0xFFFC, 0xFFFD);
            A.Value = 0;
            X.Value = 0;
            Y.Value = 0;
            SP.Value = 0xFD;

            SR.Value = 0;
            SR.Unused = true;
        }

        public void InterruptIRQ(bool queue)
        {
            if (!SR.Interrupt)
            {
                _irqQueued = false;

                interrupt(0xFFFE, 0xFFFF);

                return;
            }

            if (SR.Interrupt && queue)
            {
                _irqQueued = true;
            }
        }

        public void InterruptNMI()
        {
            interrupt(0xFFFA, 0xFFFB);
        }

        private void interrupt(int vecLo, int vecHi)
        {
            // save PC + 1
            PC.Value++;
            Stack.PushPC();

            // save status reg
            Stack.Push(SR.Value);

            // set interrupt flag
            SR.Interrupt = true;

            // set PC
            loadPC(vecLo, vecHi);

            // clear decimal flag on CMOS
            if (Type == CPUType.CMOS) SR.Decimal = false;
        }

        private void loadPC(int addrLo, int addrHi)
        {
            byte pcLo = Memory.Get(addrLo);
            byte pcHi = Memory.Get(addrHi);

            PC.Set(0, pcLo);
            PC.Set(1, pcHi);
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