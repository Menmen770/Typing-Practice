namespace TypingPractice
{
    public class TextInfo
    {
        public string       DisplayName { get; }
        public string       Filename    { get; }
        public TextCategory Category    { get; }

        public TextInfo(string displayName, string filename, TextCategory category)
        {
            DisplayName = displayName;
            Filename    = filename;
            Category    = category;
        }

        public override string ToString() => DisplayName;
    }
}
