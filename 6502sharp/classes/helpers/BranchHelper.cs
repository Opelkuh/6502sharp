using System;

namespace _6502sharp.Helpers
{
    /// <summary>
    /// Helper for compare instructions
    /// </summary>
    public class BranchHelper
    {
        private ICpu _cpu;

        /// <summary>
        /// Creates a new instance of BranchHelper
        /// </summary>
        /// <param name="cpu">cpu to branch</param>
        public BranchHelper(ICpu cpu)
        {
            _cpu = cpu;
        }

        /// <summary>
        /// Adjusts sleep cycles based on current and target PC value
        /// </summary>
        /// <param name="target">target of branch</param>
        public void Branch(int target)
        {
            ushort res = (ushort)target;

            // 3 cycles if branch is taken
            _cpu.SleepCycles++;

            // 4 cycles if target is on different page
            if (_cpu.Type == CPUType.CMOS || (res & 0xFF00) != (_cpu.PC.Value & 0xFF00))
                _cpu.SleepCycles++;

            _cpu.PC.Value = res;
        }
    }
}
