using System.Collections.Generic;

namespace ehBasic
{
    static class ListExtensions
    {
        public static T Shift<T>(this List<T> list)
        {
            T ret = list[0];

            list.RemoveAt(0);

            return ret;
        }
    }
}
