using System;
using System.Text;

namespace Algorithms.Playfair
{
    public class PlayfairExtended : PlayfairBase
    {
        public PlayfairExtended(IKeyTable table) : base(table) { }

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
            var clearedText = new StringBuilder(plaintext.Length);
            var preparedText = new StringBuilder(plaintext.Length);
            char extraLetter = 'X';
            char secondExtraLetter = 'Z';
            int length = 0;

            for(int i = 0; i < plaintext.Length; i++)
            {
                if (keyTable.LetterExist(plaintext[i]))
                {
                    var letterToAdd = plaintext[i];
                    if(lettersToReplace.ContainsKey(plaintext[i]))
                        letterToAdd = lettersToReplace[letterToAdd];

                    clearedText.Append(letterToAdd);
                }
            }

            int index = 1;
            if(clearedText.Length == 1)
                preparedText.Append(clearedText[0]);

            while(index < clearedText.Length)
            {
                preparedText.Append(clearedText[index - 1]);

                if (clearedText[index] == clearedText[index - 1])
                {
                    var letterToAppend = extraLetter;
                    if (clearedText[index] == letterToAppend)
                        letterToAppend = secondExtraLetter;

                    preparedText.Append(letterToAppend);
                    index += 1;
                }
                else
                {
                    preparedText.Append(clearedText[index]);
                    index += 2;
                }

                length += 2;
            }

            if(clearedText.Length % 2 != 0)
            {
                preparedText.Append(clearedText[^1]);
                length++;
            }

            if (length % 2 != 0)
            {
                var letter = preparedText[^1];
                var letterToAppend = extraLetter;

                if (letter == letterToAppend)
                    letterToAppend = secondExtraLetter;

                preparedText.Append(letterToAppend);
            }

            return preparedText.ToString();
        }
    }
}
