using System;

namespace NQueens
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var nQueens = NQueens.Init();
            nQueens.Solve();
        }
    }
}
