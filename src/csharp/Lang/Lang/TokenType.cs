using System;
using System.Collections.Generic;
using System.Text;

namespace Lang
{
	public enum TokenType
	{
		LeftParen,
		RightParen,
		LeftBrace,
		RightBrace,
		Comma,
		Dot,
		Minus,
		Plus,
		Semicolon,
		Slash,
		Star,

		Bang,
		BangEqual,
		Equal,
		EqualEqual,
		Greater,
		GreaterEqual,
		Less,
		LessEqual,

		Identifier,
		String,
		Number,

		And,
		Class,
		Else,
		False,
		Function,
		For,
		If,
		Null,
		Or,
		Print,
		Return,
		Super,
		This,
		True,
		Var,
		While,

		Eof
	}
}
