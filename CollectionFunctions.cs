using System;
using System.Linq;
using System.Collections.Generic;

namespace DevToolbox.Helpers 
{
  public static class CollectionFunctions
  {
    /// <summary>
    /// Joins items into a natural language list, e.g. "A, B, and C"
    /// </summary>
    /// <param name="items"></param>
    /// <param name="listSeparator"></param>
    /// <param name="listTerminator"></param>
    /// <returns></returns>
    public static string Oxford(this IEnumerable<string> items, string listSeparator = ",", string listTerminator = "and")
    {
        //special case, e.g. "A and B", which should not have commas
        if (items.Count() == 2)
        {
            return string.Join($" {listTerminator} ", items);
        }
        var lastItemIndex = items.Count() - 1;
        var commaSet = items.Take(lastItemIndex).Select(i => $"{i}{listSeparator}");
        var lastItem = items.Skip(lastItemIndex).Select(i => $"{listTerminator} {i}");
        return string.Join(" ", commaSet.Concat(lastItem));
    }
  }
}
