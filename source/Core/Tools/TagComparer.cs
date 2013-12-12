using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.Tools
{
 
    /// <summary>
    /// Compares 2 string ignoreing letters cases 
    /// </summary>
    public class CaseIgnoringStringComparer : IEqualityComparer<string>
    {

        public bool Equals(string x, string y)
        {
            return (x.ToLowerInvariant() == y.ToLowerInvariant());
        }


        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}