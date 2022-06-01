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
                token = lexer.NextToken();

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

        private Expression ParseExpression()
        {
            return ParseTerm();
        }

        private Expression ParseTerm()
        {
            var left = ParseFactor();
            while (Current.Type == SyntaxType.PlusToken
                   || Current.Type == SyntaxType.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParseFactor();
                left = new BinaryExpression(left, operatorToken, right);
            }

            return left;
        }

        private Expression ParseFactor()
        {
            var left = ParseExponent();
            while (Current.Type == SyntaxType.MultToken
                   || Current.Type == SyntaxType.DivToken)
            {
                var operatorToken = NextToken();
                var right = ParseExponent();
                left = new BinaryExpression(left, operatorToken, right);
            }

            return left;
        }

        private Expression ParseExponent()
        {
            var left = ParsePrimaryExpression();
            while (Current.Type == SyntaxType.ArrowToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
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