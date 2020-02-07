using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    class Position : IReadOnlyPosition
    {
        public int X { get; }

        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
