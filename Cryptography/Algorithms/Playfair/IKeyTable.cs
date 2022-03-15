namespace Algorithms.Playfair
{
    public interface IKeyTable
    {
        public int KeyTableCols { get; }
        public int KeyTableRows { get; }
        public char[,] Table { get; }
        public void GenerateKeyTable(string key);
        public Position GetPosition(char c);
        public char GetLetter(int row, int col);
        public IDictionary<string, string> GetLettersReplacements();
    }
}