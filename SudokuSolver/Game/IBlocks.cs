using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    interface IBlocks
    {
        IReadOnlyRange BlockRange { get; }
        IReadOnlyCollection<Cell> Cells { get; }
        bool IsComplete { get; }
        bool IsOk { get; }
        bool Contains(int value);
    }
}
