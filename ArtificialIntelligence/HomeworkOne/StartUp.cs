using System;

namespace HomeworkOne
{
    public class StartUp
    {
        private static void Main()
        {
            var board = Board.ReadBoard();

            Console.WriteLine("The initial board looks like this:");
            board.PrintBoard();
            Console.WriteLine();

            Console.WriteLine("Please select the starting cell:");
            var startingCell = ReadCell();
            Console.WriteLine();

            Console.WriteLine("Please select the destination cell:");
            var destinationCell = ReadCell();
            Console.WriteLine();

            Console.WriteLine("Path in matrix with DFS:");
            board.FindPathDFS(startingCell, destinationCell);
            Console.WriteLine();

            Console.WriteLine("Optimal path in matrix with BFS:");
            board.FindPathBFS(startingCell, destinationCell);
            Console.WriteLine();
        }

        private static (int, int) ReadCell()
        {
            Console.Write("Row: ");
            var row = int.Parse(Console.ReadLine());

            Console.Write("Col: ");
            var col = int.Parse(Console.ReadLine());

            return (row, col);
        }
    }
}
