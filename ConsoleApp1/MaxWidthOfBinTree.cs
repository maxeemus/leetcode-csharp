using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace ConsoleApp1
{
    /// <summary>
    ///  https://leetcode.com/problems/maximum-width-of-binary-tree/
    /// </summary>
    public class MaxWidthOfBinTree
    {
        public static void Test()
        {            
            new MaxWidthOfBinTree().WidthOfBinaryTree(TreeNode.Build(new int?[] { 1,3,2,5,3,null,9 })).Should().Be(4); 
            new MaxWidthOfBinTree().WidthOfBinaryTree(TreeNode.Build(new int?[] { 1,3,2,5 })).Should().Be(2); 
            //new MaxWidthOfBinTree().WidthOfBinaryTree(TreeNode.Build(new int?[] { 1,1,1,1,null,null,1,1,null,null,1 })).Should().Be(8); 

            new MaxWidthOfBinTree().WidthOfBinaryTree(
                new TreeNode(0, 
                    new TreeNode(1,
                        new TreeNode(3, 
                            new TreeNode(7),
                            null),
                        null
                    ),
                    new TreeNode(2,
                        new TreeNode(5),
                        null
                    ))
            ).Should().Be(3); 
        }

        public class TreeNode
        {
            public static TreeNode Build(int?[] array)
            {
                var nodes = array?.Select(v => v.HasValue ? new TreeNode(v.Value) : null).ToArray();                     
                foreach(var item in nodes?.Select((node, i) => (node, i)))
                {
                    if (item.node != null)
                    {
                        int left = item.i * 2 + 1;
                        int right = item.i * 2 + 2;
                        if (left < array.Length)
                            item.node.left = nodes[left];
                        if (right < array.Length)
                            item.node.right = nodes[right];
                    }                    
                }
                return nodes?.Where(node => node != null).FirstOrDefault();
            }
            
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
                this.val = val;
                this.left = left;
                this.right = right;
            }
        }

        public int WidthOfBinaryTree(TreeNode root)
        {
            int maxWidth = 1;
            var queue = new Queue<(TreeNode node, int rank)>();
            queue.Enqueue((root, 0));

            while(queue.Count > 0)
            {                                
                var levelNodes = new List<(TreeNode node, int rank)>();
                while(queue.Count > 0)
                {                    
                    levelNodes.Add(queue.Dequeue());
                }                
                                
                // if there is 1 or 0 node on the tree level then this level must be ignored because it has only one edge (left or right). If one edge then imposible to get level width
                if(levelNodes.Count > 1) 
                {
                    int minLevelRank = levelNodes.First().rank;
                    int maxLevelRank = levelNodes.Last().rank;
                    maxWidth = Math.Max(maxWidth, maxLevelRank - minLevelRank + 1);
                }
                foreach(var n in levelNodes)    
                {                                                            
                    if (n.node.left != null)
                        // left child with rank
                        queue.Enqueue((n.node.left, n.rank * 2 + 1));                         
                    if (n.node.right != null)
                        // right child with rank
                        queue.Enqueue((n.node.right, n.rank * 2 + 2));                                        
                }
            }

            return maxWidth;
        }           
    }

}
