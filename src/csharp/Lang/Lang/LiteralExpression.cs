using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class LiteralExpression : Expression
	{
		private readonly object _literal;

		public LiteralExpression(object literal)
		{
			_literal = literal;
		}

		public override T Accept<T>(IVisitor<T> visitor)
		{
			return visitor.VisitLiteralExpression(this);
		}
	}
}
