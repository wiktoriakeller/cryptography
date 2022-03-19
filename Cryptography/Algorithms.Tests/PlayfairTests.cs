using Xunit;
using Algorithms.Playfair;

namespace Algorithms.Tests
{
    public class PlayfairTests
    {
        private readonly Playfair.Playfair algorithm;

        public PlayfairTests()
        {
            algorithm = new Playfair.Playfair(new KeyTable());
        }

        [Theory]
        [InlineData("CC", "playfair example", "GRGR")]
        [InlineData("Playfair. is a cryptographic algorithm", "plAyfair example", "LAYFPYRE. MK L DXLIPQDELFBRB YADQERZBIM")]
        [InlineData("The roots of a tree are usually under the ground. However, this is not always true. The roots of the mangrove tree are often under water.", 
            "Tree", 
            "BCA EQVVCU PG E REFEA BEA OUSBQEMX ZUFRE RFB DAPOKH. CUXRXTE, RCNO MU MVC EMYRAY REQB. BCA EQVVCU PC EFB SGMHTPXT REFEA BEA QCRAU ZKHAE YRRAEW.")]
        public void TestEncryption(string plaintext, string key, string cipher)
        {
            algorithm.GenerateKeyTable(key);
            algorithm.LeaveOnlyLetters(false);
            Assert.Equal(cipher, algorithm.Encipher(plaintext));
        }

        [Theory]
        [InlineData("GRGR", "playfair example", "CXCX")]
        [InlineData("LAYFPYRE. MK L DXLIPQDELFBRB YADQERZBIM", "plAyfair example", "PLAYFAIR. IS A CRYPTOGRAPHIC ALGORITHMX")]
        [InlineData("BCA EQVVCU PG E REFEA BEA OUSBQEMX ZUFRE RFB DAPOKH. CUXRXTE, RCNO MU MVC EMYRAY REQB. BCA EQVVCU PC EFB SGMHTPXT REFEA BEA QCRAU ZKHAE YRRAEW.",
        "Tree",
        "THE ROXOTS OF A TREXE ARE USUALXLY UNDER THE GROUND. HOWEVER, THIS IS NOT ALWAYS TRUE. THE ROXOTS OF THE MANGROVE TREXE ARE OFTEN UNDER WATERX.")]
        public void TestDecryption(string cipher, string key, string text)
        {
            algorithm.GenerateKeyTable(key);
            algorithm.LeaveOnlyLetters(false);
            Assert.Equal(text, algorithm.Decipher(cipher));
        }
    }
}