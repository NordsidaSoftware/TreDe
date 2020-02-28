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
        int line;

        Dictionary<string, Token> KeywordDictionary;

        public bool EOF { get { return position >= source.Length-1; } }
        private char ch {  get { return source[position]; } }

        public Lexer(string source)
        {
            this.source = source;
            KeywordDictionary = new Dictionary<string, Token>()
            {
                { "ITEM", new Token(TokenType.Item,  "item")},
                {"GLYPH", new Token(TokenType.Glyph,  "glyph") },
                {"COLOR", new Token(TokenType.Color,  "color") },
                {"NAME",  new Token(TokenType.Name,   "name") },
                {"TAG",   new Token(TokenType.Tag,    "tag") },
                {"UNWIELDY", new Token(TokenType.Unwieldy, "unwieldy") }
            };

        }
        public Token NextToken()
        {
            Advance();
            if (EOF) { return new Token(TokenType.EOF, "eof"); }
            if (IsWhiteSpace(ch)) { ReadWhiteSpace(); }
            switch(ch)
            {
                case ']': return new Token(TokenType.Right_Bracket, ch.ToString());
                case '[': return new Token(TokenType.Left_Bracket, ch.ToString());
                case ':': return new Token(TokenType.Colon, ch.ToString());
            }
            if (IsLetter(ch)) { return (RegisterWorld( ReadWord())); }
            if (IsNumber(ch)) { return (ReadNumber()); }
            if (ch == '"' ) { return (ReadLiteral()); }
            if (ch == '(') {  return ReadGrouping(); }
            
            return new Token(TokenType.Unknown, ch.ToString());
        }

        private Token ReadGrouping()
        {
            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append(ch);
                Advance();

            }
            while (ch != ')');
            sb.Append(ch);
            Advance();

            return new Token(TokenType.Grouping, sb.ToString());
        }

        private Token ReadLiteral()
        {
            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append(ch);
                Advance();
               
            }
            while (ch != '"');
            sb.Append(ch);
            Advance();

            return new Token(TokenType.String, sb.ToString());
        }

        private Token RegisterWorld(string keyword)
        {
            if (KeywordDictionary.ContainsKey(keyword)){
                return KeywordDictionary[keyword];
            }
            return new Token(TokenType.Word, keyword);
        }

        private Token ReadNumber()
        {
            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append(ch.ToString());
                Advance();
            } while (IsNumber(ch));
            Backtrack();

            return new Token(TokenType.Number, sb.ToString());
        }

        private bool IsLetter(char ch)
        {
            if (ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z') { return true; }
            else { return false; }
        }

        private bool IsNumber(char ch)
        {
            // if (ch >= 0 && ch <= 9) { return true; }
            // else { return false; }
            return char.IsNumber(ch);
        }
        private bool IsWhiteSpace(char ch)
        {
            if (ch == ' ' || ch == '\n' || ch == '\r' || ch == '\t') { return true; }
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
