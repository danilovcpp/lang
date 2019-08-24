using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class Printer : IVisitor<string>
	{
		public string Print(Expression expression)
		{
			return expression.Accept(this);
		}

		public string VisitBinaryExpression(BinaryExpression expression)
		{
			return Parenthesize(expression.Operation.Lexeme, expression.Left, expression.Right);
		}

		public string VisitGroupingExpression(GroupingExpression expression)
		{
			return Parenthesize("group", expression.Expression);
		}

		public string VisitLiteralExpression(LiteralExpression expression)
		{
			return expression.Value == null ? "null" : expression.Value.ToString();
		}

		public string VisitUnaryExpression(UnaryExpression expression)
		{
			return Parenthesize(expression.Operation.Lexeme, expression.Right);
		}

		private string Parenthesize(string name, params Expression[] expressions)
		{
			var builder = new StringBuilder();

			builder.Append("(").Append(name);

			foreach (var expression in expressions)
			{
				builder.Append(" ").Append(expression.Accept(this));
			}

			builder.Append(")");

			return builder.ToString();
		}
	}
}
