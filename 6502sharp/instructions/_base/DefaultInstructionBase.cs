namespace _6502sharp.Instructions {
    public abstract class InstructionBase {
        protected ICpu cpu;

        public InstructionBase(ICpu cpu) {
            this.cpu = cpu;
        }
    }
}