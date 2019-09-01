using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class GroupingExpression : Expression
	{
		public Expression Expression { get; }

		public GroupingExpression(Expression expression)
		{
			Expression = expression;
		}

		public override T Accept<T>(IExpressionVisitor<T> expressionVisitor)
		{
			return expressionVisitor.VisitGroupingExpression(this);
		}
	}
}
