﻿using System;

namespace Algorithms.Playfair
{
    public struct Position
    {
        public int Row { get; }
        public int Col { get; }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
