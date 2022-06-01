namespace psc.CodeAnalysis
{
    internal sealed class Parser
    {
        private readonly SyntaxToken[] _tokens;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();

            var lexer = new Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.Lex();

                if (token.Type != SyntaxType.WhitespaceToken && token.Type != SyntaxType.UnknownToken)
                {
                    tokens.Add(token);
                }
            } while (token.Type != SyntaxType.EOFToken);

            _tokens = tokens.ToArray();
            _diagnostics.AddRange(lexer.Diagnostics);
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private SyntaxToken Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _tokens.Length)
            {
                return _tokens[_tokens.Length - 1];
            }
            return _tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            _position++;
            return current;
        }

        private SyntaxToken MatchToken(SyntaxType type)
        {
            if (Current.Type == type)
                return NextToken();

            _diagnostics.Add($"ERROR: Unexpected token <{Current.Type}>, expected <{type}>");
            return new SyntaxToken(type, Current.Position, null, null);
        }

        public SyntaxTree Parse()
        {
            var expression = ParseExpression();
            var eofToken = MatchToken(SyntaxType.EOFToken);
            return new SyntaxTree(_diagnostics, expression, eofToken);
        }
        private Expression ParseExpression(int parentPrecedence = 0)
        {
            Expression left;
            var unaryOperatorPrecedence = Current.Type.GetUnaryOperatorPrecedence();
            if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
            {
                var operatorToken = NextToken();
                var operand = ParseExpression(unaryOperatorPrecedence);
                left = new UnaryExpression(operatorToken, operand);
            }
            else
            {
                left = ParsePrimaryExpression();
            }

            while (true)
            {
                var precedence = Current.Type.GetBinaryOperatorPrecedence();
                if (precedence == 0 || precedence <= parentPrecedence)
                    break;
                var operatorToken = NextToken();
                var right = ParseExpression(precedence);
                left = new BinaryExpression(left, operatorToken, right);
            }
            return left;
        }

        private Expression ParsePrimaryExpression()
        {
            if (Current.Type == SyntaxType.OpenParenthesisToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = MatchToken(SyntaxType.CloseParenthesisToken);
                return new ParenthesesExpression(left, expression, right);
            }
            var numberToken = MatchToken(SyntaxType.NumberToken);
            return new LiteralExpression(numberToken);
        }
    }
}