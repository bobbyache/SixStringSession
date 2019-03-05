using CygSoft.SmartSession.Desktop.TreeList.Tree;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.TreeList.UnitTests
{
    [TestFixture]
    public class NodeTest
    {
        [Test]
        public void VisibleChildren()
        {
            TreeNode root = CreateTreeNode(3, 3);
            root.IsExpanded = true;
            var a = root.Children[0];
            a.IsExpanded = true;
            var b = root.Children[0].Children[2];
            b.IsExpanded = true;

            Assert.AreEqual(6, a.VisibleChildrenCount);
            Assert.AreEqual(3, b.VisibleChildrenCount);
        }

        private static TreeNode CreateTreeNode(int depth, int count)
        {
            //TreeListView treeListView = new TreeListView();
            //TreeNode root = new TreeNode(treeListView, null);
            //if (depth > 0)
            //{
            //    for (int i = 0; i < count; i++)
            //    {
            //        root.Children.Add(CreateTreeNode(depth - 1, count));
            //    }
            //}
            //return root;
            throw new NotImplementedException();
        }
    }
}
