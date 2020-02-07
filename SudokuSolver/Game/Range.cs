using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game
{
    interface IReadOnlyRange
    {
        IReadOnlyPosition StartPos { get; }
        IReadOnlyPosition EndPos { get; }
        int Width { get; }
        int Height { get; }
        bool Contains(IReadOnlyPosition p);
        Range Intersect(IReadOnlyRange r);
    }

    class Range : IReadOnlyRange
    {
        public IReadOnlyPosition StartPos { get; set; }
        public IReadOnlyPosition EndPos { get; set; }
        public int Width
        {
            get
            {
                return EndPos.X - StartPos.X;
            }
        }
        public int Height
        {
            get
            {
                return EndPos.Y - StartPos.Y;
            }
        }

        public Range(Position start, Position end)
        {
            StartPos = start;
            EndPos = end;
        }

        public bool Contains(IReadOnlyPosition p)
        {
            return p.X >= StartPos.X && p.X < EndPos.X && p.Y >= StartPos.Y && p.Y < EndPos.Y;
        }

        public Range Intersect(IReadOnlyRange r)
        {
            var startPos = new Position(Math.Max(StartPos.X, r.StartPos.X), Math.Max(StartPos.Y, r.StartPos.Y));
            var endPos = new Position(Math.Min(EndPos.X, r.EndPos.X), Math.Min(EndPos.Y, r.EndPos.Y));
            return new Range(startPos, endPos);
        }
    }
}
