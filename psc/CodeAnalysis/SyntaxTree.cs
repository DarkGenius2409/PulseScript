namespace psc.CodeAnalysis
{
    sealed class SyntaxTree
    {
        public SyntaxTree(IEnumerable<string> diagnostics, Expression root, SyntaxToken eofToken)
        {
            Diagnostics = diagnostics.ToArray();
            Root = root;
            EofToken = eofToken;
        }

        public IReadOnlyList<string> Diagnostics { get; }
        public Expression Root { get; }
        public SyntaxToken EofToken { get; }

        public static SyntaxTree Parse(string text)
        {
            var parser = new Parser(text);
            return parser.Parse();
        }
    }
}