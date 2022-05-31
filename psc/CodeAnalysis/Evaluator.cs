namespace psc.CodeAnalysis
{

    class Evaluator
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
            if (node is NumberExpression n)
                return (int)n.NumberToken.Value;
            if (node is BinaryExpression b)
            {
                var left = EvaluateExpression(b.Left);
                var right = EvaluateExpression(b.Right);

                if (b.OperatorToken.Type == SyntaxType.PlusToken)
                    return left + right;
                else if (b.OperatorToken.Type == SyntaxType.MinusToken)
                    return left - right;
                else if (b.OperatorToken.Type == SyntaxType.MultToken)
                    return left * right;
                else if (b.OperatorToken.Type == SyntaxType.DivToken)
                    return left / right;
                else if (b.OperatorToken.Type == SyntaxType.ArrowToken)
                {
                    return (int)Math.Pow(left, right);
                }
                else
                    throw new Exception($"Unexpected binary operator {b.OperatorToken.Type}");
            }
            if (node is ParenthesesExpression p)
                return EvaluateExpression(p.Expression);


            throw new Exception($"Unexpected node {node.Type}");
        }
    }
}