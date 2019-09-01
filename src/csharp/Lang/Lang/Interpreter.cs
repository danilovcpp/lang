using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class Interpreter : IExpressionVisitor<object>, IStatementVisitor<object>
	{
		public void Interpret(List<Statement> statements)
		{
			try
			{
				foreach (var statement in statements)
				{
					Execute(statement);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			//try
			//{
			//	var value = Evaluate(expression);
			//	Console.WriteLine($"= {Stringify(value)}");
			//}
			//catch (Exception ex)
			//{
			//	Console.WriteLine(ex.Message);
			//}
		}

		private void Execute(Statement statement)
		{
			statement.Accept(this);
		}

		public object VisitBinaryExpression(BinaryExpression expression)
		{
			var left = Evaluate(expression.Left);
			var right = Evaluate(expression.Right);

			switch (expression.Operation.Type)
			{
				case TokenType.Greater:
					return (double)left > (double)right;
				case TokenType.GreaterEqual:
					return (double)left >= (double)right;
				case TokenType.Less:
					return (double)left < (double)right;
				case TokenType.LessEqual:
					return (double)left <= (double)right;
				case TokenType.BangEqual:
					return !IsEqual(left, right);
				case TokenType.EqualEqual:
					return IsEqual(left, right);
				case TokenType.Minus:
					return (double)left - (double)right;
				case TokenType.Plus:
					if (left is double && right is double)
						return (double)left + (double)right;
					if (left is string && right is string)
						return (string)left + (string)right;
					break;
				case TokenType.Slash:
					return (double)left / (double)right;
				case TokenType.Star:
					return (double)left * (double)right;
			}

			// unreachable
			return null;
		}

		public object VisitGroupingExpression(GroupingExpression expression) => Evaluate(expression.Expression);

		public object VisitLiteralExpression(LiteralExpression expression) => expression.Value;

		public object VisitUnaryExpression(UnaryExpression expression)
		{
			var right = Evaluate(expression.Right);

			switch (expression.Operation.Type)
			{
				case TokenType.Bang:
					return !IsTruthy(right);
				case TokenType.Minus:
					return -(double)right;
			}

			// unreachable
			return null;
		}

		private object Evaluate(Expression expression) => expression.Accept(this);

		private bool IsTruthy(object @object)
		{
			if (@object == null) return false;
			if (@object is bool truthy) return truthy;

			return true;
		}

		private bool IsEqual(object a, object b)
		{
			if (a == null && b == null) return true;
			if (a == null) return false;

			return a.Equals(b);
		}

		private string Stringify(object @object)
		{
			if (@object == null) return "null";

			return @object.ToString();
		}

		public object VisitExpressionStatement(ExpressionStatement statement)
		{
			Evaluate(statement.Expression);

			return null;
		}

		public object VisitPrintStatement(PrintStatement statement)
		{
			var value = Evaluate(statement.Expression);
			Console.WriteLine(Stringify(value));

			return null;
		}
	}
}
