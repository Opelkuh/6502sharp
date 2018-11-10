using _6502sharp.Helpers;

namespace _6502sharp
{
    public class Stack : IStack
    {
        private ICpu _cpu;
        private int _offset = 0x100;

        /// <summary>
        /// Offset of memory where the stack is stored
        /// </summary>
        public int Offset { get => _offset; set => _offset = value; }

        public Stack(ICpu cpu)
        {
            _cpu = cpu;
        }

        public void Push(params byte[] values)
        {
            foreach (byte val in values)
            {
                _cpu.Memory.Set(_offset + _cpu.SP.Value, val);

                _cpu.SP.Value--;
            }
        }

        public void PushPC()
        {
            byte[] pc = {
                (byte)(_cpu.PC.Value >> 8),
                (byte)(_cpu.PC.Value & 0xFF)
            };

            Push(pc);
        }

        public byte Pop()
        {
            byte target = ++_cpu.SP.Value;
            return _cpu.Memory.Get(_offset + target);
        }

        public int PopPC()
        {
            byte[] pc = PopMultiple(2);

            return LEHelper.From(pc);
        }

        public byte[] PopMultiple(int length)
        {
            byte[] ret = new byte[length];

            for (int i = 0; i < length; i++)
            {
                ret[i] = Pop();
            }

            return ret;
        }
    }
}
