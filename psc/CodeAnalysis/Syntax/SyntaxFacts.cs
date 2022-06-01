namespace PulseScript.CodeAnalysis.Syntax
{
    internal static class SyntaxFacts
    {

        public static int GetUnaryOperatorPrecedence(this SyntaxKind type)
        {
            switch (type)
            {
                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 4;

                default:
                    return 0;
            }
        }
        public static int GetBinaryOperatorPrecedence(this SyntaxKind type)
        {
            switch (type)
            {
                case SyntaxKind.ArrowToken:
                    return 3;

                case SyntaxKind.MultToken:
                case SyntaxKind.DivToken:
                    return 2;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;

                default:
                    return 0;
            }
        }
    }
}