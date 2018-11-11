using System;

namespace SlidingBlocks
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Board board = Board.ReadBoard();
            var slidingBlocks = new SlidingBlocks(board);
            slidingBlocks.Solve();
        }
    }
}
