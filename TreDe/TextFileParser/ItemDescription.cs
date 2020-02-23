using System.Collections.Generic;

namespace TreDe.TextFileParser
{
    public class ItemDescription
    {
        public List<Statement> statements = new List<Statement>();
    }

    public class Statement
    {

    }

    public class AssignmentStatement : Statement
    {
        public Token assignmentToken;
        public Token identifier;
        public Token value;
    }

}
