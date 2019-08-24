using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public class BinaryExpression : Expression
	{
		private readonly Expression _left;
		private readonly Expression _right;
		private readonly Token _operation;

		public BinaryExpression(Expression left, Token operation, Expression right)
		{
			_left = left;
			_operation = operation;
			_right = right;
		}
	}
}
