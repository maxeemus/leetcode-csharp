using System.Collections.Generic;
using FluentAssertions;

namespace ConsoleApp1.Common
{
    public class TreeNode
    {
        public static TreeNode Create(int?[] array)
        {
            if((array?.Length ?? 0) == 0)
                return null;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            TreeNode root = new TreeNode(array[0].Value);
            queue.Enqueue(root);
            int i = 0;
            while(queue.Count > 0 && i < array.Length)
            {
                var node = queue.Dequeue();
                if(i + 1 < array.Length && array[i + 1].HasValue)
                {
                    node.left = new TreeNode(array[i + 1].Value);
                    queue.Enqueue(node.left);
                }
                if(i + 2 < array.Length && array[i + 2].HasValue)
                {
                    node.right = new TreeNode(array[i + 2].Value);
                    queue.Enqueue(node.right);
                }
                i += 2;
            }

            return root;
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

    class TreeNodeTests
    {
        public static void CreateTest()
        {
            TreeNode.Create(new int?[0]).Should().BeNull();
            TreeNode.Create(new int?[]{1}).Should().BeEquivalentTo(new TreeNode(1));
            TreeNode.Create(new int?[]{1,2}).Should().BeEquivalentTo(new TreeNode(1, new TreeNode(2)));
            TreeNode.Create(new int?[]{1,2,3}).Should().BeEquivalentTo(new TreeNode(1, new TreeNode(2), new TreeNode(3)));            
        }
    }
}