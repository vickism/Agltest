using System;
using System.Collections.Generic;

namespace AglTest.Helpers
{
    public static class EnumerableExtenstion
    {
	    public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
	    {
		    foreach (T item in @this)
		    {
			    action(item);
		    }
	    }
	}
}
