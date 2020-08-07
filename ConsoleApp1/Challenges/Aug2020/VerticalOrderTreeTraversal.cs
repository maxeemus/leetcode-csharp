using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Common;
using FluentAssertions;

namespace Challenges.Aug2020
{
    // https://leetcode.com/problems/vertical-order-traversal-of-a-binary-tree/solution/
    class VerticalOrderTreeTraversal
    {        
        public static void Test()
        {
            new VerticalOrderTreeTraversal().VerticalTraversal(TreeNode.Create(new int?[]{1,2,3,4,5,6,7})).Should().BeEquivalentTo(new[]{new[]{4},new[]{2},new[]{1,5,6},new[]{3},new[]{7}});
            new VerticalOrderTreeTraversal().VerticalTraversal(TreeNode.Create(new int?[]{15,9,20,null,null,3,7})).Should().BeEquivalentTo(new[]{new[]{9},new[]{3,15},new[]{20},new[]{7}});
            new VerticalOrderTreeTraversal().VerticalTraversal(TreeNode.Create(new int?[]{0,8,1,null,null,3,2,null,4,5,null,null,7,6})).Should().BeEquivalentTo(new[]{new[]{8},new[]{0,3,6},new[]{1,4,5},new[]{2,7}});
            new VerticalOrderTreeTraversal().VerticalTraversal(TreeNode.Create(new int?[]{0,5,1,9,null,2,null,null,null,null,3,4,8,6,null,null,null,7})).Should().BeEquivalentTo(new[]{new[]{9,7},new[]{5,6},new[]{0,2,4},new[]{1,3}, new[]{8}});            
        }

        public IList<IList<int>> VerticalTraversal(TreeNode root) 
        {            
            var traversed = new List<(TreeNode node, int x, int y)>();
            DFS(root, 0, 0, traversed);
            var res = traversed
                .GroupBy(o => o.x)
                .Select(g => 
                (
                    vals: g.OrderBy(v => v, Comparer<(TreeNode node, int x, int y)>.Create(CompareSameX)).Select(v => v.node.val), 
                    x: g.Key
                ))
                .OrderBy(g => g.x)
                .Select(g => (IList<int>)g.vals.ToList())
                .ToList();
            return (IList<IList<int>>)res;
        }

        private int CompareSameX((TreeNode node, int x, int y) v1, (TreeNode node, int x, int y) v2) => v1.y != v2.y ? v1.y - v2.y : v1.node.val - v2.node.val;
        
        private static void DFS(TreeNode node, int x, int y, List<(TreeNode node, int x, int y)> traversed)
        {
            if(node != null)
            {
                traversed.Add((node, x, y));
                DFS(node.left, x - 1, y + 1, traversed);
                DFS(node.right, x + 1, y + 1, traversed);
            }
        }        
    }    
}