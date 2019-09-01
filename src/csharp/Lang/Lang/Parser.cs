using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class Parser
	{
		private readonly List<Token> _tokens;
		private int _current = 0;

		public Parser(List<Token> tokens)
		{
			_tokens = tokens;
		}

		public List<Statement> Parse()
		{
			List<Statement> statements = new List<Statement>();

			while (!IsAtEnd())
			{
				statements.Add(Statement());
			}

			return statements;

			//try
			//{
			//	return Expression();
			//}
			//catch (Exception ex)
			//{
			//	Console.WriteLine(ex.Message);

			//	return null;
			//}
		}

		private Expression Expression()
		{
			return Equality();
		}

		private Statement Statement()
		{
			if (Match(TokenType.Print))
				return PrintStatement();

			return ExpressionStatement();
		}

		private Statement PrintStatement()
		{
			var value = Expression();
			Consume(TokenType.Semicolon, "Expect ';' after value.");
			return new PrintStatement(value);
		}

		private Statement ExpressionStatement()
		{
			var expression = Expression();

			Consume(TokenType.Semicolon, "Expect ';' after expression");

			return new ExpressionStatement(expression);
		}

		private Expression Equality()
		{
			var expression = Comparison();

			while (Match(TokenType.BangEqual, TokenType.EqualEqual))
			{
				var operation = Previous();
				var right = Comparison();

				expression = new BinaryExpression(expression, operation, right);
			}

			return expression;
		}

		private Expression Comparison()
		{
			var expression = Addition();

			while (Match(TokenType.Greater, TokenType.GreaterEqual, TokenType.Less, TokenType.LessEqual))
			{
				var operation = Previous();
				var right = Addition();

				expression = new BinaryExpression(expression, operation, right);
			}

			return expression;
		}

		private Expression Addition()
		{
			var expression = Multiplication();

			while (Match(TokenType.Minus, TokenType.Plus))
			{
				var operation = Previous();
				var right = Multiplication();

				expression = new BinaryExpression(expression, operation, right);
			}

			return expression;
		}

		private Expression Multiplication()
		{
			var expression = Unary();

			while (Match(TokenType.Slash, TokenType.Star))
			{
				var operation = Previous();
				var right = Unary();

				expression = new BinaryExpression(expression, operation, right);
			}

			return expression;
		}

		private Expression Unary()
		{
			if (Match(TokenType.Bang, TokenType.Minus))
			{
				var operation = Previous();
				var right = Unary();

				return new UnaryExpression(operation, right);
			}

			return Primary();
		}

		private Expression Primary()
		{
			if (Match(TokenType.False)) return new LiteralExpression(false);
			if (Match(TokenType.True)) return new LiteralExpression(true);
			if (Match(TokenType.Null)) return new LiteralExpression(null);

			if (Match(TokenType.Number, TokenType.String))
			{
				return new LiteralExpression(Previous().Literal);
			}

			if (Match(TokenType.LeftParen))
			{
				var expression = Expression();
				Consume(TokenType.RightParen, "Expect ')' after expression");

				return new GroupingExpression(expression);
			}

			throw new Exception();
		}

		private Token Consume(TokenType type, string message)
		{
			if (Check(type)) return Advance();

			throw new Exception($"{Peek()} {message}");
		}

		private void Synchronize()
		{
			Advance();

			while (!IsAtEnd())
			{
				if (Previous().Type == TokenType.Semicolon)
					return;

				switch (Peek().Type)
				{
					case TokenType.Class:
					case TokenType.Function:
					case TokenType.Var:
					case TokenType.For:
					case TokenType.If:
					case TokenType.While:
					case TokenType.Print:
					case TokenType.Return:
						return;
				}

				Advance();
			}
		}

		private bool Match(params TokenType[] types)
		{
			foreach (var type in types)
			{
				if (Check(type))
				{
					Advance();

					return true;
				}
			}

			return false;
		}


		private bool Check(TokenType type)
		{
			if (IsAtEnd())
				return false;

			return Peek().Type == type;
		}

		private Token Advance()
		{
			if (!IsAtEnd()) _current++;

			return Previous();
		}

		private bool IsAtEnd() => Peek().Type == TokenType.Eof;

		private Token Peek() => _tokens[_current];

		private Token Previous() => _tokens[_current - 1];
	}
}
