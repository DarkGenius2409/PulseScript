namespace psc.CodeAnalysis
{
    enum SyntaxType
    {
        NumberToken,
        WhitespaceToken,
        PlusToken,
        MinusToken,
        MultToken,
        DivToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        UnknownToken,
        EOFToken,
        NumberExpression,
        BinaryExpression,
        ParenthesesExpression,
        ArrowToken
    }
}