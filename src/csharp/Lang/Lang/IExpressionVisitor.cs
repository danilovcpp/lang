﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public interface IExpressionVisitor<T>
	{
		T VisitBinaryExpression(BinaryExpression expression);
		T VisitGroupingExpression(GroupingExpression expression);
		T VisitLiteralExpression(LiteralExpression expression);
		T VisitUnaryExpression(UnaryExpression expression);
	}
}
