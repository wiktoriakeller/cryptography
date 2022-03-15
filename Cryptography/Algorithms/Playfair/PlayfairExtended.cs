using System;
using System.Text;

namespace Algorithms.Playfair
{
    public class PlayfairExtended : PlayfairBase
    {
        public PlayfairExtended(IKeyTable table) : base(table)
        {
            lettersToDiscard = new HashSet<char> { '(', ')', ':', ';' };
        }

        protected override string Decode(string text, int shift)
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
                    int firstCol = Modulo(firstPos.Col + shift, false);
                    int secondCol = Modulo(secondPos.Col + shift, false);

                    firstEncrypted = keyTable.GetLetter(firstPos.Row, firstCol);
                    secondEncrypted = keyTable.GetLetter(firstPos.Row, secondCol);
                }
                else if (firstPos.Col == secondPos.Col)
                {
                    int firstRow = Modulo(firstPos.Row + shift, true);
                    int secondRow = Modulo(secondPos.Row + shift, true);

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

        protected override string PreparePlaintext(string plaintext)
        {
            var newText = new StringBuilder(plaintext.Length);
            char extraLetter = 'X';
            int lastLetterIndex = 0;
            int length = 0;

            for (int i = 0; i < plaintext.Length; i++)
            {
                if (lettersToDiscard.Contains(plaintext[i]))
                {
                    continue;
                }
                else if (lettersToReplace.ContainsKey(plaintext[i]))
                {
                    newText.Append(lettersToReplace[plaintext[i]]);
                    length++;
                }
                else
                {
                    newText.Append(plaintext[i]);
                    lastLetterIndex = i;
                    length++;
                }
            }

            if (length % 2 != 0)
                newText.Insert(lastLetterIndex + 1, extraLetter);

            return newText.ToString();
        }
    }
}
