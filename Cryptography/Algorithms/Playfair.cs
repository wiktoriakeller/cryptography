using System.Text;

namespace Algorithms
{
    public class Playfair : IEncipher, IDecipher
    {
        public IKeyTable KeyTable { get; set; }
        private readonly IDictionary<string, string> replacements;

        public Playfair(IKeyTable table)
        {
            KeyTable = table;
            replacements = new Dictionary<string, string>() { { " ", "" }, { "J", "I" }, { ".", "" }, { ",", "" }, { "!", "" }, {"(", "" }, {")", ""} };
        }

        public void GenerateKeyTable(string key)
        {
            KeyTable.GenerateKeyTable(key);
        }

        public string Encipher(string plaintext)
        {
            var pairs = ConvertToPairs(plaintext.ToUpper().ReplaceCharacters(replacements));
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
                var firstPos = KeyTable.GetPosition(text[i]);
                var secondPos = KeyTable.GetPosition(text[i + 1]);
                char firstEncrypted;
                char secondEncrypted;

                if (firstPos.Row == secondPos.Row)
                {
                    int firstCol = Modulo(firstPos.Col + shift);
                    int secondCol = Modulo(secondPos.Col + shift);

                    firstEncrypted = KeyTable.GetLetter(firstPos.Row, firstCol);
                    secondEncrypted = KeyTable.GetLetter(firstPos.Row, secondCol);
                }
                else if (firstPos.Col == secondPos.Col)
                {
                    int firstRow = Modulo(firstPos.Row + shift);
                    int secondRow = Modulo(secondPos.Row + shift);

                    firstEncrypted = KeyTable.GetLetter(firstRow, firstPos.Col);
                    secondEncrypted = KeyTable.GetLetter(secondRow, firstPos.Col);
                }
                else
                {
                    int firstCol = secondPos.Col;
                    int secondCol = firstPos.Col;
                    firstEncrypted = KeyTable.GetLetter(firstPos.Row, firstCol);
                    secondEncrypted = KeyTable.GetLetter(secondPos.Row, secondCol);
                }

                decoded.Append(firstEncrypted);
                decoded.Append(secondEncrypted);
            }

            return decoded.ToString();
        }

        private int Modulo(int number)
        {
            if (number < 0)
                number += KeyTable.KeyTableSize;

            return number % KeyTable.KeyTableSize;
        }
    }
}
