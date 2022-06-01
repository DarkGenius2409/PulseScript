namespace PulseScript.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        UnknownToken,
        EOFToken,
        WhitespaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        MultToken,
        DivToken,
        ArrowToken,
        OpenParenthesisToken,
        CloseParenthesisToken,

        // Expressions
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesesExpression,

    }
}