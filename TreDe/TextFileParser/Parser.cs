using System;
using System.Collections.Generic;

namespace TreDe.TextFileParser
{
    internal class Parser
    {
        private Lexer l;
        private Token currentToken;
        private Token nextToken;

        public Parser(Lexer l)
        {
            this.l = l;
            Advance(); //  looks a bit weird, but advance twice initially will set 
            Advance(); //  currentToken to the first token, and nextToken to token nr. 2.
        }

        private void Advance()
        {
            currentToken = nextToken;
            nextToken = l.NextToken();
        }
        internal ItemDescription Run()
        {

            ItemDescription ItemDesc = new ItemDescription();

            while (!l.EOF)
            {
                switch (currentToken.TokenType)
                {
                    case TokenType.Item: { RecordItem(); break; }
                }
                Advance();
            }
            return ItemDesc;

            void RecordItem()
            {
                Advance();
                Expect(TokenType.Left_Bracket);
                while (currentToken.TokenType != TokenType.End)
                {
                AssignmentStatement AS = new AssignmentStatement();
                    AS.identifier = currentToken;
                    Advance();
                    Expect(TokenType.Colon);
                    AS.value = currentToken;
                    Advance();
                    ItemDesc.statements.Add(AS);
                }
            }

            void Expect(TokenType token)
            {
                if (currentToken.TokenType == token) { Advance(); return; }
                else throw new Exception("PARSE ERROR : Expected " + token.ToString() +
                    " Got " + currentToken.ToString());
            }
        }
    }
}