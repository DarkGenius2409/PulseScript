namespace psc.CodeAnalysis
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxType Type { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}