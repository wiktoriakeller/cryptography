using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public struct Postition
    {
        public int Row { get; }
        public int Col { get; }

        public Postition(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
