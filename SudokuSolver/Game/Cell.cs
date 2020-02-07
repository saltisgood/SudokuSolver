using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    class Cell
    {
        public int Value { get; set; }
        public bool HasValue
        {
            get
            {
                return Value != 0;
            }
        }
        private readonly List<int> mPossibilities = new List<int>();
        public IReadOnlyList<int> Possibilities
        {
            get { return mPossibilities; }
        }

        public IReadOnlyPosition Position
        {
            get { return mPosition; }
        }
        private readonly Position mPosition;

        public bool IsFixed
        {
            get; set;
        }

        public Row ParentRow
        {
            get; private set;
        }
        public Column ParentColumn
        {
            get; private set;
        }
        public Block ParentBlock
        {
            get; private set;
        }
        /// <summary>
        /// Whether the cell is possibly affected by a previous move
        /// </summary>
        public bool Dirty { get; set; }

        public Cell(Position p, Row parentRow, Column parentColumn, Block parentBlock)
        {
            mPosition = p;
            ParentRow = parentRow;
            ParentRow.SetCell(this);
            ParentColumn = parentColumn;
            ParentColumn.SetCell(this);
            ParentBlock = parentBlock;
            ParentBlock.SetCell(this);
        }

        public Cell(Position p, int fixedValue, Row parentRow, Column parentColumn, Block parentBlock) : this(p, parentRow, parentColumn, parentBlock)
        {
            IsFixed = true;
            Value = fixedValue;
        }

        public void Reset()
        {
            IsFixed = false;
            Value = 0;
            mPossibilities.Clear();
        }

        public ISet<Cell> GetInlineCellsWithinBlock()
        {
            ISet<Cell> cells = new HashSet<Cell>();
            foreach (var cell in ParentBlock.Cells)
            {
                if (cell != this && (cell.Position.X == Position.X || cell.Position.Y == Position.Y))
                {
                    cells.Add(cell);
                }
            }
            return cells;
        }

        public ISet<Cell> GetInlineCellsWithinColumn()
        {
            ISet<Cell> cells = new HashSet<Cell>();
            foreach (var cell in ParentColumn.Cells)
            {
                if (cell != this)
                {
                    cells.Add(cell);
                }
            }
            return cells;
        }

        public ISet<Cell> GetInlineCellsWithinRow()
        {
            ISet<Cell> cells = new HashSet<Cell>();
            foreach (var cell in ParentRow.Cells)
            {
                if (cell != this)
                {
                    cells.Add(cell);
                }
            }
            return cells;
        }
    }
}
