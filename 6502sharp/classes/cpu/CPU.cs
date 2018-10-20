namespace _6502sharp
{
    public partial class CPU
    {
        public CPU()
        {
            Reflector reflector = new Reflector();

            reflector.FindInjectables();
        }
    }
}
