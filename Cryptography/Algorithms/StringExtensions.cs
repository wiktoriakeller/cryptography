using System;
using System.Text;

namespace Algorithms
{
    public static class StringExtensions
    {
        public static string DiscardCharacters(this string s, HashSet<char> lettersToDiscard)
        {
            var sb = new StringBuilder();
            for(int i = 0; i < s.Length; i++)
            {
                if (lettersToDiscard.Contains(s[i]))
                    continue;

                sb.Append(s[i]);
            }

            return sb.ToString();
        }
    }
}
