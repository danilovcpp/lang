using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class Scanner
	{
		private readonly string _source;
		private readonly List<Token> _tokens = new List<Token>();
		private int _start = 0;
		private int _current = 0;
		private int _line = 1;

		private static readonly Dictionary<string, TokenType> _keywords = new Dictionary<string, TokenType>
		{
			["and"] = TokenType.And,
			["class"] = TokenType.Class,
			["else"] = TokenType.Else,
			["false"] = TokenType.False,
			["for"] = TokenType.For,
			["function"] = TokenType.Function,
			["if"] = TokenType.If,
			["null"] = TokenType.Null,
			["or"] = TokenType.Or,
			["print"] = TokenType.Print,
			["return"] = TokenType.Return,
			["super"] = TokenType.Super,
			["this"] = TokenType.This,
			["true"] = TokenType.True,
			["var"] = TokenType.Var,
			["while"] = TokenType.While
		};

		public Scanner(string source)
		{
			_source = source;
		}

		public List<Token> ScanTokens()
		{
			while (!IsAtEnd())
			{
				_start = _current;

				ScanToken();
			}

			_tokens.Add(new Token(TokenType.Eof, "", null, _line));

			return _tokens;
		}

		private void ScanToken()
		{
			var c = Advance();

			switch (c)
			{
				case '(':
					AddToken(TokenType.LeftParen);
					break;
				case ')':
					AddToken(TokenType.RightParen);
					break;
				case '{':
					AddToken(TokenType.LeftBrace);
					break;
				case '}':
					AddToken(TokenType.RightBrace);
					break;
				case ',':
					AddToken(TokenType.Comma);
					break;
				case '.':
					AddToken(TokenType.Dot);
					break;
				case '-':
					AddToken(TokenType.Minus);
					break;
				case '+':
					AddToken(TokenType.Plus);
					break;
				case ';':
					AddToken(TokenType.Semicolon);
					break;
				case '*':
					AddToken(TokenType.Star);
					break;
				case '!':
					AddToken(Match('=') ? TokenType.BangEqual : TokenType.Bang);
					break;
				case '=':
					AddToken(Match('=') ? TokenType.EqualEqual : TokenType.Equal);
					break;
				case '<':
					AddToken(Match('=') ? TokenType.LessEqual : TokenType.Less);
					break;
				case '>':
					AddToken(Match('=') ? TokenType.GreaterEqual : TokenType.Greater);
					break;
				case '/':
					if (Match('/'))
					{
						while (Peek() != '\n' && !IsAtEnd())
							Advance();
					}
					else
					{
						AddToken(TokenType.Slash);
					}
					break;
				case ' ':
				case '\r':
				case '\t':
					// Ignore whitespace
					break;
				case '\n':
					_line++;
					break;
				case '"':
					String();
					break;
				default:
					if (IsDigit(c))
					{
						Number();
					}
					else if (IsAlpha(c))
					{
						Identifier();
					}
					else
					{
						Console.WriteLine("Error");
					}
					break;
			}
		}

		private void Identifier()
		{
			while (IsAlphaNumeric(Peek()))
				Advance();

			var text = _source.Substring(_start, _current - _start);

			if (_keywords.ContainsKey(text))
			{
				AddToken(_keywords[text]);
				return;
			}

			AddToken(TokenType.Identifier);
		}

		private void Number()
		{
			while (IsDigit(Peek()))
				Advance();

			if (Peek() == '.' && IsDigit(PeekNext()))
			{
				Advance();

				while (IsDigit(Peek()))
					Advance();
			}

			AddToken(TokenType.Number, double.Parse(_source.Substring(_start, _current - _start)));
		}

		private void String()
		{
			while (Peek() != '"' && !IsAtEnd())
			{
				if (Peek() == '\n')
					_line++;

				Advance();
			}

			if (IsAtEnd())
			{
				Console.WriteLine($"{_line}: Unterminated string");
				return;
			}

			// The closing "
			Advance();

			// Trim the surrounding quotes
			var value = _source.Substring(_start + 1, _current - _start - 2);
			AddToken(TokenType.String, value);
		}

		private bool IsDigit(char c)
		{
			return c >= '0' && c <= '9';
		}

		private bool Match(char expected)
		{
			if (IsAtEnd()) return false;
			if (_source[_current] != expected) return false;

			_current++;
			return true;
		}

		private char Peek()
		{
			return IsAtEnd() ? '\0' : _source[_current];
		}

		private char PeekNext()
		{
			return _current + 1 >= _source.Length ? '\0' : _source[_current + 1];
		}

		private char Advance()
		{
			_current++;
			return _source[_current - 1];
		}

		private void AddToken(TokenType type, object literal = null)
		{
			var text = _source.Substring(_start, _current - _start);
			_tokens.Add(new Token(type, text, literal, _line));
		}

		private bool IsAlpha(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
		}

		private bool IsAlphaNumeric(char c)
		{
			return IsAlpha(c) || IsDigit(c);
		}

		private bool IsAtEnd()
		{
			return _current >= _source.Length;
		}
	}
}
