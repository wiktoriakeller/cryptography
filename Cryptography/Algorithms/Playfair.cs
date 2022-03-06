﻿using System;
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
            var cipher = new StringBuilder();
            
            for(int i = 0; i < pairs.Length; i += 2)
            {
                var firstPos = letterPositions[pairs[i]];
                var secondPos = letterPositions[pairs[i + 1]];
                char secondCiphered;
                char firstCiphered;

                if (firstPos.Row == secondPos.Row)
                {
                    int firstCol = (firstPos.Col + 1) % KeyTableSize;
                    int secondCol = (secondPos.Col + 1) % KeyTableSize;

                    firstCiphered = keyTable[firstPos.Row, firstCol];
                    secondCiphered = keyTable[firstPos.Row, secondCol];
                }
                else if (firstPos.Col == secondPos.Col)
                {
                    int firstRow = (firstPos.Row + 1) % KeyTableSize;
                    int secondRow = (secondPos.Row + 1) % KeyTableSize;

                    firstCiphered = keyTable[firstRow, firstPos.Col];
                    secondCiphered = keyTable[secondRow, firstPos.Col];
                }
                else
                {
                    int firstCol = secondPos.Col;
                    int secondCol = firstPos.Col;
                    firstCiphered = keyTable[firstPos.Row, firstCol];
                    secondCiphered = keyTable[secondPos.Row, secondCol];
                }

                cipher.Append(firstCiphered);
                cipher.Append(secondCiphered);
            }

            return cipher.ToString();
        }

        public string Decipher(string cipher)
        {
            var encrypted = new StringBuilder();

            for(int i = 0; i < cipher.Length; i += 2)
            {
                var firstPos = letterPositions[cipher[i]];
                var secondPos = letterPositions[cipher[i + 1]];
                char firstEncrypted;
                char secondEncrypted;

                if (firstPos.Row == secondPos.Row)
                {
                    int firstCol = ModuloTableSize(firstPos.Col - 1);
                    int secondCol = ModuloTableSize(secondPos.Col - 1);

                    firstEncrypted = keyTable[firstPos.Row, firstCol];
                    secondEncrypted = keyTable[firstPos.Row, secondCol];
                }
                else if (firstPos.Col == secondPos.Col)
                {
                    int firstRow = ModuloTableSize(firstPos.Row - 1);
                    int secondRow = ModuloTableSize(secondPos.Row - 1);

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

                encrypted.Append(firstEncrypted);
                encrypted.Append(secondEncrypted);
            }

            return encrypted.ToString();
        }

        private int ModuloTableSize(int number)
        {
            if (number < 0)
                number += KeyTableSize;

            return number % KeyTableSize;
        }
    }
}
