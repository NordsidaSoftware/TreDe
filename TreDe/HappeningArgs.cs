namespace TreDe
{
    public class HappeningArgs
    {
        public TypeOfComponent requires;
        public string text { get; }   // read only
        public HappeningArgs(TypeOfComponent requires, string text)
        {
            this.requires = requires;
            this.text = text;
        }
    }

}