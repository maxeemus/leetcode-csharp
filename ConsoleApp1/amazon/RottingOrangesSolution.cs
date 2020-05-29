using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp1.Amazon
{
    public class RottingOrangesSolution
    {
        public void Test()
        {
            Debug.Assert(OrangesRotting(new int[][]
            {
                new[]{2,1,1},
                new[]{1,1,0},
                new[]{0,1,1}
            }) == 4);

            Debug.Assert(OrangesRotting(new int[][]
            {
                new[]{2,1,1},
                new[]{0,1,1},
                new[]{1,0,1}
            }) == -1);

            Debug.Assert(OrangesRotting(new int[][]
            {
                new[]{0,2},
            }) == 0);
            Debug.Assert(OrangesRotting(new int[][]
            {
                new[]{0,0},
            }) == 0);

            Debug.Assert(OrangesRotting(new int[][]
            {
                new[] {1,2,1,1,2,1,1},
            }) == 2);

            Debug.Assert(OrangesRotting(new int[][]
            {
                new[] {0},
            }) == 0);

            Console.WriteLine($"{nameof(OrangesRotting)} is succesfully done.");
        }

        public int OrangesRotting(int[][] grid)
        {
            if ((grid?.Length ?? 0) == 0)
                return -1;

            int minutes = -1;
            int freshCount = 0;
            Queue<(int row, int col)> queueRotten = new Queue<(int row, int col)>();

            // Init queue and freshCount
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < (grid[row]?.Length ?? -1); col++)
                {
                    switch (grid[row][col])
                    {
                        case 1:
                            freshCount++;
                            break;
                        case 2:
                            queueRotten.Enqueue((row, col));
                            break;
                        default:
                            break;
                    }
                }
            }
            if(freshCount == 0)
                return 0;

            // continue until queue has rotten
            while (queueRotten.Count > 0)
            {                
                minutes++;
                int queueCurrentSize = queueRotten.Count;
                for (int i = 0; i < queueCurrentSize; i++)
                {
                    var (rottenRow, rottenCol) = queueRotten.Dequeue();
                    foreach(var (nbhRow, nbhCol) in GetValidNeibours (grid, rottenRow, rottenCol))
                    {
                        if(grid[nbhRow][nbhCol] == 1)
                        {
                            freshCount--;
                            grid[nbhRow][nbhCol] = 2;
                            queueRotten.Enqueue((nbhRow, nbhCol));
                        }
                    }

                }                
            }

            return freshCount == 0 ? minutes : -1;
        }

        private static readonly int[][] cellNbrs = new int[][]
        {
            new[]{-1, 0},
            new[]{0, 1},
            new[]{1, 0},
            new[]{0, -1},
        };

        
        private IEnumerable<(int, int)> GetValidNeibours(int[][] grid, int row, int col) => cellNbrs
            .Select(p => (row + p[0], col + p[1]))
            .Where(c => IsValidCell(grid, c));

        private bool IsValidCell(int[][] grid, (int row, int col) cell) => 
            cell.row >= 0 && cell.row < grid.Length && 
            cell.col >= 0 && cell.col < (grid[cell.row]?.Length ?? -1);
        
    }
}