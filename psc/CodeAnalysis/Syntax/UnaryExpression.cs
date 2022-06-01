namespace psc.CodeAnalysis.Syntax
{
    public sealed class UnaryExpression : Expression
    {
        public UnaryExpression(SyntaxToken operatorToken, Expression operand)
        {
            OperatorToken = operatorToken;
            Operand = operand;
        }

        public SyntaxToken OperatorToken { get; }
        public Expression Operand { get; }

        public override SyntaxType Type => SyntaxType.UnaryExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OperatorToken;
            yield return Operand;
        }
    }
}