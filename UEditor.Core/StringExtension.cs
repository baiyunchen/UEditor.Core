using System;
using System.Collections.Generic;
using System.Text;

namespace UEditor.Core
{
    public static class StringExtension
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            if (value == null) return true;

            for (int i = 0; i < value.Length; i++)
            {
                if (!Char.IsWhiteSpace(value[i])) return false;
            }

            return true;
        }
    }
}
