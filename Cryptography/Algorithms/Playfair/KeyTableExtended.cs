using System;

namespace Algorithms.Playfair
{
    public class KeyTableExtended : KeyTable
    {
        public override int KeyTableRows { get; } = 8;

        public KeyTableExtended() : base()
        {
            extraLetters = new char[15] { ' ', '.', ',', '-', '!', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        }
    }
}
