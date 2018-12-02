using System;

namespace MiniMax
{
    class StartUp
    {
        static void Main(string[] args)
        {
            Board ticTacToe = new Board();
            AlphaBetaPruning game = new AlphaBetaPruning();
            game.Play(ticTacToe);
        }
    }
}
