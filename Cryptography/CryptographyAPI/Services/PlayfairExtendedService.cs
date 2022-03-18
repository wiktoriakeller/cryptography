using CryptographyAPI.Models;
using Algorithms.Playfair;

namespace CryptographyAPI.Services
{
    public class PlayfairExtendedService : IAlgorithmService<PlayfairExtendedData>
    {
        private readonly PlayfairExtended playfair;
        public PlayfairExtendedService()
        {
            playfair = new PlayfairExtended(new KeyTableExtended());
        }

        public bool PrepareData(PlayfairExtendedData data)
        {
            if (data == null || data.Key == null || data.Text == null)
                return false;

            playfair.GenerateKeyTable(data.Key);

            return true;
        }

        public string Encipher(PlayfairExtendedData data)
        {
            return playfair.Encipher(data.Text);    
        }

        public string Decipher(PlayfairExtendedData data)
        {
            return playfair.Decipher(data.Text);
        }
    }
}
