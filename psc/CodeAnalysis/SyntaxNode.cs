namespace psc.CodeAnalysis
{
    abstract class SyntaxNode
    {
        public abstract SyntaxType Type { get; }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}