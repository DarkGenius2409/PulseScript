using psc.CodeAnalysis.Syntax;

namespace psc.CodeAnalysis
{

    public sealed class Evaluator
    {
        private readonly Expression _root;

        public Evaluator(Expression root)
        {
            _root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(_root);
        }

        private int EvaluateExpression(Expression node)
        {
            if (node is LiteralExpression n)
                return (int)n.LiteralToken.Value;
            if (node is UnaryExpression u)
            {
                var operand = EvaluateExpression(u.Operand);

                switch (u.OperatorToken.Type)
                {
                    case SyntaxType.PlusToken:
                        return operand;
                    case SyntaxType.MinusToken:
                        return -operand;
                    default:
                        throw new Exception($"Unexpected unary operator {u.OperatorToken.Type}");
                }

            }
            if (node is BinaryExpression b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                switch (b.OperatorToken.Type)
                {
                    case SyntaxType.PlusToken:
                        return left + right;
                    case SyntaxType.MinusToken:
                        return left - right;
                    case SyntaxType.MultToken:
                        return left * right;
                    case SyntaxType.DivToken:
                        return left / right;
                    case SyntaxType.ArrowToken:
                        return (int)Math.Pow(left, right);
                    default:
                        throw new Exception($"Unexpected binary operator {b.OperatorToken.Type}");
                }
            }
            if (node is ParenthesesExpression p)
                return EvaluateExpression(p.Expression);


            throw new Exception($"Unexpected node {node.Type}");
        }
    }
}