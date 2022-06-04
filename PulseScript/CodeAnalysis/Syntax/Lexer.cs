namespace PulseScript.CodeAnalysis.Syntax
{
    internal sealed class Lexer
    {
        private readonly string _text;
        private int _position;
        private DiagnosticList _diagnostics = new DiagnosticList();

        public Lexer(string text)
        {
            _text = text;
        }

        public DiagnosticList Diagnostics => _diagnostics;

        private char Current => Peek(0);
        private char LookAhead => Peek(1);

        private char Peek(int offset)
        {
            var index = _position + offset;
            if (index >= _text.Length)
                return '\0';
            return _text[index];
        }

        private void Next()
        {
            _position++;
        }

        public SyntaxToken Lex()
        {
            var start = _position;

            if (_position >= _text.Length)
                return new SyntaxToken(SyntaxKind.EOFToken, _position, "\0", null);

            if (char.IsDigit(Current))
            {


                while (char.IsDigit(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                if (!int.TryParse(text, out var value))
                {
                    _diagnostics.ReportInvalidNumber(new TextSpan(start, length), _text, typeof(int));
                }
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if (char.IsWhiteSpace(Current))
            {


                while (char.IsWhiteSpace(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            if (char.IsLetter(Current))
            {


                while (char.IsLetter(Current))
                    Next();

                var length = _position - start;
                var text = _text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }

            switch (Current)
            {
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, _position++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")", null);
                case '^':
                    return new SyntaxToken(SyntaxKind.ArrowToken, _position++, "^", null);
                case '!':
                    if (LookAhead == '=')
                    {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.ExclamationEqualsToken, start, "!=", null);
                    }
                    else
                        return new SyntaxToken(SyntaxKind.ExclamationToken, _position++, "!", null);
                case '&':
                    if (LookAhead == '&')
                    {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.DoubleAmpersandToken, start, "&&", null);
                    }
                    break;
                case '|':
                    if (LookAhead == '|')
                    {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.DoublePipeToken, start, "||", null);
                    }
                    break;
                case '=':
                    if (LookAhead == '=')
                    {
                        _position += 2;
                        return new SyntaxToken(SyntaxKind.DoubleEqualsToken, start, "==", null);
                    }
                    break;
            }

            _diagnostics.ReportBadCharacter(_position, Current);
            return new SyntaxToken(SyntaxKind.UnknownToken, _position++, _text.Substring(_position - 1, 1), null);
        }

    }
}