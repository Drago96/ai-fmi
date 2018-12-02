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
            this.playerMove = playerMove == Move.O ? Move.O : Move.X;

            Move currentPlayer = Move.X;

            while (board.Winner == null)
            {
                if (this.playerMove == currentPlayer)
                {
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
            board.Print();
            Console.Write("Select indeces for move: ");
            var dimensions = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var move = (dimensions[0], dimensions[1]);

            if (!board.IsValidMove(move))
            {
                Console.WriteLine("Invalid move!");
                MakePlayerMove(board);
                return;
            }

            board.MakeMove(move);
        }

        public int MakeAIMove(Board board)
        {
            if (this.playerMove == Move.X)
            {
                return GetMin(board, int.MinValue, int.MaxValue, 0);
            }

            return GetMax(board, int.MinValue, int.MaxValue, 0);
        }

        private int GetMax(Board board, int alpha, int beta, int currentDepth)
        {
            if (board.Winner != null)
            {
                return GetScore(currentDepth, board.Winner);
            }

            (int row, int col)? bestMove = null;

            foreach (var move in board.PossibleMoves)
            {

                Board newBoard = new Board(board);

                newBoard.MakeMove(move);

                int score = GetMin(newBoard, alpha, beta, currentDepth + 1);

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
            if (board.Winner != null)
            {
                return GetScore(currentDepth, board.Winner);
            }

            (int row, int col)? bestMove = null;

            foreach (var move in board.PossibleMoves)
            {

                Board newBoard = new Board(board);

                newBoard.MakeMove(move);

                int score = GetMax(newBoard, alpha, beta, currentDepth + 1);

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
