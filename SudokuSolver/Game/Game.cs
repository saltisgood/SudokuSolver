using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    class Game
    {
        public const int MaxNum = 9;
        public const int NumColumns = 9;
        public const int NumRows = 9;
        public const int NumBlocks = 9;
        public const int BlockWidth = 3;
        public const int BlockHeight = 3;
        public const int NumBlockCells = BlockWidth * BlockHeight;
        public const int NumCells = NumColumns * NumRows;
        public const int BlocksPerWidth = NumColumns / BlockWidth;
        public const int BlocksPerHeight = NumRows / BlockHeight;

        public static int GetPositionIndex(IReadOnlyPosition pos)
        {
            return pos.X + (pos.Y * NumColumns);
        }

        public static Position GetIndexPosition(int idx)
        {
            return new Position(idx % NumColumns, idx / NumColumns);
        }

        public IReadOnlyCollection<Row> Rows
        {
            get
            {
                return mRows;
            }
        }
        private readonly Row[] mRows = new Row[NumRows];

        public IReadOnlyCollection<Column> Columns
        {
            get
            {
                return mColumns;
            }
        }
        private readonly Column[] mColumns = new Column[NumColumns];

        public IReadOnlyCollection<Block> Blocks
        {
            get
            {
                return mBlocks;
            }
        }
        private readonly Block[] mBlocks = new Block[NumBlocks];

        private readonly IReadOnlyList<IBlocks> mAllBlocks;

        private readonly Cell[] mCells = new Cell[NumCells];

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

        public Game()
        {
            var allBlocks = new List<IBlocks>();

            for (int x = 0; x < NumColumns; ++x)
            {
                var row = new Row(x);
                mRows[x] = row;
                allBlocks.Add(row);
            }

            for (int y = 0; y < NumRows; ++y)
            {
                var col = new Column(y);
                mColumns[y] = col;
                allBlocks.Add(col);
            }

            {
                int i = 0;
                for (int y = 0; y < NumRows; y += BlockHeight)
                {
                    for (int x = 0; x < NumColumns; x += BlockWidth)
                    {
                        var block = new Block(new Position(x, y));
                        mBlocks[i++] = block;
                        allBlocks.Add(block);
                    }
                }
            }
            mAllBlocks = allBlocks;

            for (int y = 0; y < NumRows; ++y)
            {
                var row = GetRow(new Position(0, y));
                for (int x = 0; x < NumColumns; ++x)
                {
                    var pos = new Position(x, y);
                    var col = GetColumn(pos);
                    var block = GetBlock(pos);
                    var cell = new Cell(pos, row, col, block);
                    mCells[GetPositionIndex(pos)] = cell;
                }
            }
        }

        public void Reset()
        {
            foreach (var cell in mCells)
            {
                cell.Reset();
            }
        }

        public void SetFixedValue(Position p, int value)
        {
            var cell = GetCell(p);
            cell.IsFixed = true;
            cell.Value = value;
        }

        public Block GetBlock(Position p)
        {
            return mBlocks[(p.X / BlockWidth) + (BlocksPerWidth * (p.Y / BlockHeight))];
        }

        public Row GetRow(Position p)
        {
            Row row = GetRow(p.Y);
            Debug.Assert(row.BlockRange.Contains(p));
            return row;
        }

        public Row GetRow(int y)
        {
            return mRows[y];
        }

        public Column GetColumn(Position p)
        {
            Column col = GetColumn(p.X);
            Debug.Assert(col.BlockRange.Contains(p));
            return col;
        }

        public Column GetColumn(int x)
        {
            return mColumns[x];
        }

        public Cell GetCell(IReadOnlyPosition p)
        {
            return mCells[GetPositionIndex(p)];
        }

        private bool IsCompleteImpl()
        {
            foreach (var block in mAllBlocks)
            {
                if (!block.IsComplete)
                    return false;
            }
            return true;
        }

        private bool IsOkImpl()
        {
            foreach (var block in mAllBlocks)
            {
                if (!block.IsOk)
                    return false;
            }
            return true;
        }
    }
}
