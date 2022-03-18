using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public interface ICryptographyAlgorithm
    {
        public string Encipher(string plaintext);
        public string Decipher(string cipher);
    }
}
