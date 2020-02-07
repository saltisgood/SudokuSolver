using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    interface IReadOnlyPosition
    {
        int X { get; }
        int Y { get; }
    }
}
