using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    class Row : BlockBase, IBlocks
    {
        public int Pos { get { return BlockRange.StartPos.Y; } }

        public Row(int position) : base(new Range(new Position(0, position), new Position(Game.NumColumns, position + 1)), Game.NumColumns)
        {
        }

        protected override void SetCellImpl(Cell cell)
        {
            mCells[cell.Position.X] = cell;
        }
    }
}
