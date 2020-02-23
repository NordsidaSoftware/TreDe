namespace TreDe.TextFileParser
{
    public struct Token
    {
        public TokenType TokenType;
        public string Literal;

        public Token(TokenType TokenType, string Literal)
        {
            this.TokenType = TokenType;
            this.Literal = Literal;
        }
        public override string ToString()
        {
            return ("< " + TokenType.ToString() + " - " + Literal + " >");
        }
    }

    public enum TokenType { Left_Bracket, Right_Bracket, Colon, Identifier,
                            Item, End, Color, Glyph, Name, Tag, Unknown }
}
