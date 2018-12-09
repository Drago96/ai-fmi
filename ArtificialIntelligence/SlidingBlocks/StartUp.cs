using System;

namespace SlidingBlocks
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var board = Board.ReadBoard();
            var slidingBlocks = new SlidingBlocks(board);
            slidingBlocks.Solve();
        }
    }
}
