using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Playfair : IEncipher, IDecipher
    {
        const int KeyTableSize = 5;
        public char[,] KeyTable { get; private set; }
        private IDuplicateRemover remover;

        public Playfair(IDuplicateRemover remover)
        {
            this.remover = remover;
        }

        public void GenerateKeyTable(string key)
        {
            key = remover.RemoveDuplicates(key.ToLower().Replace("j", "i"));
            char[,] table = new char[KeyTableSize, KeyTableSize];
            var sb = new StringBuilder(key.Length);
            IDictionary<char, int> exists = new Dictionary<char, int>();

            for(int i = 0; i < key.Length; i++)
            {
                if(key[i] != ' ' && Char.IsLetter(key[i]))
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

            KeyTable = table;
        }

        public string Encipher(string plaintext)
        {
            throw new NotImplementedException();
        }

        public string Decipher(string cipher)
        {
            throw new NotImplementedException();
        }
    }
}
