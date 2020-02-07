using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game.Solver
{
    interface IGameWatcher
    {
        void HandleMove(Move move);
    }
}
