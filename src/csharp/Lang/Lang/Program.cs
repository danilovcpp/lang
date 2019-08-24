using System;
using System.IO;

namespace Lang
{
	class Program
	{
		static bool hasError = false;

		static void Main(string[] args)
		{
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

			foreach (var token in tokens)
			{
				Console.WriteLine(token);
			}
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
