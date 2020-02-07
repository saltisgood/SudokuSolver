using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game.Solver
{
    /// <summary>
    /// Look at each number value individually and check if more of the same can be added in other rows/cols
    /// </summary>
    class NumberWiseAlgo : IAlgo
    {
        private readonly Game mGame;

        public NumberWiseAlgo(Game game)
        {
            mGame = game;
        }

        public bool Run(out List<Move> moves)
        {
            moves = new List<Move>();
            bool foundSomething = false;
            for (int i = 1; i < Game.MaxNum + 1; ++i)
            {
                foundSomething |= RunImpl(moves, i);
            }
            return foundSomething;
        }

        private bool RunImpl(List<Move> moves, int value)
        {
            var possibleBlocks = new List<Block>();
            foreach (var block in mGame.Blocks)
            {
                if (!block.IsComplete && !block.Contains(value))
                {
                    possibleBlocks.Add(block);
                }
            }

            bool foundSomething = false;
            foreach (var block in possibleBlocks)
            {
                foundSomething |= CheckBlock(moves, value, block);
            }

            return foundSomething;
        }

        private bool CheckBlock(List<Move> moves, int value, Block block)
        {
            Cell targetCell = null;
            foreach (var cell in block.Cells)
            {
                if (!cell.HasValue && !cell.ParentColumn.Contains(value) && !cell.ParentRow.Contains(value))
                {
                    if (targetCell == null)
                    {
                        targetCell = cell;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (targetCell != null)
            {
                moves.Add(new Move(targetCell, value));
                return true;
            }

            return false;
        }

        public override String ToString()
        {
            return "NumberWiseAlgo";
        }
    }
}
