using System.Text;

namespace Algorithms.Playfair
{
    public class Playfair : IEncipher, IDecipher
    {
        private IKeyTable keyTable;
        private readonly IDictionary<char, char> lettersToReplace = new Dictionary<char, char>() { { 'J', 'I' } };
        private readonly HashSet<char> lettersToDiscard = new HashSet<char>() { ' ', '.', ',', '!', '(', ')', '?', '-', ':' };
        private bool leaveOnlyLetters;

        public Playfair(IKeyTable table)
        {
            leaveOnlyLetters = true;
            keyTable = table;
        }

        public void LeaveOnlyLetters(bool leaveOnlyLetters)
        {
            this.leaveOnlyLetters = leaveOnlyLetters;
        }

        public void GenerateKeyTable(string key)
        {
            keyTable.GenerateKeyTable(key);
        }

        public string Encipher(string plaintext)
        {
            plaintext = PreparePlaintext(plaintext.ToUpper());
            return Decode(plaintext, 1);
        }

        public string Decipher(string cipher)
        {
            return Decode(cipher.ToUpper(), -1);
        }

        private string PreparePlaintext(string text)
        {
            var newText = new StringBuilder(text.Length);
            char extraLetter = 'X';
            int lastLetterIndex = 0;
            int length = 0;

            for(int i = 0; i < text.Length; i++)
            {
                if(lettersToDiscard.Contains(text[i]))
                {
                    if(!leaveOnlyLetters)
                        newText.Append(text[i]);
                }
                else if(lettersToReplace.ContainsKey(text[i]))
                {
                    newText.Append(lettersToReplace[text[i]]);
                    length++;
                }
                else if(char.IsLetter(text[i]))
                {
                    newText.Append(text[i]);
                    lastLetterIndex = i;
                    length++;
                }
            }

            if (length % 2 != 0)
                newText.Insert(lastLetterIndex + 1, extraLetter);

            return newText.ToString();
        }

        private string Decode(string text, int shift)
        {
            var decoded = new StringBuilder();
            var tmpSb = new StringBuilder();
            int add = 2;
            int firstIndex = -1;
            int secondIndex = -1;

            if (!leaveOnlyLetters)
                add = 1;

            for (int i = 0; i < text.Length; i += add)
            {
                if(!leaveOnlyLetters)
                {
                    if (lettersToDiscard.Contains(text[i]))
                    {
                        if (firstIndex != -1)
                            tmpSb.Append(text[i]);
                        else
                            decoded.Append(text[i]);

                        continue;
                    }

                    if(firstIndex == -1)
                    {
                        firstIndex = i;
                        continue;
                    }
                    else if(secondIndex == -1)
                    {
                        secondIndex = i;
                    }
                }
                else
                {
                    firstIndex = i;
                    secondIndex = i + 1;
                }

                var firstPos = keyTable.GetPosition(text[firstIndex]);
                var secondPos = keyTable.GetPosition(text[secondIndex]);
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

                if(tmpSb.Length > 0)
                {
                    decoded.Append(tmpSb);
                    tmpSb.Clear();
                }

                decoded.Append(secondEncrypted);

                firstIndex = -1;
                secondIndex = -1;
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
