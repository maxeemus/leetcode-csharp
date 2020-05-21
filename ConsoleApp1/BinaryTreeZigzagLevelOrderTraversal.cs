using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    ///  https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal
    /// </summary>
    public class BinaryTreeZigzagLevelOrderTraversal
    {
        public void Test()
        {
            //                                           0  1  2   3     4     5   6  7     8     9     10    11  12    13    14
            //ZigzagLevelOrder(TreeNode.Build(new int?[] { 3, 9, 20, null, null, 15, 7, null, null, null, null, 6,  25,   null, 200 }));
            //ZigzagLevelOrder(TreeNode.Build(new int?[] { 1, 2, 3, 4, 5 })); // [[1],[3,2],[4,5]]
            //ZigzagLevelOrder(TreeNode.Build(new int?[] { 1, 2, 3, 4, null, null, 5})); // [[1],[3,2],[4,5]]            
            ZigzagLevelOrder(TreeNode.Build(new int?[] { 0, 2, 4, 1, null, 3, -1, 5, 1, null, 6, null, 8 })); // [[0],[4,2],[1,3,-1],[8,6,1,5]]
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

        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            List<IList<int>> zigzag = new List<IList<int>>();
            if (root == null)
                return zigzag;
            
            int direction = 1; // 0-right ;  - is left
            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while(queue.Count > 0)
            {                
                var order = new List<int>(4);
                List<TreeNode> levelNodes = new List<TreeNode>(4);
                while (queue.Count > 0)
                {                    
                    var node = queue.Dequeue();
                    if (node != null)
                    {
                        levelNodes.Add(node);
                        order.Add(node.val);
                    }
                }

                foreach(var node in levelNodes)    
                {
                    if (node.left != null)
                        queue.Enqueue(node.left);
                    if (node.right != null)
                        queue.Enqueue(node.right);
                }

                if (direction == 0)
                {
                    order.Reverse(); // if right then reverse order
                    direction = 1;
                }
                else
                    direction = 0;
                zigzag.Add(order);                                
            }
            
            return zigzag;
        }

        #region wrong
        //public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        //{
        //    List<IList<int>> zigzag = new List<IList<int>>();
        //    if (root == null)
        //        return zigzag;
        //    zigzag.Add(new List<int> { root.val });

        //    ZigzagRight(root.left, root.right, zigzag);
        //    return zigzag;
        //}

        //private void ZigzagRight(TreeNode left, TreeNode right, List<IList<int>> zigzag)
        //{
        //    var levelOrder = (new int?[] { right?.val, left?.val }).Where(v => v.HasValue).Select(v => v.Value);
        //    if (levelOrder.Any())
        //        zigzag.Add(levelOrder.ToList());
        //    if (right != null)
        //        ZigzagLeft(right.left, right.right, zigzag);
        //    if (left != null)
        //        ZigzagLeft(left.left, left.right, zigzag);

        //}

        //private void ZigzagLeft(TreeNode left, TreeNode right, List<IList<int>> zigzag)
        //{
        //    var levelOrder = (new int?[] { left?.val, right?.val }).Where(v => v.HasValue).Select(v => v.Value);
        //    if (levelOrder.Any())
        //        zigzag.Add(levelOrder.ToList());
        //    if (left != null)
        //        ZigzagRight(left.left, left.right, zigzag);
        //    if (right != null)
        //        ZigzagRight(right.left, right.right, zigzag);
        //} 
        #endregion
    }

}
