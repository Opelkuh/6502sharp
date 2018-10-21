namespace _6502sharp
{
    public interface IMachine
    {
        EventReadableBase Memory { get; }
        ICpu CPU { get; }
    }
}
