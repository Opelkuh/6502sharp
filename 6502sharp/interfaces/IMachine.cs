namespace _6502sharp
{
    public interface IMachine
    {
        IReadable Memory { get; }
        ICpu CPU { get; }
    }
}
