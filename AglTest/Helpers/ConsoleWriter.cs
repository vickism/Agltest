using System;

namespace AglTest.Helpers
{
	public class ConsoleWriter : ITraceWriter
	{
		public void WriteHeading(string value)
		{
			Console.WriteLine($"--{value.ToUpperInvariant()}--");
		}

		public void WriteLine(string value)
		{
			Console.WriteLine(value);
		}

		public ConsoleKeyInfo ReadKey()
		{
			Console.WriteLine("Press any key to exit");
			return Console.ReadKey();
		}
	}
}