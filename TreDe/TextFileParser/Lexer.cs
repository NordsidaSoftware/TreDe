using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreDe.TextFileParser
{
    public class Lexer
    {
        int position;
        int nextPosition;
        string source;

        Dictionary<string, Token> KeywordDictionary;

        public bool EOF { get { return position >= source.Length-1; } }
        private char ch {  get { return source[position]; } }

        public Lexer(string source)
        {
            this.source = source;
            KeywordDictionary = new Dictionary<string, Token>()
            {
                { "ITEM", new Token(TokenType.Item, "item")},
                { "END", new Token(TokenType.End, "end") },
                {"GLYPH", new Token(TokenType.Glyph, "glyph") },
                {"COLOR", new Token(TokenType.Color, "color") },
                {"NAME", new Token(TokenType.Name, "name") },
                {"TAG", new Token(TokenType.Tag, "tag") }
            };

        }
        public Token NextToken()
        {
            Advance();
            if (ch == '#') { ReadCommentLine(); NextToken(); }
            if (IsWhiteSpace(ch)) { ReadWhiteSpace(); }
            switch(ch)
            {
                case ']': return new Token(TokenType.Right_Bracket, ch.ToString());
                case '[': return new Token(TokenType.Left_Bracket, ch.ToString());
                case ':': return new Token(TokenType.Colon, ch.ToString());
            }
            if (IsLetter(ch)) { return (RegisterWorld( ReadWord())); }
            
            return new Token(TokenType.Unknown, ch.ToString());
        }

        private Token RegisterWorld(string keyword)
        {
            if (KeywordDictionary.ContainsKey(keyword)){
                return KeywordDictionary[keyword];
            }
            return new Token(TokenType.Identifier, keyword);
        }

        private void ReadCommentLine()
        {
            while (ch != '\n') { Advance(); }
        }

        private bool IsLetter(char ch)
        {
            if (ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z') { return true; }
            else { return false; }
        }
        private bool IsWhiteSpace(char ch)
        {
            if (ch == ' ' || ch == '\n' || ch == '\r') { return true; }
            else { return false; }
        }

        private void ReadWhiteSpace()
        {
            while(IsWhiteSpace(ch)) { Advance(); }
        }
        private string ReadWord()
        {
            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append(ch.ToString());
                Advance();
            } while (IsLetter(ch));
            Backtrack();
            return sb.ToString();
        }

        private void Advance()
        {
            if (!EOF)
            {
                position = nextPosition;
                nextPosition++;
            }
        }

        private  void Backtrack()
        {
            position--;
            nextPosition--;
        }
    }
}
