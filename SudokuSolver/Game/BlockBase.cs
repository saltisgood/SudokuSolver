using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    abstract class BlockBase
    {
        public IReadOnlyCollection<Cell> Cells
        {
            get
            {
                return mCells;
            }
        }
        protected readonly Cell[] mCells;

        public IReadOnlyRange BlockRange
        {
            get
            {
                return mBlockRange;
            }
        }
        private readonly IReadOnlyRange mBlockRange;

        public bool IsComplete
        {
            get
            {
                return IsCompleteImpl();
            }
        }
        public bool IsOk
        {
            get
            {
                return IsOkImpl();
            }
        }

        private bool mIntersectedDirty = true;
        public IReadOnlyCollection<Row> IntersectedRows
        {
            get
            {
                RefreshIntersected();
                return mIntersectedRows;
            }
        }
        private List<Row> mIntersectedRows;
        public IReadOnlyCollection<Column> IntersectedColumns
        {
            get
            {
                RefreshIntersected();
                return mIntersectedColumns;
            }
        }
        private List<Column> mIntersectedColumns;
        public IReadOnlyCollection<Block> IntersectedBlocks
        {
            get
            {
                RefreshIntersected();
                return mIntersectedBlocks;
            }
        }
        private List<Block> mIntersectedBlocks;

        protected BlockBase(Range range, int cellCount)
        {
            mBlockRange = range;
            mCells = new Cell[cellCount];
        }

        public bool Contains(int value)
        {
            foreach (var cell in mCells)
            {
                if (cell.Value == value)
                    return true;
            }
            return false;
        }

        public void SetCell(Cell cell)
        {
            Debug.Assert(BlockRange.Contains(cell.Position));
            mIntersectedDirty = true;
            SetCellImpl(cell);
        }

        protected abstract void SetCellImpl(Cell cell);

        private static readonly HashSet<int> ReferenceSet = new HashSet<int>(Enumerable.Range(1, Game.MaxNum));
        private bool IsCompleteImpl()
        {
            HashSet<int> nums = new HashSet<int>();
            foreach (var cell in mCells)
            {
                // Doesn't have a value
                if (cell.Value == 0)
                {
                    return false;
                }
                // The value is already in there
                else if (!nums.Add(cell.Value))
                {
                    return false;
                }
            }

            return nums.SetEquals(ReferenceSet);
        }

        private bool IsOkImpl()
        {
            HashSet<int> nums = new HashSet<int>();
            foreach (var cell in mCells)
            {
                if (cell.Value != 0 && !nums.Add(cell.Value))
                {
                    return false;
                }
            }

            return true;
        }

        private void RefreshIntersected()
        {
            if (!mIntersectedDirty)
                return;
            var rows = new HashSet<Row>();
            var cols = new HashSet<Column>();
            var blocks = new HashSet<Block>();
            foreach (var cell in mCells)
            {
                if (cell != null)
                {
                    rows.Add(cell.ParentRow);
                    cols.Add(cell.ParentColumn);
                    blocks.Add(cell.ParentBlock);
                }
            }

            mIntersectedRows = new List<Row>(rows);
            mIntersectedColumns = new List<Column>(cols);
            mIntersectedBlocks = new List<Block>(blocks);
            mIntersectedDirty = false;
        }
    }
}
