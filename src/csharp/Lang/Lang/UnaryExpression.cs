using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class UnaryExpression : Expression
	{
		public Token Operation { get; }
		public Expression Right { get; }

		public UnaryExpression(Token operation, Expression right)
		{
			Operation = operation;
			Right = right;
		}

		public override T Accept<T>(IExpressionVisitor<T> expressionVisitor)
		{
			return expressionVisitor.VisitUnaryExpression(this);
		}
	}
}
