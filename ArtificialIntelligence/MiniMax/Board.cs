using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace MiniMax
{
    class Board
    {
        private Move[,] board;
        private Move currentPlayer;

        public Board()
        {
            this.board = new[,] { { Move.Blank, Move.Blank, Move.Blank }, { Move.Blank, Move.Blank, Move.Blank }, { Move.Blank, Move.Blank, Move.Blank } };
            this.currentPlayer = Move.X;
            this.Winner = null;
        }

        public Board(Board otherBoard)
        {
            this.board = (Move[,])otherBoard.board.Clone();
            this.currentPlayer = otherBoard.currentPlayer;
            this.Winner = otherBoard.Winner;
        }

        public Move? Winner { get; private set; }

        public void Print()
        {
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    Console.Write(this.board[i,j] == Move.Blank ? "-" : this.board[i,j].ToString());
                    Console.Write(" ");
                }

                Console.WriteLine();
            }
        }

        public IEnumerable<(int row, int col)> PossibleMoves
        {
            get
            {
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (this.board[i, j] == Move.Blank)
                        {
                            yield return (i, j);
                        }
                    }
                }
            }
        }

        public bool IsValidMove((int row, int col) move)
        {
            return this.board[move.row, move.col] == Move.Blank;
        }

        public void MakeMove((int row, int col) move)
        {
            this.board[move.row, move.col] = this.currentPlayer;

            this.CheckForGameOver(move, currentPlayer);

            this.currentPlayer = this.currentPlayer == Move.X ? Move.O : Move.X;
        }

        private void CheckForGameOver((int row, int col) move, Move currentPlayer)
        {
            if (IsFullCol() || IsFullRow() || IsFullDiagonal())
            {
                this.Winner = currentPlayer;
            }
            else if (IsBoardFull())
            {
                this.Winner = Move.Blank;
            }

            bool IsFullDiagonal()
            {
                return new[] { this.board[0, 0], this.board[1, 1], this.board[2, 2] }.All(block => block == currentPlayer)
                       || new[] { this.board[0, 2], this.board[1, 1], this.board[2, 0] }.All(block =>
                             block == currentPlayer);
            }

            bool IsFullRow()
            {
                for (var i = 0; i < 3; i++)
                {
                    if (this.board[i, move.col] != currentPlayer)
                    {
                        return false;
                    }
                }

                return true;
            }

            bool IsFullCol()
            {
                for (var i = 0; i < 3; i++)
                {
                    if (this.board[move.row, i] != currentPlayer)
                    {
                        return false;
                    }
                }

                return true;
            }

            bool IsBoardFull()
            {
                for (var i = 0; i < 3; i++)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        if (this.board[i, j] == Move.Blank)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
}
