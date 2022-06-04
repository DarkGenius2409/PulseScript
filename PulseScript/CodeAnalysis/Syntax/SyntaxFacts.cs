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
                case SyntaxKind.ExclamationToken:
                    return 7;

                default:
                    return 0;
            }
        }
        public static int GetBinaryOperatorPrecedence(this SyntaxKind type)
        {
            switch (type)
            {
                case SyntaxKind.ArrowToken:
                    return 6;

                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 5;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 4;

                case SyntaxKind.DoubleEqualsToken:
                case SyntaxKind.ExclamationEqualsToken:
                    return 3;

                case SyntaxKind.DoubleAmpersandToken:
                    return 2;

                case SyntaxKind.DoublePipeToken:
                    return 1;

                default:
                    return 0;
            }
        }

        public static SyntaxKind GetKeywordKind(string text)
        {
            switch (text)
            {
                case "true":
                    return SyntaxKind.TrueKeyword;
                case "false":
                    return SyntaxKind.FalseKeyword;
                default:
                    return SyntaxKind.IdentifierToken;
            }
        }
    }
}