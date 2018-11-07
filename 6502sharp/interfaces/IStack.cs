namespace _6502sharp
{
    public interface IStack
    {
        /// <summary>
        /// Pushes new value(s) onto the stack.
        /// Values are pushed in the provided order.
        /// </summary>
        /// <param name="value">values to be added</param>
        void Push(params byte[] values);

        /// <summary>
        /// Removes and returns the topmost value from the stack
        /// </summary>
        /// <returns>removed value</returns>
        byte Pop();

        /// <summary>
        /// Removes and returns multiple topmost values from the stack.
        /// The returned value is the first popped value.
        /// </summary>
        /// <param name="length">number of values to pop</param>
        /// <returns>popped values (first popped value = first returned value)</returns>
        byte[] PopMultiple(int length);
    }
}
