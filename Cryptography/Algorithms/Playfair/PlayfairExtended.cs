using System;

namespace Algorithms.Playfair
{
    public class PlayfairExtended : PlayfairBase
    {
        public PlayfairExtended(IKeyTable table) : base(table)
        {
            lettersToDiscard = new HashSet<char> { '(', ')', ':', ';' };
        }

        protected override string Decode(string text, int shif)
        {
            throw new NotImplementedException();
        }

        protected override string PreparePlaintext(string plaintext)
        {
            throw new NotImplementedException();
        }
    }
}
