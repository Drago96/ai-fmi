using System;
using System.Collections.Generic;
using System.Linq;

namespace SlidingBlocks
{
    public class Board : IComparable<Board>
    {
        private readonly int[][] board;

        public static Board ReadBoard()
        {
            var numberOfBlocks = int.Parse(Console.ReadLine());

            var numberOfRows = Math.Sqrt(numberOfBlocks + 1);

            if (numberOfRows % 1 != 0)
            {
                throw new Exception("Invalid board size");
            }

            var board = new int[(int)numberOfRows][];

            for (var i = 0; i < numberOfRows; i++)
            {
                var numbersForRow = Console.ReadLine().Split(" ").Select(int.Parse);

                if (numbersForRow.Count() != numberOfRows)
                {
                    throw new Exception("Invalid number count for row");
                }

                board[i] = numbersForRow.ToArray();
            }

            return new Board(board);
        }

        public Board(int[][] board)
        {
            this.board = board;
            this.DepthToBoard = 0;
            this.DirectionsToBoard = new List<string>();

            var emptyBlockX = this.board.ToList().FindIndex((row) => row.Contains(0));
            var emptyBlockY = this.board[emptyBlockX].ToList().FindIndex((cell) => cell == 0);
            this.EmptyBlockCoordinates = (emptyBlockX, emptyBlockY);
        }

        public Board(int[][] board, int depthToBoard, List<string> directionsToBoard, (int x, int y) emptyBlockCoordinates)
        {
            this.board = board;
            this.DepthToBoard = depthToBoard;
            this.DirectionsToBoard = directionsToBoard;
            this.EmptyBlockCoordinates = emptyBlockCoordinates;
        }

        public int DepthToBoard { get; set; }

        public List<string> DirectionsToBoard { get; set; }

        public int Weight => this.DepthToBoard + this.Heuristic;

        public (int row, int col) EmptyBlockCoordinates { get; set; }

        public bool IsSolution => this.Heuristic == 0;

        public List<Board> PossibleBoardsAfterMove
        {
            get
            {
                return this.GetTraversableNeighbours(this.EmptyBlockCoordinates).Select(neighbour =>
                {
                    var copiedBoard = board.Select(row => row.ToArray()).ToArray();

                    var temp = copiedBoard[this.EmptyBlockCoordinates.row][this.EmptyBlockCoordinates.col];
                    copiedBoard[this.EmptyBlockCoordinates.row][this.EmptyBlockCoordinates.col] =
                        copiedBoard[neighbour.row][neighbour.col];
                    copiedBoard[neighbour.row][neighbour.col] = temp;

                    return new Board(copiedBoard, this.DepthToBoard + 1, this.DirectionsToBoard.Concat(new[] { neighbour.direction }).ToList(), (neighbour.row, neighbour.col));
                }).ToList();

            }
        }

        private int Heuristic
        {
            get
            {
                return this.board.Sum((row) =>
                    row.Where(cell => cell != 0).Sum(cell => this.GetManhattanDistance(cell, (this.board.ToList().IndexOf(row), row.ToList().IndexOf(cell)))));
            }
        }

        private List<(int row, int col, string direction)> GetTraversableNeighbours((int row, int col) cell)
        {
            (int row, int col, string direction)[] possibleNeighbours = new[]
            {
                (cell.row - 1, cell.col, "down"), (cell.row + 1, cell.col, "up"),
                (cell.row, cell.col - 1, "right"), (cell.row, cell.col + 1, "left")
            };

            return possibleNeighbours.Where(neighbour => IsCellInMatrix(neighbour.row, neighbour.col)).ToList();
        }


        private bool IsCellInMatrix(int row, int col)
        {
            return row >= 0 && row < this.board.Length && col >= 0 && col < this.board.Length;
        }

        private int NumberOfRows => this.board.Length;

        private int GetManhattanDistance(int number, (int row, int col) currentCell)
        {
           var destinationCell = this.GetDestinationCoordinatesForNumber(number);

            return Math.Abs(currentCell.row - destinationCell.row) + Math.Abs(currentCell.col - destinationCell.col);
        }

        private (int row, int col) GetDestinationCoordinatesForNumber(int number)
        {
            var row = number % this.NumberOfRows == 0 ? number / this.NumberOfRows - 1 : number / this.NumberOfRows;

            var col = number % this.NumberOfRows == 0 ? this.NumberOfRows - 1 : number % this.NumberOfRows - 1;

            return (row, col);
        }

        public int CompareTo(Board other)
        {
            return this.Weight - other.Weight;
        }

        public override bool Equals(object other)
        {
            var boardToCompare = (Board)other;

            for (var i = 0; i < this.NumberOfRows; i++)
            {
                for (var j = 0; j < this.NumberOfRows; j++)
                {
                    if (this.board[i][j] != boardToCompare.board[i][j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return this.Heuristic;
        }
    }
}
