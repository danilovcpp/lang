using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class BinaryExpression : Expression
	{
		public Expression Left { get; }
		public Expression Right { get; }
		public Token Operation { get; }

		public BinaryExpression(Expression left, Token operation, Expression right)
		{
			Left = left;
			Operation = operation;
			Right = right;
		}

		public override T Accept<T>(IExpressionVisitor<T> expressionVisitor)
		{
			return expressionVisitor.VisitBinaryExpression(this);
		}
	}
}
