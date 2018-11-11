using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace SlidingBlocks
{
    public class SlidingBlocks
    {
        private readonly Board board;

        public SlidingBlocks(Board board)
        {
            this.board = board;
        }

        public void Solve()
        {
            var visited = new HashSet<Board>();
            var boardsToExplore = new MinPriorityQueue<Board>();

            visited.Add(this.board);
            boardsToExplore.Enqueue(this.board);


            while (boardsToExplore.Count >= 0)
            {
                if (boardsToExplore.Count == 0)
                {
                    Console.WriteLine("No solution found");
                    return;
                }

                var board = boardsToExplore.Dequeue();

                if (board.IsSolution)
                {
                    Console.WriteLine(board.DepthToBoard);
                    board.DirectionsToBoard.ForEach(Console.WriteLine);
                    return;
                }

                board.PossibleBoardsAfterMove.ToList().ForEach(neighbourBoard =>
                {
                    if (!visited.Contains(neighbourBoard))
                    {
                        visited.Add(neighbourBoard);
                        boardsToExplore.Enqueue(neighbourBoard);
                    }
                });

            }
        }

    }
}
