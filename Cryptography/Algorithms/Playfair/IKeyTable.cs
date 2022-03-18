namespace Algorithms.Playfair
{
    public interface IKeyTable : IGenerateKeyTable
    {
        public int KeyTableCols { get; }
        public int KeyTableRows { get; }
        public char[,] Table { get; }
        public Position GetPosition(char c);
        public char GetLetter(int row, int col);
        public bool LetterExist(char letter);
    }
}