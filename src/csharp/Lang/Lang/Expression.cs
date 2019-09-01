using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public abstract class Expression
	{
		public abstract T Accept<T>(IExpressionVisitor<T> expressionVisitor);
	}
}
