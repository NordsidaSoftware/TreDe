using System;
using System.Collections.Generic;
using System.Text;

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
            if (nextToken.TokenType == TokenType.EOF) { currentToken = nextToken; return; }
            currentToken = nextToken;
            nextToken = l.NextToken();
        }


        internal List<ItemDescription> ReadSource()
        {

            List<ItemDescription> ItemDescriptions = new List<ItemDescription>();
            StringBuilder sb = new StringBuilder();
            while (currentToken.TokenType != TokenType.EOF)
            {
                switch (currentToken.TokenType)
                {
                    case TokenType.Item: { ItemDescriptions.Add(RecordItem()); break; }
                }
              
                Advance();

            }
            return ItemDescriptions;
               
            
           

            
            ItemDescription RecordItem()
            {
                ItemDescription itemDescription = new ItemDescription();
                Advance();
                Expect(TokenType.Left_Bracket);
                while (currentToken.TokenType != TokenType.Right_Bracket)
                {
                    // All Item assignment is in the form <assignment> : <expression> 
                    // or <flag>
                    // expression can be <word> <number> <string> <grouping>
                    if (nextToken.TokenType == TokenType.Colon)
                    {
                        AssignmentStatement AS = new AssignmentStatement();
                        AS.Assignment = currentToken;
                        Advance();
                        Expect(TokenType.Colon);
                        AS.Expr = ReadExpression();
                        Advance();

                        itemDescription.statements.Add(AS);
                    }
                    else
                    {
                        FlagStatement FS = new FlagStatement();
                        FS.Flag = currentToken;
                        Advance();
                        itemDescription.statements.Add(FS);
                    }
                }

                return itemDescription;
            }

            void Expect(TokenType token)
            {
                if (currentToken.TokenType == token) { Advance(); return; }
                else throw new Exception("PARSE ERROR : Expected " + token.ToString() +
                    " Got " + currentToken.ToString());
            }

            Expression ReadExpression()
            {
                switch (currentToken.TokenType)
                {
                    case TokenType.Word: return new Literal(currentToken);
                    case TokenType.Number:return new Literal(currentToken);
                    case TokenType.String:return new Literal(currentToken);
                    case TokenType.Grouping:return new Literal(currentToken);
                }

                return new Expression();
            }
        }

    }
}