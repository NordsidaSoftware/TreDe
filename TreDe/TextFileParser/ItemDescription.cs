using System.Collections.Generic;

namespace TreDe.TextFileParser
{
    public class ItemDescription
    {
        public List<Statement> statements = new List<Statement>();
    }

    public class Statement { }

    public class AssignmentStatement : Statement
    {
        public Token Assignment;
        public Expression Expr;
    }
    public class FlagStatement : Statement
    {
        public Token Flag;
    }

    public class Expression { }
    public class Literal : Expression
    {
        public Token literal;
        public Literal (Token literal) { this.literal = literal; }
    }

}
