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
                    case TokenType.Weapon: { ReadWeaponSource() ; break; }
                }
                Advance();

            }
            return ItemDescriptions;

            void ReadWeaponSource(){
                Weapon w = new Weapon(null);
                Advance();
                Expect(TokenType.Left_Bracket);
                while (currentToken.TokenType != TokenType.Right_Bracket)
                {
                    if (nextToken.TokenType == TokenType.Colon)
                    {
                        switch (currentToken.TokenType)
                        {
                            case TokenType.Name: ReadName(); break;
                            case TokenType.Glyph: ReadGlyph(); break;
                            case TokenType.Color: ReadColor(); break;
                            case TokenType.Edge: ReadEdge(); break;
                        }
                    }

                    else
                    {
                        switch(currentToken.TokenType)
                        {
                            case TokenType.Axe: w.Components.Add(new Component(TypeOfComponent.PHYSIC, w));break;
                            
                        }
                    }
                    Advance();
                }
                void ReadName()
                {
                    Advance();
                    Expect(TokenType.Colon);
                    w.Name = currentToken.Literal;
                }

                void ReadGlyph()
                {
                    Advance();
                    Expect(TokenType.Colon);
                    w.Glyph = Convert.ToInt32(currentToken.Literal);
                }

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
                        string assignment = currentToken.Literal;
                        Advance();
                        Expect(TokenType.Colon);
                        Token expression = currentToken;
                        Advance();

                        itemDescription.assignments[assignment] = expression;
                    }
                    else
                    {
                        string Flag = currentToken.Literal;
                        Advance();
                        itemDescription.flags.Add(Flag);
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
        }
    }
}