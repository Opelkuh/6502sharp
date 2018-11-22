using _6502sharp.Helpers;

namespace _6502sharp.Instructions {
    public abstract class InstructionBase {
        /// <summary>
        /// Injected CPU
        /// </summary>
        protected ICpu cpu;
        /// <summary>
        /// CompareHelper bound to the injected CPU
        /// </summary>
        protected CompareHelper compare;
        /// <summary>
        /// FlagHelper bound to the injected CPU
        /// </summary>
        protected FlagHelper flags;
        /// <summary>
        /// BCDHelper bound to the injected CPU
        /// </summary>
        protected BCDHelper bcd;

        public InstructionBase(ICpu cpu) {
            this.cpu = cpu;

            compare = new CompareHelper(cpu);
            flags = new FlagHelper(cpu);
            bcd = new BCDHelper(cpu);
        }
    }
}