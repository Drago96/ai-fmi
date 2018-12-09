using System;

namespace MiniMax
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var ticTacToe = new Board();
            var game = new AlphaBetaPruning();
            game.Play(ticTacToe);
        }
    }
}
