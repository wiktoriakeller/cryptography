using System;

namespace Algorithms.Playfair
{
    public abstract class PlayfairBase : IEncipher, IDecipher, IGenerateKeyTable
    {
        protected IDictionary<char, char> lettersToReplace;
        protected IKeyTable keyTable;

        public PlayfairBase(IKeyTable table)
        {
            keyTable = table;
            lettersToReplace = new Dictionary<char, char>() { { 'J', 'I' } };
        }

        public string Encipher(string plaintext)
        {
            plaintext = PreparePlaintext(plaintext.ToUpper().Trim());
            return Decode(plaintext, 1);
        }

        public string Decipher(string cipher)
        {
            return Decode(cipher.ToUpper().Trim(), -1);
        }

        public void GenerateKeyTable(string key)
        {
            keyTable.GenerateKeyTable(key);
        }

        protected abstract string PreparePlaintext(string plaintext);

        protected abstract string Decode(string text, int shift);

        protected int Modulo(int number, bool row = false)
        {
            int size = keyTable.KeyTableRows;
            if (!row)
                size = keyTable.KeyTableCols;

            if (number < 0)
                number += size;

            return number % size;
        }
    }
}
