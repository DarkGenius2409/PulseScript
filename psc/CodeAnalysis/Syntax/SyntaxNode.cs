namespace psc.CodeAnalysis.Syntax
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxType Type { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}