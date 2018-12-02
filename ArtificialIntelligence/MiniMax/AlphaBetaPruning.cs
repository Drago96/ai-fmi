using System;
using System.Linq;

namespace MiniMax
{
    class AlphaBetaPruning
    {
        private Move playerMove;

        public void Play(Board board)
        {
            Console.Write("Please select X or O: ");
            Enum.TryParse(Console.ReadLine(), out Move playerMove);
            this.playerMove = playerMove;

            Move currentPlayer = Move.X;

            while (board.Winner == null)
            {
                if (this.playerMove == currentPlayer)
                {
                    board.Print();
                    this.MakePlayerMove(board);
                }
                else
                {
                    this.MakeAIMove(board);
                }

                currentPlayer = currentPlayer == Move.X ? Move.O : Move.X;
            }

            board.Print();

            if (board.Winner == this.playerMove)
            {
                Console.WriteLine("Congratz! You win!");
            }
            else if (board.Winner != Move.Blank)
            {
                Console.WriteLine("Oh no! You lose!");
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }
        }

        public void MakePlayerMove(Board board)
        {
            Console.Write("Select indeces for move: ");
            var dimensions = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            board.MakeMove((dimensions[0], dimensions[1]));
        }

        public void MakeAIMove(Board board)
        {
            this.MakeAIMove(board, int.MinValue, int.MaxValue, 0, this.playerMove != Move.X);   
        }

        private int MakeAIMove(Board board, int alpha, int beta, int currentDepth, bool isMax)
        {
            if (board.Winner != null)
            {
                return GetScore(currentDepth, board.Winner);
            }

            if (isMax)
            {
                return GetMax(board, alpha, beta, currentDepth);
            }

            return GetMin(board, alpha, beta, currentDepth);
        }

        private int GetMax(Board board, int alpha, int beta, int currentDepth)
        {
            (int row, int col)? bestMove = null;

            foreach (var move in board.PossibleMoves)
            {

                Board newBoard = new Board(board);

                newBoard.MakeMove(move);

                int score = MakeAIMove(newBoard, alpha, beta, currentDepth + 1, false);

                if (score > alpha)
                {
                    alpha = score;
                    bestMove = move;
                }

                if (alpha >= beta)
                {
                    break;
                }
            }

            if (bestMove != null)
            {
                board.MakeMove(bestMove.Value);
            }

            return alpha;
        }

        private int GetMin(Board board, int alpha, int beta, int currentDepth)
        {
            (int row, int col)? bestMove = null;

            foreach (var move in board.PossibleMoves)
            {

                Board newBoard = new Board(board);

                newBoard.MakeMove(move);

                int score = MakeAIMove(newBoard, alpha, beta, currentDepth + 1, true);

                if (score < beta)
                {
                    beta = score;
                    bestMove = move;
                }

                if (alpha >= beta)
                {
                    break;
                }
            }

            if (bestMove != null)
            {
                board.MakeMove(bestMove.Value);
            }

            return beta;
        }

        private int GetScore(int currentDepth, Move? winner)
        {
            if (winner == Move.X)
            {
                return 10 - currentDepth;
            }

            if (winner == Move.O)
            {
                return -10 + currentDepth;
            }

            return 0;
        }
    }
}
