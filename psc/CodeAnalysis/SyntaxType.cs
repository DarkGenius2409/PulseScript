namespace psc.CodeAnalysis
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
        BinaryExpression,
        ParenthesesExpression,

    }
}