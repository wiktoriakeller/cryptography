using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class Playfair : IEncipher, IDecipher
    {
        public const int KeyTableSize = 5;
        public char[,] KeyTable { get { return (char[,])keyTable.Clone(); } }
        private char[,] keyTable;
        private IDuplicateRemover remover;
        private IDictionary<char, Postition> letterPositions;

        public Playfair(IDuplicateRemover remover)
        {
            this.remover = remover;
            keyTable = new char[KeyTableSize, KeyTableSize];
            letterPositions = new Dictionary<char, Postition>();
        }

        public void GenerateKeyTable(string key)
        {
            key = remover.RemoveDuplicates(key.ToUpper().Replace("J", "I"));
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

            for(char c = 'A'; c <= 'Z'; c++)
            {
                if(!exists.ContainsKey(c) && c != 'J')
                {
                    sb.Append(c);
                    exists[c] = 1;
                }
            }

            int index = 0;
            letterPositions = new Dictionary<char, Postition>();
            for (int i = 0; i < KeyTableSize; i++)
            {
                for (int j = 0; j < KeyTableSize; j++)
                {
                    table[i, j] = sb[index];
                    letterPositions[sb[index]] = new Postition(i, j);
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
                    char extraLetter = 'X';
                    if (text[i - 1] == 'X')
                        extraLetter = 'Z';

                    sb.Append(extraLetter);
                    sb.Append(text[i]);
                }
            }

            if(sb.Length % 2 != 0)
            {
                char extraLetter = 'X';
                if (sb[sb.Length - 1] == 'X')
                    extraLetter = 'Z';

                sb.Append(extraLetter);
            }

            return sb.ToString();
        }

        public string Encipher(string plaintext)
        {
            plaintext = plaintext.ToUpper().Replace(" ", "").Replace("J", "I");
            var pairs = ConvertToPairs(plaintext);
            return Decode(pairs, 1);
        }

        public string Decipher(string cipher)
        {
            return Decode(cipher, -1);
        }

        private string Decode(string text, int shift)
        {
            var decoded = new StringBuilder();

            for (int i = 0; i < text.Length; i += 2)
            {
                var firstPos = letterPositions[text[i]];
                var secondPos = letterPositions[text[i + 1]];
                char firstEncrypted;
                char secondEncrypted;

                if (firstPos.Row == secondPos.Row)
                {
                    int firstCol = Modulo(firstPos.Col + shift);
                    int secondCol = Modulo(secondPos.Col + shift);

                    firstEncrypted = keyTable[firstPos.Row, firstCol];
                    secondEncrypted = keyTable[firstPos.Row, secondCol];
                }
                else if (firstPos.Col == secondPos.Col)
                {
                    int firstRow = Modulo(firstPos.Row + shift);
                    int secondRow = Modulo(secondPos.Row + shift);

                    firstEncrypted = keyTable[firstRow, firstPos.Col];
                    secondEncrypted = keyTable[secondRow, firstPos.Col];
                }
                else
                {
                    int firstCol = secondPos.Col;
                    int secondCol = firstPos.Col;
                    firstEncrypted = keyTable[firstPos.Row, firstCol];
                    secondEncrypted = keyTable[secondPos.Row, secondCol];
                }

                decoded.Append(firstEncrypted);
                decoded.Append(secondEncrypted);
            }

            return decoded.ToString();
        }

        private int Modulo(int number)
        {
            if (number < 0)
                number += KeyTableSize;

            return number % KeyTableSize;
        }
    }
}
