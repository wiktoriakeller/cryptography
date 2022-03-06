using System.Text;

namespace Algorithms
{
    public class DuplicateRemover : IDuplicateRemover
    {
        public string RemoveDuplicates(string text)
        {
            IDictionary<char, int> exists = new Dictionary<char, int>();
            var sb = new StringBuilder(text.Length);

            for(int i = 0; i < text.Length; i++)
            {
                if(!exists.ContainsKey(text[i]))
                {
                    sb.Append(text[i]);
                    exists[text[i]] = 1;
                }
            }

            return sb.ToString();
        }
    }
}
