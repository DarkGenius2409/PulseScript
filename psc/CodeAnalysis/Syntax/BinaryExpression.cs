namespace PulseScript.CodeAnalysis.Syntax
{
    public sealed class BinaryExpression : Expression
    {
        public BinaryExpression(Expression left, SyntaxToken operatorToken, Expression right)
        {
            Left = left;
            OperatorToken = operatorToken;
            Right = right;
        }

        public Expression Left { get; }
        public SyntaxToken OperatorToken { get; }
        public Expression Right { get; }

        public override SyntaxKind Kind => SyntaxKind.BinaryExpression;

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return Left;
            yield return OperatorToken;
            yield return Right;
        }
    }
}