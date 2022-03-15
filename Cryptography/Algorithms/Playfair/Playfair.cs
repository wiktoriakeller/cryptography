using System.Text;

namespace Algorithms.Playfair
{
    public class Playfair : PlayfairBase
    {
        private bool leaveOnlyLetters;

        public Playfair(IKeyTable table) : base(table)
        {
            leaveOnlyLetters = true;
            lettersToDiscard = new HashSet<char>() { ' ', '.', ',', '!', '(', ')', '?', '-', ':', ';' };
        }

        public void LeaveOnlyLetters(bool leaveOnlyLetters)
        {
            this.leaveOnlyLetters = leaveOnlyLetters;
        }

        protected override string PreparePlaintext(string plaintext)
        {
            var newText = new StringBuilder(plaintext.Length);
            char extraLetter = 'X';
            int lastLetterIndex = 0;
            int length = 0;

            for(int i = 0; i < plaintext.Length; i++)
            {
                if(lettersToDiscard.Contains(plaintext[i]))
                {
                    if(!leaveOnlyLetters)
                        newText.Append(plaintext[i]);
                }
                else if(lettersToReplace.ContainsKey(plaintext[i]))
                {
                    newText.Append(lettersToReplace[plaintext[i]]);
                    length++;
                }
                else if(char.IsLetter(plaintext[i]))
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

        protected override string Decode(string text, int shift)
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
    }
}
