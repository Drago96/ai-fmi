using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeworkOne
{
    public class Board
    {
        private readonly int[][] board;

        public static Board ReadBoard()
        {
            Console.Write("Matrix dimensions: ");
            var dimensions = int.Parse(Console.ReadLine());

            Console.Write("Cells to block: ");
            var numberOfCellsToBlock = int.Parse(Console.ReadLine());

            return new Board(dimensions, numberOfCellsToBlock);
        }

        public Board(int dimensions, int numberOfCellsToBlock)
        {
            this.board = new int[dimensions][];

            for (var i = 0; i < dimensions; i++)
            {
                this.board[i] = Enumerable.Repeat(1, dimensions).ToArray();
            }

            var elementsToBlock = Enumerable.Range(0, dimensions * dimensions).OrderBy(n => Guid.NewGuid())
                .Take(numberOfCellsToBlock);

            elementsToBlock.ToList().ForEach(e =>
            {
                var row = e / dimensions;
                var cell = e % dimensions;

                this.board[row][cell] = 0;
            });
        }

        public void PrintBoard()
        {
            for (var i = 0; i < this.board.Length; i++)
            {
                for (var j = 0; j < this.board.Length; j++)
                {
                    Console.Write($"{this.board[i][j]} ");
                }
                Console.WriteLine();
            }
        }

        public void FindPathDFS((int row, int col) startingCell, (int row, int col) destinationCell)
        {
            var visited = new HashSet<(int, int)>();
            var path = new HashSet<(int, int)>();

            FindPathDFS(startingCell);

            void FindPathDFS((int row, int col) currentCell)
            {
                if (!IsCellTraversable(currentCell, visited))
                {
                    return;
                }

                visited.Add(currentCell);
                path.Add(currentCell);

                if (currentCell.Equals(destinationCell))
                {
                    this.PrintPath(path);
                    return;
                }

                GetNeighbours(currentCell).ToList().ForEach(FindPathDFS);

                path.Remove(currentCell);
            }
        }

        public void FindPathBFS((int row, int col) startingCell, (int row, int col) destinationCell)
        {
            var visited = new HashSet<(int, int)>();
            var parents = new Dictionary<(int, int), (int, int)>();
            var cellsToVisit = new Queue<(int row, int col)>();

            FindPathBFS();

            void FindPathBFS()
            {
                if (IsCellTraversable(startingCell, visited))
                {
                    cellsToVisit.Enqueue(startingCell);
                    visited.Add(startingCell);
                }

                while (cellsToVisit.Count > 0)
                {
                    var currentCell = cellsToVisit.Dequeue();

                    if (currentCell.Equals(destinationCell))
                    {
                        this.PrintPath(parents, destinationCell);
                        return;
                    }

                    GetNeighbours(currentCell).ToList().ForEach(cell =>
                    {
                        if (IsCellTraversable(cell, visited))
                        {
                            parents[cell] = currentCell;
                            cellsToVisit.Enqueue(cell);
                            visited.Add((cell.row, cell.col));
                        }
                    });
                }
            }
        }

        private (int row, int col)[] GetNeighbours((int row, int col) cell)
        {
            return new[]
            {
                (cell.row - 1, cell.col), (cell.row + 1, cell.col),
                (cell.row, cell.col - 1), (cell.row, cell.col + 1)
            };
        }

        private bool IsCellTraversable((int row, int col) cell, HashSet<(int, int)> visited)
        {
            return IsCellInMatrix(cell.row, cell.col) &&
                   !visited.Contains((cell.row, cell.col)) &&
                   this.board[cell.row][cell.col] == 1;
        }

        private bool IsCellInMatrix(int row, int col)
        {
            return row >= 0 && row < this.board.Length && col >= 0 && col < this.board.Length;
        }

        private void PrintPath(Dictionary<(int, int), (int, int)> parents, (int, int) destinationCell)
        {
            var path = new HashSet<(int, int)>();

            path.Add(destinationCell);

            var currentCell = destinationCell;

            while (parents.ContainsKey(currentCell))
            {
                path.Add(parents[currentCell]);
                currentCell = parents[currentCell];
            }

            PrintPath(path);
        }

        private void PrintPath(HashSet<(int, int)> path)
        {
            for (var i = 0; i < this.board.Length; i++)
            {
                for (var j = 0; j < this.board.Length; j++)
                {
                    if (path.Contains((i, j)))
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write($"{this.board[i][j]} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
