using System.Text;

namespace Algorithms
{
    public class Playfair : IEncipher, IDecipher
    {
        private IKeyTable keyTable { get; set; }

        public Playfair(IKeyTable table)
        {
            keyTable = table;
        }

        public void GenerateKeyTable(string key)
        {
            keyTable.GenerateKeyTable(key);
        }

        public string Encipher(string plaintext)
        {
            var pairs = ConvertToPairs(plaintext.ToUpper().ReplaceCharacters(keyTable.GetLettersReplacements()));
            return Decode(pairs, 1);
        }

        public string Decipher(string cipher)
        {
            return Decode(cipher.ToUpper(), -1);
        }

        private string ConvertToPairs(string text)
        {
            var pairs = new StringBuilder(text.Length);
            char extraLetter = 'X';

            for (int i = 0; i < text.Length; i++)
            {
                if (i % 2 == 0 || (i % 2 != 0 && text[i - 1] != text[i]))
                {
                    pairs.Append(text[i]);
                }
                else
                {
                    pairs.Append(extraLetter);
                    pairs.Append(text[i]);
                }
            }

            if (pairs.Length % 2 != 0)
                pairs.Append(extraLetter);

            return pairs.ToString();
        }

        private string Decode(string text, int shift)
        {
            var decoded = new StringBuilder();

            for (int i = 0; i < text.Length; i += 2)
            {
                var firstPos = keyTable.GetPosition(text[i]);
                var secondPos = keyTable.GetPosition(text[i + 1]);
                char firstEncrypted;
                char secondEncrypted;

                if (firstPos.Row == secondPos.Row)
                {
                    int firstCol = Modulo(firstPos.Col + shift);
                    int secondCol = Modulo(secondPos.Col + shift);

                    firstEncrypted = keyTable.GetLetter(firstPos.Row, firstCol);
                    secondEncrypted = keyTable.GetLetter(firstPos.Row, secondCol);
                }
                else if (firstPos.Col == secondPos.Col)
                {
                    int firstRow = Modulo(firstPos.Row + shift);
                    int secondRow = Modulo(secondPos.Row + shift);

                    firstEncrypted = keyTable.GetLetter(firstRow, firstPos.Col);
                    secondEncrypted = keyTable.GetLetter(secondRow, firstPos.Col);
                }
                else
                {
                    int firstCol = secondPos.Col;
                    int secondCol = firstPos.Col;
                    firstEncrypted = keyTable.GetLetter(firstPos.Row, firstCol);
                    secondEncrypted = keyTable.GetLetter(secondPos.Row, secondCol);
                }

                decoded.Append(firstEncrypted);
                decoded.Append(secondEncrypted);
            }

            return decoded.ToString();
        }

        private int Modulo(int number, bool row = false)
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
