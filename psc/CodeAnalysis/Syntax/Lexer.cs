namespace psc.CodeAnalysis.Syntax
{
    internal sealed class Lexer
    {
        private readonly string _text;
        private int _position;
        private List<string> _diagnostics = new List<string>();

        public Lexer(string text)
        {
            _text = text;
        }

        public IEnumerable<string> Diagnostics => _diagnostics;

        private char Current
        {
            get
            {
                if (_position >= _text.Length)
                    return '\0';
                return _text[_position];
            }
        }

        private void Next()
        {
            _position++;
        }

        public SyntaxToken Lex()
        {
            if (_position >= _text.Length)
                return new SyntaxToken(SyntaxType.EOFToken, _position, "\0", null);

            if (char.IsDigit(Current))
            {
                var start = _position;

                while (char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                {
                    _diagnostics.Add($"The number {_text} isn't valed Int32");
                }
                return new SyntaxToken(SyntaxType.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {
                var start = _position;

                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxType.WhitespaceToken, start, text, null);
            }

            switch (Current)
            {
                case '+':
                    return new SyntaxToken(SyntaxType.PlusToken, _position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxType.MinusToken, _position++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxType.MultToken, _position++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxType.DivToken, _position++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxType.OpenParenthesisToken, _position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxType.CloseParenthesisToken, _position++, ")", null);
                case '^':
                    return new SyntaxToken(SyntaxType.ArrowToken, _position++, "^", null);
            }

            _diagnostics.Add($"ERROR: bad character input: '{Current}'");
            return new SyntaxToken(SyntaxType.UnknownToken, _position++, _text.Substring(_position - 1, 1), null);
        }

    }
}