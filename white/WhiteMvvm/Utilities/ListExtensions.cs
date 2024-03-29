﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Utilities
{
    public static class ListExtensions 
    {
        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
