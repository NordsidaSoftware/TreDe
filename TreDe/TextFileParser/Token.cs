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
            return (TokenType.ToString().ToUpper() + " ( " + Literal + " )");
        }
    }

    public enum TokenType
    {
        // single character tokens
        Left_Bracket,
        Right_Bracket,
        Left_Paren,
        Right_Paren,
        Colon,
        Quotation, 
        Aphostrophe,

        // literals
        Number,
        Grouping,
        Word,
        String,

        // assignements
        Item, 
        Tag, 
        Name, 
        Color, 
        Glyph,

        // flags

        Unwieldy,


        // other
        Unknown,
        EOF,
        Weapon,
        Edge,
        One_hand,
        Cut,
        Unbalanced,
        Axe
    }
}
