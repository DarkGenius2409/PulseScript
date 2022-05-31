namespace psc.CodeAnalysis
{
    sealed class NumberExpression : Expression
    {
        public NumberExpression(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override SyntaxType Type => SyntaxType.NumberExpression;

        public SyntaxToken NumberToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }
}