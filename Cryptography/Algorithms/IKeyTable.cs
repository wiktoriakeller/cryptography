namespace Algorithms
{
    public interface IKeyTable
    {
        public int KeyTableSize { get; }
        public char[,] Table { get; }
        public Position GetPosition(char c);
        public char GetLetter(int row, int col);
        public void GenerateKeyTable(string key);
    }
}