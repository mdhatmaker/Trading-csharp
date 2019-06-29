using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTAPI
{
    class Util
    {
        public static T ToEnum<T>(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                throw new ArgumentException("Argument null or empty");
            }
            if (@string.Length > 1)
            {
                throw new ArgumentException("Argument length greater than one");
            }
            return (T)Enum.ToObject(typeof(T), @string[0]);
        }
    } //class
} //namespace
