namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        public event CpuEventHandler Cycle;
        public event CpuEventHandler<InstructionEventArgs> Instruction;

        protected virtual void OnCycle()
        {
            CpuEventHandler handler = Cycle;
            handler?.Invoke(this);
        }

        protected virtual void OnInstruction(Instruction instruction)
        {
            CpuEventHandler<InstructionEventArgs> handler = Instruction;
            if (handler != null)
            {
                InstructionEventArgs args = new InstructionEventArgs
                {
                    OpCode = instruction.OpCode,
                    Cycles = instruction.Cycles
                };

                handler(this, args);
            }
        }
    }
}