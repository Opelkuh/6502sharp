namespace _6502sharp
{
    public interface IReadable
    {
        byte Get(int location);
        void Set(int location, byte value);
    }
}
