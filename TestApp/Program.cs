using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TestApp
{
    internal class Program
    {
        private static void Main()
        {
            var t = new Tokenizer();
            t.TokenDefinitions.Add(new WhitespaceTokenDefinition());
            t.TokenDefinitions.Add(new WordTokenDefinition());
            t.TokenDefinitions.Add(new NumberTokenDefinition());
            t.TokenDefinitions.Add(new DelimTokenDefinition("{}[]();,"));

            Console.WriteLine("Tokenizing...");
            var res = t.Tokenize("if (abc(1,2)) { 123; }").ToArray();
            foreach (var token in res) Console.WriteLine($"\t{token}");

            Console.WriteLine();

            Console.WriteLine("Parsing...");
            var resnows = res.Where(tok => !(tok is WhitespaceTokenDefinition.WhitespaceToken)).ToArray();
            foreach (var token in resnows) Console.WriteLine($"\t{token}");
        }
    }

    public class WhitespaceTokenDefinition : Tokenizer.TokenDefinition
    {
        public class WhitespaceToken : Tokenizer.Token
        {
            public WhitespaceToken(string value) : base(value) { }
        }

        protected override bool InnerOk(char c) => char.IsWhiteSpace(c);
        public override bool Complete() => Buffer.Length > 0;
        public override Type TokenType => typeof(WhitespaceToken);
    }

    public class DelimTokenDefinition : Tokenizer.TokenDefinition
    {
        public class DelimToken : Tokenizer.Token
        {
            public DelimToken(string value) : base(value) { }
        }

        private readonly string _delimiters;
        public DelimTokenDefinition(string delimiters) { _delimiters = delimiters; }
        protected override bool InnerOk(char c) => _delimiters.Contains(c) && Buffer.Length == 0;
        public override bool Complete() => Buffer.Length == 1;
        public override Type TokenType => typeof(DelimToken);
    }

    public class NumberTokenDefinition : Tokenizer.TokenDefinition
    {
        public class NumberToken : Tokenizer.Token
        {
            public NumberToken(string value) : base(value) { }
        }

        protected override bool InnerOk(char c) => (c == '.' || char.IsDigit(c) || (c == ',' && Buffer.Length > 0)) && double.TryParse(Value + c, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double x);
        public override bool Complete() => Buffer.Length > 0 && double.TryParse(Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double x);
        public override Type TokenType => typeof(NumberToken);
    }

    public class WordTokenDefinition : Tokenizer.TokenDefinition
    {
        public class WordToken : Tokenizer.Token
        {
            public WordToken(string value) : base(value) { }
        }

        protected override bool InnerOk(char c) => char.IsLetter(c);
        public override bool Complete() => Buffer.Length > 0;
        public override Type TokenType => typeof(WordToken);
    }

    public class Tokenizer
    {
        public abstract class TokenDefinition
        {
            public abstract Type TokenType { get; }
            protected abstract bool InnerOk(char c);

            protected readonly StringBuilder Buffer = new StringBuilder();

            public int Count { get; protected set; }
            public string Value => Buffer.ToString();

            public bool Ok(char c)
            {
                Count++;
                var ret = InnerOk(c);
                if (ret) Buffer.Append(c);
                return ret;
            }

            public abstract bool Complete();

            public virtual void Reset()
            {
                Buffer.Clear();
                Count = 0;
            }
        }

        public class Token
        {
            public sealed class UnknownToken : Token
            {
                public UnknownToken(string value) : base(value) { }
            }

            public string Value { get; }
            public Token(string value)
            {
                Value = value;
            }
            public override string ToString()
            {
                var name = GetType().Name.Replace("Token", string.Empty);
                return $"{name}:{new string(' ', 30 - name.Length)}'{Value}'";
            }
        }

        public List<TokenDefinition> TokenDefinitions { get; } = new List<TokenDefinition>();

        public IEnumerable<Token> Tokenize(string s)
        {
            var pottoks = TokenDefinitions.ToList();

            var pos = 0;
            while (pos < s.Length)
            {
                foreach (var tokenDefinition in pottoks)
                    for (var i = pos; i < s.Length; i++)
                        if (!tokenDefinition.Ok(s[i])) break;

                if (pottoks.All(p => !p.Complete()))
                {
                    var minlen = pottoks.Min(p => p.Count);
                    yield return new Token.UnknownToken(s.Substring(pos, minlen));
                    pos += minlen;
                }
                else
                {
                    var res = pottoks.OrderByDescending(t => t.Value.Length).First();
                    yield return (Token)Activator.CreateInstance(res.TokenType, res.Value);
                    pos += res.Value.Length;
                }

                pottoks.ForEach(t => t.Reset());
            }
        }
    }
}
