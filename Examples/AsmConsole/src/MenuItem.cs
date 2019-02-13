namespace AsmConsole
{
    struct MenuItem
    {
        public string displayValue;
        public string value;

        public MenuItem(string displayValue, string value)
        {
            this.displayValue = displayValue;
            this.value = value;
        }
    }
}
