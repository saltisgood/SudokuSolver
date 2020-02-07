using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game.Solver
{
    /// <summary>
    /// If there are N-1 filled out cells within a range, this will fill out the final one
    /// </summary>
    class FinaliserAlgo : IAlgo
    {
        private readonly Game mGame;

        public FinaliserAlgo(Game game)
        {
            mGame = game;
        }

        public bool Run(out List<Move> moves)
        {
            moves = new List<Move>();
            return FinishBlocks(moves);
        }

        private static readonly HashSet<int> ReferenceSet = new HashSet<int>(Enumerable.Range(1, Game.MaxNum));
        private bool FinishBlocks(List<Move> moves)
        {
            bool foundAnything = false;
            foreach (var block in mGame.Blocks)
            {
                Cell emptyCell = null;
                bool failed = false;
                var values = new HashSet<int>();
                foreach (var cell in block.Cells)
                {
                    if (cell.HasValue)
                    {
                        values.Add(cell.Value);
                        continue;
                    }

                    if (emptyCell == null)
                    {
                        emptyCell = cell;
                        continue;
                    }

                    failed = true;
                    break;
                }

                if (failed || emptyCell == null)
                    continue;

                foundAnything = true;
                var lastValue = ReferenceSet.Except(values);
#if DEBUG
                bool only1 = false;
#endif
                foreach (var value in lastValue)
                {
#if DEBUG
                    Debug.Assert(!only1);
                    only1 = true;
#endif

                    moves.Add(new Move(emptyCell, value));
                }

#if DEBUG
                Debug.Assert(only1);
#endif
            }

            return foundAnything;
        }
    }
}
