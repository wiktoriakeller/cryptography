using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Playfair : IEncipher, IDecipher
    {
        public const int KeyTableSize = 5;
        public char[,] KeyTable { get { return (char[,])keyTable.Clone(); } }
        private char[,] keyTable;
        private IDuplicateRemover remover;

        public Playfair(IDuplicateRemover remover)
        {
            this.remover = remover;
            keyTable = new char[KeyTableSize, KeyTableSize];
        }

        public void GenerateKeyTable(string key)
        {
            key = remover.RemoveDuplicates(key.ToLower().Replace("j", "i"));
            char[,] table = new char[KeyTableSize, KeyTableSize];
            IDictionary<char, int> exists = new Dictionary<char, int>();
            var sb = new StringBuilder(key.Length);

            for (int i = 0; i < key.Length; i++)
            {
                if(key[i] != ' ' && char.IsLetter(key[i]))
                {
                   sb.Append(key[i]);
                   exists[key[i]] = 1;
                }
            }

            for(char c = 'a'; c <= 'z'; c++)
            {
                if(!exists.ContainsKey(c) && c != 'j')
                {
                    sb.Append(c);
                    exists[c] = 1;
                }
            }

            int index = 0;
            for (int i = 0; i < KeyTableSize; i++)
            {
                for (int j = 0; j < KeyTableSize; j++)
                {
                    table[i, j] = sb[index];
                    index++;
                }
            }

            keyTable = table;
        }

        public string ConvertToPairs(string text)
        {
            var sb = new StringBuilder(text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                if (i % 2 == 0 || (i % 2 != 0 && text[i - 1] != text[i]))
                {
                    sb.Append(text[i]);
                }
                else
                {
                    char extraLetter = 'x';
                    if (text[i - 1] == 'x')
                        extraLetter = 'z';

                    sb.Append(extraLetter);
                    sb.Append(text[i]);
                }
            }

            if(sb.Length % 2 != 0)
            {
                char extraLetter = 'x';
                if (sb[sb.Length - 1] == 'x')
                    extraLetter = 'z';

                sb.Append(extraLetter);
            }

            return sb.ToString();
        }

        public string Encipher(string plaintext)
        {
            plaintext = plaintext.ToLower().Replace(" ", "");
            var cipher = new StringBuilder(ConvertToPairs(plaintext));
            return cipher.ToString();
        }

        public string Decipher(string cipher)
        {
            throw new NotImplementedException();
        }
    }
}
