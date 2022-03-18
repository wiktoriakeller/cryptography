using System.Text;

namespace Algorithms.Playfair
{
    public class Playfair : PlayfairBase
    {
        private bool leaveOnlyLetters;

        public Playfair(IKeyTable table) : base(table)
        {
            leaveOnlyLetters = true;
        }

        public void LeaveOnlyLetters(bool leaveOnlyLetters)
        {
            this.leaveOnlyLetters = leaveOnlyLetters;
        }

        protected override string PreparePlaintext(string plaintext)
        {
            var newText = new StringBuilder(plaintext.Length);
            
            char extraLetter = 'X';
            char secondExtraLetter = 'Z';
            char lastLetter = ' ';

            int length = 0;
            int lastLetterIndex = -1;
            int firstLetterIndex = -1;
            int secondLetterIndex = -1;

            int index = 0;
            var nonLetterCharacters = new StringBuilder();
            while(index < plaintext.Length)
            {
                if(!char.IsLetter(plaintext[index]) && !leaveOnlyLetters)
                {
                    if(firstLetterIndex != -1)
                        nonLetterCharacters.Append(plaintext[index]);
                    else
                        newText.Append(plaintext[index]);
                }
                else if(char.IsLetter(plaintext[index]))
                {
                    lastLetterIndex = newText.Length;
                    lastLetter = plaintext[index];
                    if(firstLetterIndex == -1)
                    {
                        firstLetterIndex = index;
                        index++;
                        continue;
                    }

                    if(secondLetterIndex == -1)
                        secondLetterIndex = index;

                    newText.Append(plaintext[firstLetterIndex]);
                    if (nonLetterCharacters.Length > 0)
                    {
                        newText.Append(nonLetterCharacters);
                        nonLetterCharacters.Clear();
                    }

                    if (plaintext[firstLetterIndex] == plaintext[secondLetterIndex])
                    {
                        var letterToAppend = extraLetter;
                        if (plaintext[firstLetterIndex] == letterToAppend)
                            letterToAppend = secondExtraLetter;

                        newText.Append(letterToAppend);
                        firstLetterIndex = secondLetterIndex;
                        secondLetterIndex = -1;
                    }
                    else
                    {
                        newText.Append(plaintext[secondLetterIndex]);
                        firstLetterIndex = -1;
                        secondLetterIndex = -1;
                    }

                    length += 2;
                }

                index++;
            }

            if(firstLetterIndex != -1)
            {
                newText.Insert(lastLetterIndex, lastLetter);

                if(nonLetterCharacters.Length > 0)
                    newText.Append(nonLetterCharacters);

                lastLetterIndex++;
                length++;
            }

            if (length % 2 != 0)
            {
                var indexToAppend = lastLetterIndex;
                if (leaveOnlyLetters)
                    indexToAppend = newText.Length;

                var letter = newText[indexToAppend - 1];
                var letterToAppend = extraLetter;

                if (letter == letterToAppend)
                    letterToAppend = secondExtraLetter;

                newText.Insert(indexToAppend, letterToAppend);
            }

            return newText.ToString();
        }

        protected override string Decode(string text, int shift)
        {
            var decoded = new StringBuilder();
            var nonLetterCharacters = new StringBuilder();
            int add = 2;
            int firstIndex = -1;
            int secondIndex = -1;

            if (!leaveOnlyLetters)
                add = 1;

            for (int i = 0; i < text.Length; i += add)
            {
                if(!leaveOnlyLetters)
                {
                    if (!char.IsLetter(text[i]))
                    {
                        if (firstIndex != -1)
                            nonLetterCharacters.Append(text[i]);
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

                if(nonLetterCharacters.Length > 0)
                {
                    decoded.Append(nonLetterCharacters);
                    nonLetterCharacters.Clear();
                }

                decoded.Append(secondEncrypted);

                firstIndex = -1;
                secondIndex = -1;
            }

            return decoded.ToString();
        }
    }
}
