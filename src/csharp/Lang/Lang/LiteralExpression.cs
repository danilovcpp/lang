using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class LiteralExpression : Expression
	{
		public object Value { get; }

		public LiteralExpression(object value)
		{
			Value = value;
		}

		public override T Accept<T>(IExpressionVisitor<T> expressionVisitor)
		{
			return expressionVisitor.VisitLiteralExpression(this);
		}
	}
}
