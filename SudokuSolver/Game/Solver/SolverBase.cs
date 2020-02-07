using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Game.Solver
{
    class SolverBase
    {
        private bool mModified = false;
        private readonly List<IAlgo> mAlgos = new List<IAlgo>();
        private readonly Game mGame;
        private readonly List<IGameWatcher> mGameWatchers = new List<IGameWatcher>();

        private class GameUpdaterWatcher : IGameWatcher
        {
            public void HandleMove(Move move)
            {
                move.AffectedCell.Value = move.Value;
            }
        }

        private class MoveLoggerWatcher : IGameWatcher
        {
            public void HandleMove(Move move)
            {
                Console.WriteLine("Set ({0}, {1}) to {2}", move.AffectedCell.Position.X, move.AffectedCell.Position.Y, move.Value);
            }
        }

        public SolverBase(Game game)
        {
            mGame = game;
            mAlgos.Add(new NumberWiseAlgo(game));
            mAlgos.Add(new FinaliserAlgo(game));

            AddGameWatcher(new GameUpdaterWatcher());
#if DEBUG
            AddGameWatcher(new MoveLoggerWatcher());
#endif
        }

        public void AddGameWatcher(IGameWatcher watcher)
        {
            mGameWatchers.Add(watcher);
        }

        public void RemoveGameWatcher(IGameWatcher watcher)
        {
            mGameWatchers.Remove(watcher);
        }

        public bool Run()
        {
            int i = 1;
            while (!mGame.IsComplete)
            {
                Console.WriteLine("Starting game loop {0}", i);
                Debug.Assert(mGame.IsOk);
                if (!RunImpl())
                    return false;
                ++i;
            }

            return true;
        }

        private bool RunImpl()
        {
            bool foundSomething = false;
            foreach (var algo in mAlgos)
            {
                List<Move> moves;
                Console.WriteLine("Running algo {0}", algo);
                bool goodAlgo = algo.Run(out moves);
                if (goodAlgo)
                {
                    Console.WriteLine("Found {0} moves", moves.Count);
                    foundSomething = true;
                    HandleMoves(moves);
                }
            }

            return foundSomething;
        }

        private void HandleMoves(List<Move> moves)
        {
            foreach (var watcher in mGameWatchers)
            {
                foreach (var move in moves)
                {
                    watcher.HandleMove(move);
                }
            }
        }
    }
}
