using System;
using System.Text;

namespace Algorithms
{
    public class KeyTable : IKeyTable
    {
        public int KeyTableCols { get; } = 5;
        public int KeyTableRows { get; } = 5;
        public char[,] Table { get { return (char[,])table.Clone(); } }
        
        private char[,] table;
        private IDictionary<char, Position> letterPositions;
        private readonly IDictionary<string, string> replacements;

        public KeyTable()
        {
            table = new char[KeyTableRows, KeyTableCols];
            letterPositions = new Dictionary<char, Position>();
            replacements = new Dictionary<string, string>() { { " ", "" }, { "J", "I" }, { ".", "" }, { ",", "" }, { "!", "" }, { "(", "" }, { ")", "" } };
        }

        public Position GetPosition(char c)
        {
            return letterPositions[c];
        }

        public char GetLetter(int row, int col)
        {
            return table[row, col];
        }

        public IDictionary<string, string> GetLettersReplacements()
        {
            return replacements;
        }

        public void GenerateKeyTable(string key)
        {
            IDictionary<char, int> exists = new Dictionary<char, int>();
            char[,] table = new char[KeyTableRows, KeyTableCols];
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
