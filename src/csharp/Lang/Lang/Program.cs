using System;
using System.IO;

namespace Lang
{
	class Program
	{
		static bool hasError = false;

		static void Main(string[] args)
		{
			var expression = new BinaryExpression(
				new UnaryExpression(
					new Token(TokenType.Minus, "-", null, 1),
					new LiteralExpression(2.0)
				),
				new Token(TokenType.Star, "*", null, 1),
				new GroupingExpression(
					new LiteralExpression(5.0)
				)
			);

			Console.WriteLine(new Printer().Print(expression));

			//var interpreter = new Interpreter();
			//interpreter.Interpret(expression);

			if (args.Length > 1)
			{
				Console.WriteLine("Usage lang [script]");
			}
			else if (args.Length == 1)
			{
				RunFile(args[0]);
			}
			else
			{
				RunPrompt();
			}
		}

		static void RunFile(string path)
		{
			var source = File.ReadAllText(path);
			Run(source);

			//if(hasError) 
		}

		static void RunPrompt()
		{
			while (true)
			{
				Console.Write(">");
				Run(Console.ReadLine());
				hasError = false;
			}
		}

		static void Run(string source)
		{
			var scanner = new Scanner(source);
			var tokens = scanner.ScanTokens();

			var parser = new Parser(tokens);
			var statements = parser.Parse();

			var interpreter = new Interpreter();
			interpreter.Interpret(statements);
		}


		static void Error(int line, string message)
		{
			Report(line, "", message);
		}

		static void Report(int line, string where, string message)
		{
			Console.WriteLine($"[line: {line}] Error {where}: {message}");
			hasError = true;
		}
	}
}
