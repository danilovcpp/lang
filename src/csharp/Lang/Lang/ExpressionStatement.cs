using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class ExpressionStatement : Statement
	{
		public Expression Expression { get; set; }

		public ExpressionStatement(Expression expression)
		{
			Expression = expression;
		}

		public override T Accept<T>(IStatementVisitor<T> visitor)
		{
			return visitor.VisitExpressionStatement(this);
		}
	}
}
