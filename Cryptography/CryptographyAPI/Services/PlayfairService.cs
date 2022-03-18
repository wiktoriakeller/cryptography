using Algorithms.Playfair;
using CryptographyAPI.Models;

namespace CryptographyAPI.Services
{
    public class PlayfairService : IAlgorithmService<PlayfairData>
    {
        private readonly Playfair playfair;

        public PlayfairService()
        {
            playfair = new Playfair(new KeyTable());
        }

        public bool PrepareData(PlayfairData data)
        {
            if(data == null || data.Key == null || data.Text == null)
                return false;

            playfair.GenerateKeyTable(data.Key);
            playfair.LeaveOnlyLetters(data.LeaveOnlyLetters);

            return true;
        }

        public string Encipher(PlayfairData data)
        {
            return playfair.Encipher(data.Text);
        }

        public string Decipher(PlayfairData data)
        {
            return playfair.Decipher(data.Text);
        }
    }
}
