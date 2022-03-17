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
            int lastLetterIndex = 0;

            for (int i = 0; i < plaintext.Length; i++)
            {
                if (!char.IsLetter(plaintext[i]) && !leaveOnlyLetters)
                {
                    newText.Append(plaintext[i]);
                }
                else if (char.IsLetter(plaintext[i]))
                {
                    lastLetterIndex = i;
                    var letter = plaintext[i];
                    if(lettersToReplace.ContainsKey(letter))
                        letter = lettersToReplace[letter];

                    if(letter == lastLetter)
                    {
                        var letterToAppend = extraLetter;
                        if(letter == letterToAppend)
                            letterToAppend = secondExtraLetter;

                        newText.Append(letterToAppend);
                        length++;
                    }

                    newText.Append(letter);
                    lastLetter = letter;
                    length++;
                }
            }
            
            if(length % 2 != 0)
            {
                var index = lastLetterIndex + 1;
                if (leaveOnlyLetters)
                    index = newText.Length;

                var letter = newText[index - 1];
                var letterToAppend = extraLetter;

                if (letter == letterToAppend)
                    letterToAppend = secondExtraLetter;

                newText.Insert(index, letterToAppend);
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
