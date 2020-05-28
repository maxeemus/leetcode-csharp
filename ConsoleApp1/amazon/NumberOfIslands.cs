using System.Collections.Generic;
using System.Linq;
using System;

namespace ConsoleApp1.Amazon
{
    class NumberOfIslands
    {
        public void Test()
        {
            var num = NumIslands(new char[][] 
            {
                "11000".ToCharArray(),
                "11000".ToCharArray(),
                "00100".ToCharArray(),
                "00011".ToCharArray(),
            });

            num = NumIslands(new char[][] 
            {
                "111111".ToCharArray(),
                "100001".ToCharArray(),
                "101101".ToCharArray(),
                "100001".ToCharArray(),
                "111111".ToCharArray(),
            });
        }

        private static readonly int[][] cellNbrs = new int[][] 
        {
            new[]{-1, 0},
            new[]{0, 1},
            new[]{1, 0},
            new[]{0, -1},
        };
        
        // Traverse only if row/col are not out of range, cell is island and was not visited before
        private static bool CanTraverseCell(char[][] grid, int row, int col, HashSet<(int, int)> visited) => 
            row > -1 && row < grid.Length &&
            col > -1 && col < grid[row].Length &&
            grid[row][col] == '1' && !visited.Contains((row, col));

        private static void Dfs(char[][] grid, int row, int col, HashSet<(int, int)> visited)
        {                        
            visited.Add((row, col)); // mark as visited to prevent inf. traversal
            foreach(var (r, c) in cellNbrs.Select(n => (r: row + n[0], c: col + n[1]))) // iterate all neibours 
            {
                if(CanTraverseCell(grid, r, c, visited))                
                    Dfs(grid, r, c, visited);
            }            
        }

        public int NumIslands(char[][] grid) 
        {
            int islandCount = 0;
            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            for (int row = 0; row < grid.Length; row++)
            {
                for (int col = 0; col < grid[row].Length; col++)
                {
                    if(CanTraverseCell(grid, row, col, visited))
                    {
                        Dfs(grid, row, col, visited);                        
                        islandCount++;
                    }

                }
            }
            return islandCount;
        }        
    }
}