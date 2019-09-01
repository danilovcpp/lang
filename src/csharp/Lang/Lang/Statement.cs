using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public abstract class Statement
	{
		public abstract T Accept<T>(IStatementVisitor<T> visitor);
	}
}
