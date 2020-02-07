using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    class Block : BlockBase, IBlocks
    {
        public Block(Position pos) : base(new Range(pos, new Position(pos.X + Game.BlockWidth, pos.Y + Game.BlockHeight)), Game.NumBlockCells)
        {
        }

        protected override void SetCellImpl(Cell cell)
        {
            int rowDelta = cell.Position.Y - BlockRange.StartPos.Y;
            int colDelta = cell.Position.X - BlockRange.StartPos.X;
            mCells[colDelta + (rowDelta * Game.BlockWidth)] = cell;
        }
    }
}
