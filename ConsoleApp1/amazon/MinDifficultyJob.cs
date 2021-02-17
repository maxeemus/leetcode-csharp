using System;

namespace ConsoleApp1.Amazon
{
    public class MinDifficultyJob
    {
        public int MinDifficulty(int[] jobDifficulty, int d)
        {
            if (jobDifficulty.Length < d)
                return -1;
            int[][] mem = new int[d][];
            for (int i = 0; i < d; i++)
            {
                mem[i] = new int[jobDifficulty.Length];
                Array.Fill(mem[i], -1);
            }
            return dfs(mem, jobDifficulty, d - 1, 0);
        }

        /*
            idx - is a index of first avaliable job in jobDifficulty for days
            day 0   day 1
            [1,2,3][4,6,7]
            ^       ^
            |       |
            idx     idx 

            d - is a how many days for schedule left jobs  (from idx)
        */
        private int dfs(int[][] mem, int[] jobDifficulty, int d, int idx)
        {
            int max = -1;

            // if no days -> get last day dificulty == max job
            if (d == 0)
            {
                while (idx < jobDifficulty.Length)
                {
                    max = Math.Max(max, jobDifficulty[idx++]);
                }
                return max;
            }
            
            if (mem[d][idx] != -1)
                return mem[d][idx];

            max = -1;
            int res = Int32.MaxValue;
            // check all avaliable jobs for day and select with minimum job
            for (int i = idx; i < jobDifficulty.Length - d; i++)
            {
                max = Math.Max(max, jobDifficulty[i]);
                res = Math.Min(res, max + dfs(mem, jobDifficulty, d - 1, i + 1));
            }
            mem[d][idx] = res;

            return res;
        }
    }
}