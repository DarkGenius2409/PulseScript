namespace psc.CodeAnalysis.Syntax
{
    public enum SyntaxType
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