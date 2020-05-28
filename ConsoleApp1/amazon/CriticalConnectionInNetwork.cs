using System.Collections.Generic;
using System.Linq;
using System;

namespace ConsoleApp1.Amazon
{
    class CriticalConnectionInNetwork
    {
        public void Test()
        {
            CriticalConnections(4, new List<IList<int>>
            {
                new List<int>{0,1},
                new List<int>{1,2},
                new List<int>{2,0},
                new List<int>{1,3}
            });
        }

        public IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            IList<IList<int>> res = new List<IList<int>>();

            var graph = BuildAdjListGraph(n, connections);
            int[] vxDisc = new int[n];
            Array.Fill(vxDisc, -1);
            int[] vxLowRanks = new int[n];
            int time = 0;

            for(int i = 0; i < n; i++)
            {
                if(vxDisc[i] == -1)
                    GraphDfs(i, i, ref time, vxLowRanks, vxDisc, graph, res);
            }

            return res;
        }

        private void GraphDfs(int u, int pre, ref int time, int[] vxLowRanks, int[] vxDisc, IList<IList<int>> graph, IList<IList<int>> res) 
        {
            vxDisc[u] = vxLowRanks[u] = time;
            time++;
            foreach(int v in graph[u].Where(x => x != pre)) // ignore parent vertex
            {
                if(vxDisc[v] == -1) // child vertex was ton visited yet
                {
                    GraphDfs(v, u, ref time, vxLowRanks, vxDisc, graph, res);
                    vxLowRanks[u] = Math.Min(vxLowRanks[u], vxLowRanks[v]);
                    if(vxDisc[u] < vxLowRanks[v]) // That means E(u,v) is critical
                        res.Add(new[]{u, v}.ToList());
                }
                else
                    vxLowRanks[u] = Math.Min(vxLowRanks[u], vxDisc[v]);
            }
        }

        private IList<IList<int>> BuildAdjListGraph(int n, IList<IList<int>> connections)
        {
            var graph = Enumerable.Range(0, n).Select(i => (IList<int>)new List<int>()).ToArray();
            foreach(var c in connections)
            {
                int from = c[0];
                int to = c[1];
                graph[from].Add(to);
                graph[to].Add(from);
            }
            return graph;            
        }
    }
}