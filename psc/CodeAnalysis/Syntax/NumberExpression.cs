namespace psc.CodeAnalysis.Syntax
{
    sealed class LiteralExpression : Expression
    {
        public LiteralExpression(SyntaxToken literalToken)
        {
            LiteralToken = literalToken;
        }

        public override SyntaxType Type => SyntaxType.LiteralExpression;

        public SyntaxToken LiteralToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}