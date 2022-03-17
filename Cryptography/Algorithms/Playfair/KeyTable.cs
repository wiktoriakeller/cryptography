using System;
using System.Text;

namespace Algorithms.Playfair
{
    public class KeyTable : IKeyTable
    {
        public virtual int KeyTableCols { get; } = 5;
        public virtual int KeyTableRows { get; } = 5;
        public char[,] Table { get { return (char[,])table.Clone(); } }
        
        protected char[] extraLetters;
        private char[,] table;
        private IDictionary<char, Position> letterPositions;
        private HashSet<char> exists;

        public KeyTable()
        {
            table = new char[KeyTableRows, KeyTableCols];
            letterPositions = new Dictionary<char, Position>();
            exists = new HashSet<char>();
        }

        public Position GetPosition(char c)
        {
            return letterPositions[c];
        }

        public char GetLetter(int row, int col)
        {
            return table[row, col];
        }

        public bool LetterExist(char letter)
        {
            return exists.Contains(letter);
        }

        public void GenerateKeyTable(string key)
        {
            exists.Clear();
            char[,] table = new char[KeyTableRows, KeyTableCols];
            var sb = new StringBuilder(key.Length);
            char letter;

            for (int i = 0; i < key.Length; i++)
            {
                letter = char.ToUpper(key[i]);
                if (letter == 'J')
                    letter = 'I';

                if (!exists.Contains(letter) && char.IsLetter(letter))
                {
                    sb.Append(letter);
                    exists.Add(letter);
                }
            }

            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (!exists.Contains(c) && c != 'J')
                {
                    sb.Append(c);
                    exists.Add(c);
                }
            }

            if(extraLetters is not null)
            {
                for (int i = 0; i < extraLetters.Length; i++)
                {
                    if (!exists.Contains(extraLetters[i]))
                    {
                        sb.Append(extraLetters[i]);
                        exists.Add(extraLetters[i]);
                    }
                }
            }

            int index = 0;
            letterPositions = new Dictionary<char, Position>();
            for (int i = 0; i < KeyTableRows; i++)
            {
                for (int j = 0; j < KeyTableCols; j++)
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
