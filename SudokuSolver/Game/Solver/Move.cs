using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game.Solver
{
    class Move
    {
        public readonly Cell AffectedCell;
        public readonly int Value;

        public Move(Cell cell, int value)
        {
            AffectedCell = cell;
            Value = value;
        }
    }
}
