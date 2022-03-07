using System;
using System.Text;

namespace Algorithms
{
    public class KeyTable : IKeyTable
    {
        public int KeyTableSize { get; } = 5;
        public char[,] Table { get { return (char[,])table.Clone(); } }
        private char[,] table;
        private IDictionary<char, Position> letterPositions;

        public KeyTable()
        {
            letterPositions = new Dictionary<char, Position>();
            table = new char[KeyTableSize, KeyTableSize];
        }

        public Position GetPosition(char c)
        {
            return letterPositions[c];
        }

        public char GetLetter(int row, int col)
        {
            return table[row, col];
        }

        public void GenerateKeyTable(string key)
        {
            IDictionary<char, int> exists = new Dictionary<char, int>();
            char[,] table = new char[KeyTableSize, KeyTableSize];
            var sb = new StringBuilder(key.Length);
            char letter;

            for (int i = 0; i < key.Length; i++)
            {
                letter = char.ToUpper(key[i]);
                if (letter == 'J')
                    letter = 'I';

                if (!exists.ContainsKey(letter) && letter != ' ' && char.IsLetter(letter))
                {
                    sb.Append(letter);
                    exists[letter] = 1;
                }
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (!exists.ContainsKey(c) && c != 'J')
                {
                    sb.Append(c);
                    exists[c] = 1;
                }
            }

            int index = 0;
            letterPositions = new Dictionary<char, Position>();
            for (int i = 0; i < KeyTableSize; i++)
            {
                for (int j = 0; j < KeyTableSize; j++)
                {
                    table[i, j] = sb[index];
                    letterPositions[sb[index]] = new Position(i, j);
                    index++;
                }
            }

            this.table = table;
        }
    }
}
