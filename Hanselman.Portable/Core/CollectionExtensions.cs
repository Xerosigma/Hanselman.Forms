﻿using System;
using System.Collections.Generic;

namespace Hanselman.Portable
{
	public static class CollectionExtensions
	{
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> newItems)
		{
			foreach (T item in newItems)
			{
				collection.Add (item);
			}
		}
	}
}

