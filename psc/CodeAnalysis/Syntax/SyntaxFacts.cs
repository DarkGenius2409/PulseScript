namespace psc.CodeAnalysis.Syntax
{
    internal static class SyntaxFacts
    {

        public static int GetUnaryOperatorPrecedence(this SyntaxType type)
        {
            switch (type)
            {
                case SyntaxType.PlusToken:
                case SyntaxType.MinusToken:
                    return 4;

                default:
                    return 0;
            }
        }
        public static int GetBinaryOperatorPrecedence(this SyntaxType type)
        {
            switch (type)
            {
                case SyntaxType.ArrowToken:
                    return 3;

                case SyntaxType.MultToken:
                case SyntaxType.DivToken:
                    return 2;

                case SyntaxType.PlusToken:
                case SyntaxType.MinusToken:
                    return 1;

                default:
                    return 0;
            }
        }
    }
}