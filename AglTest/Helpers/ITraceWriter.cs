using System;

namespace AglTest.Helpers
{
	public interface ITraceWriter
	{
		void WriteHeading(string value);
		void WriteLine(string value );
	    ConsoleKeyInfo ReadKey();
    }
}
