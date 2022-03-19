using Xunit;
using Algorithms.Playfair;

namespace Algorithms.Tests
{
    public class PlayfairExtendedTests
    {
        private readonly PlayfairExtended algorithm;

        public PlayfairExtendedTests()
        {
            algorithm = new PlayfairExtended(new KeyTableExtended());
        }

        [Theory]
        [InlineData("Like most classical ciphers, the Playfair cipher can be easily cracked if there is enough text.",
        "tree",
        "MKLR-IPUC0FIGYOMGTI,IOUDAEQ-0CFB.OMEXGTMT.IOUDAE0IBM!TT,ABOMMX0IEBDIRF0OC,BCAET,MO,TIUSHC!RAVE,W")]
        public void TestEncryption(string plaintext, string key, string cipher)
        {
            algorithm.GenerateKeyTable(key);
            Assert.Equal(cipher, algorithm.Encipher(plaintext));
        }

        [Theory]
        [InlineData("MKLR-IPUC0FIGYOMGTI,IOUDAEQ-0CFB.OMEXGTMT.IOUDAE0IBM!TT,ABOMMX0IEBDIRF0OC,BCAET,MO,TIUSHC!RAVE,W",
            "tree",
            "LIKE MOST CLASSICAL CIPHERS, THE PLAYFAIR CIPHER CAN BE EASILY CRACKED IF THERE IS ENOUGH TEXT.X")]
        public void TestDecryption(string cipher, string key, string text)
        {
            algorithm.GenerateKeyTable(key);
            Assert.Equal(text, algorithm.Decipher(cipher));
        }
    }
}
