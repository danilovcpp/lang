using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class PrintStatement : Statement
	{
		public Expression Expression { get; set; }

		public PrintStatement(Expression expression)
		{
			Expression = expression;
		}

		public override T Accept<T>(IStatementVisitor<T> visitor)
		{
			return visitor.VisitPrintStatement(this);
		}
	}
}
