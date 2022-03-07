using System;
using System.Text;

namespace Algorithms
{
    public static class StringExtensions
    {
        public static string ReplaceCharacters(this string s, IDictionary<string, string> replacements)
        {
            var sb = new StringBuilder();
            string letter;
            for(int i = 0; i < s.Length; i++)
            {
                letter = s[i].ToString();
                if(replacements.ContainsKey(letter)) 
                    letter = replacements[letter];

                sb.Append(letter);
            }

            return sb.ToString();
        }
    }
}
