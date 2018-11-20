namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        public CPUType Type => _type;
        public bool DecimalMode { get => _decimalMode; set => _decimalMode = value; }
        public IStack Stack { get => _stack; set => _stack = value; }

        private IMachine _machine;
        private IStack _stack;
        private CPUType _type;
        private bool _decimalMode = true;
        private Instruction[] _instructions = new Instruction[256];

        public CPU(IMachine machine, CPUType type)
        {
            _machine = machine;
            _type = type;
            _stack = new Stack(this);

            FindDefaultInjectables();
            FindInjectables();
        }

        public void RegisterInstruction(Instruction instruction)
        {
            _instructions[instruction.OpCode] = instruction;
        }
    }
}
