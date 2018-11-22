using _6502sharp.Helpers;

namespace _6502sharp.Instructions {
    public abstract class InstructionBase {
        protected ICpu cpu;
        protected CompareHelper compare;

        public InstructionBase(ICpu cpu) {
            this.cpu = cpu;
            
            compare = new CompareHelper(cpu);
        }
    }
}