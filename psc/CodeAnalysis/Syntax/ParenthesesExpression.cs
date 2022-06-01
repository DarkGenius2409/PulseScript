namespace psc.CodeAnalysis.Syntax
{
    public sealed class ParenthesesExpression : Expression
    {
        public ParenthesesExpression(SyntaxToken openParenthesisToken, Expression expression, SyntaxToken closeParenthesisToken)
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }

        public override SyntaxType Type => SyntaxType.ParenthesesExpression;

        public SyntaxToken OpenParenthesisToken { get; }
        public Expression Expression { get; }
        public SyntaxToken CloseParenthesisToken { get; }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return CloseParenthesisToken;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}