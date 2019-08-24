using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class GroupingExpression : Expression
	{
		private readonly Expression _expression;

		public GroupingExpression(Expression expression)
		{
			_expression = expression;
		}

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.VisitGroupingExpression(this);
		}
	}
}
