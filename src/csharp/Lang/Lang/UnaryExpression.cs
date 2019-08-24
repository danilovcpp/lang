using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class UnaryExpression : Expression
	{
		private readonly Token _operation;
		private readonly Expression _right;

		public UnaryExpression(Token operation, Expression right)
		{
			_operation = operation;
			_right = right;
		}
	}
}
