namespace PulseScript.CodeAnalysis.Binding
{
    internal sealed class BoundParenthesesExpression : BoundExpression
    {
        public BoundParenthesesExpression(BoundExpression expression)
        {
            Expression = expression;
        }

        public override BoundNodeKind Kind => BoundNodeKind.ParenthesesExpression;

        public override Type Type => Expression.Type;

        public BoundExpression Expression { get; }
    }
}