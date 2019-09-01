using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public interface IStatementVisitor<T>
	{
		T VisitExpressionStatement(ExpressionStatement statement);
		T VisitPrintStatement(PrintStatement statement);
	}
}
