using System;
using System.Linq;
using MoreLinq;

namespace NQueens
{
    public class NQueens
    {
        private readonly int boardSize;
        private readonly Random cellRandomizer;

        private int[] queens;
        private int[] attackedRows;
        private int[] attackedRightDiagonals;
        private int[] attackedLeftDiagonals;

        public static NQueens Init()
        {
            Console.Write("Input board size: ");
            int boardSize = int.Parse(Console.ReadLine());

            return new NQueens(boardSize);
        }

        public NQueens(int boardSize)
        {
            this.boardSize = boardSize;
            this.cellRandomizer = new Random();
        }

        public void Solve()
        {
            while (true)
            {
                this.queens = this.GetRandomBoard();

                int iterations = 0;

                while (iterations < 2 * this.boardSize)
                {
                    int colWithMaxConflicts = this.GetColWithMaxConflcits();
                    int rowWithMinConflicts = this.GetRowWithMinConflicts(colWithMaxConflicts);
                    this.MoveQueen(colWithMaxConflicts, rowWithMinConflicts);

                    iterations++;
                }


                if (this.IsSolution())
                {
                    this.PrintBoard();
                    return;
                }
            }
        }

        private void PrintBoard()
        {
            for (int row = 0; row < this.boardSize; row++)
            {
                for (int col = 0; col < this.boardSize; col++)
                {
                    if (this.queens[col] == row)
                    {
                        Console.Write("* ");
                        continue;
                    }
                    Console.Write("_ ");
                }
                Console.WriteLine();
            }
        }

        private int[] GetRandomBoard()
        {
            this.attackedRows = new int[this.boardSize];
            this.attackedRightDiagonals = new int[2 * this.boardSize];
            this.attackedLeftDiagonals = new int[2 * this.boardSize];

            int[] queens = new int[this.boardSize];

            for (int col = 0; col < this.boardSize; col++)
            {
                int row = this.cellRandomizer.Next(0, this.boardSize);

                queens[col] = row;

                this.attackedRows[row]++;
                this.attackedRightDiagonals[row + col]++;
                this.attackedLeftDiagonals[(row - col) + this.boardSize]++;
            }

            return queens;
        }

        private int GetColWithMaxConflcits()
        {
            var colsWithMaxConflicts = Enumerable.Range(0, this.boardSize - 1).MaxBy(this.GetConflictsForCol);
            var colToGet = this.cellRandomizer.Next(0, colsWithMaxConflicts.Count());
            return colsWithMaxConflicts.ElementAt(colToGet);
        }

        private int GetConflictsForCol(int col)
        {
            int row = this.queens[col];
            return this.attackedRows[row] + this.attackedRightDiagonals[row + col] +
                   this.attackedLeftDiagonals[(row - col) + this.boardSize] - 3;
        }

        private int GetRowWithMinConflicts(int col)
        {
            var rowsWithMinConflicts = Enumerable.Range(0, this.boardSize - 1).MinBy((row) => this.GetConflictsForRow(row, col));
            var rowToGet = this.cellRandomizer.Next(0, rowsWithMinConflicts.Count());
            return rowsWithMinConflicts.ElementAt(rowToGet);
        }

        private int GetConflictsForRow(int row, int col)
        {
            if (row == this.queens[col])
            {
                return this.GetConflictsForCol(col);
            }

            return this.attackedRows[row] + this.attackedRightDiagonals[row + col] +
                   this.attackedLeftDiagonals[(row - col) + this.boardSize];
        }

        private void MoveQueen(int col, int newRow)
        {
            int currentRow = this.queens[col];

            this.attackedRows[currentRow]--;
            this.attackedRightDiagonals[currentRow + col]--;
            this.attackedLeftDiagonals[(currentRow - col) + this.boardSize]--;

            this.attackedRows[newRow]++;
            this.attackedRightDiagonals[newRow + col]++;
            this.attackedLeftDiagonals[(newRow - col) + this.boardSize]++;

            this.queens[col] = newRow;
        }

        private bool IsSolution()
        {
            return this.attackedRows.All(attacks => attacks == 1) &&
                   this.attackedRightDiagonals.All(attacks => attacks <= 1) &&
                   this.attackedLeftDiagonals.All(attacks => attacks <= 1);
        }
    }
}
