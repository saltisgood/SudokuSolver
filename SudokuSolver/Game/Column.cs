using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    class Column : BlockBase, IBlocks
    {
        public int Pos
        {
            get
            {
                return BlockRange.StartPos.X;
            }
        }

        public Column(int position) : base(new Range(new Position(position, 0), new Position(position + 1, Game.NumRows)), Game.NumRows)
        {
        }

        protected override void SetCellImpl(Cell cell)
        {
            mCells[cell.Position.Y] = cell;
        }
    }
}
