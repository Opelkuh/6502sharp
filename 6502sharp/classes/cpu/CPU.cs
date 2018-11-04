namespace _6502sharp
{
    public partial class CPU : ICpu
    {
        public CPUType Type => _type;
        public bool DecimalMode { get => _decimalMode; set => _decimalMode = value; }

        private IMachine _machine;
        private CPUType _type;
        private bool _decimalMode = true;
        private Instruction[] _instructions = new Instruction[256];

        public CPU(IMachine machine, CPUType type)
        {
            _machine = machine;
            _type = type;

            FindInjectables();
        }

        public void RegisterInstruction(Instruction instruction)
        {
            _instructions[instruction.OpCode] = instruction;
        }
    }
}
