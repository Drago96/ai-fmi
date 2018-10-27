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
            (int, int) startingCell = ReadCell();
            Console.WriteLine();

            Console.WriteLine("Please select the destination cell:");
            (int, int) destinationCell = ReadCell();
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
            int row = int.Parse(Console.ReadLine());

            Console.Write("Col: ");
            int col = int.Parse(Console.ReadLine());

            return (row, col);
        }
    }
}
