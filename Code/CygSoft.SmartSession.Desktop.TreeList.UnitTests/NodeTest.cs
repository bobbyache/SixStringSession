using CygSoft.SmartSession.Desktop.TreeList.Tree;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.TreeList.UnitTests
{
    public class NodeTest
    {

        //TODO: Should changing the IsSelected property of the earthworm not modify the SelectedNode?
        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Select_EarthWorm_IsSelected_Changed_Does_Not_Modify_SelectedNode()
        {
            var treeListView = new TreeListView();
            var animals = AnimalTree.GetTree(treeListView);

            var earthworm =
                animals
                    .Children[0/*Invertebrates*/]
                        .Children[1/*Without Legs*/]
                            .Children[1/*Worm-like*/]
                                .Children[0/*Earthworm*/];

            earthworm.IsSelected = true;

            Assert.IsNull(treeListView.SelectedNode);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Earthworm_Parent_Is_WormLike()
        {
            var animals = AnimalTree.GetTree();

            var earthworm =
                animals
                    .Children[0/*Invertebrates*/]
                        .Children[1/*Without Legs*/]
                            .Children[1/*Worm-like*/]
                                .Children[0/*Earthworm*/];

            Assert.AreEqual("Worm-like", (string)earthworm.Parent.Tag);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Earthworm_DoesNot_Have_Children()
        {
            var animals = AnimalTree.GetTree();

            var earthworm =
                animals
                    .Children[0/*Invertebrates*/]
                        .Children[1/*Without Legs*/]
                            .Children[1/*Worm-like*/]
                                .Children[0/*Earthworm*/];

            Assert.IsFalse(earthworm.HasChildren);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Earthworm_IsNot_Expandable()
        {
            var animals = AnimalTree.GetTree();

            var earthworm =
                animals
                    .Children[0/*Invertebrates*/]
                        .Children[1/*Without Legs*/]
                            .Children[1/*Worm-like*/]
                                .Children[0/*Earthworm*/];

            Assert.IsFalse(earthworm.IsExpandable);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Earthworm_NextNode_Is_Null()
        {
            var animals = AnimalTree.GetTree();

            var earthworm =
                animals
                    .Children[0/*Invertebrates*/]
                        .Children[1/*Without Legs*/]
                            .Children[1/*Worm-like*/]
                                .Children[0/*Earthworm*/];

            Assert.IsNull(earthworm.NextNode);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Earthworm_PreviousNode_Is_Null()
        {
            var animals = AnimalTree.GetTree();

            var earthworm =
                animals
                    .Children[0/*Invertebrates*/]
                        .Children[1/*Without Legs*/]
                            .Children[1/*Worm-like*/]
                                .Children[0/*Earthworm*/];


            Assert.IsNull(earthworm.PreviousNode);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Fish_NextNode_Is_Reptiles()
        {
            var animals = AnimalTree.GetTree();

            var fish =
                animals
                    .Children[1/*Vertebrates*/]
                        .Children[0/*Cold Blooded*/]
                            .Children[1/*Fish*/];

            Assert.AreEqual("Reptiles", (string)fish.NextNode.Tag);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Fish_PreviousNode_Is_Amphibians()
        {
            var animals = AnimalTree.GetTree();

            var fish =
                animals
                    .Children[1/*Vertebrates*/]
                        .Children[0/*Cold Blooded*/]
                            .Children[1/*Fish*/];

            Assert.AreEqual("Amphibians", (string)fish.PreviousNode.Tag);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_()
        {
            var treeListView = new TreeListView();
            var root = new TreeNode(treeListView, "ROOT");

            var rootChild1 = new TreeNode(treeListView, "ROOT -> Child 1");
            var rootChild2 = new TreeNode(treeListView, "ROOT -> Child 2");

            root.Children.Add(rootChild1);
            root.Children.Add(rootChild2);

            var child1OfChild1 = new TreeNode(treeListView, "ROOT -> Child 1 -> Child 1");
            var child2OfChild1 = new TreeNode(treeListView, "ROOT -> Child 1 -> Child 2");

            rootChild1.Children.Add(child1OfChild1);
            rootChild1.Children.Add(child2OfChild1);

            root.AssignIsExpanded(true);

            Assert.AreSame(rootChild2, child1OfChild1.BottomNode);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_AssignIsExpanded_Reflects_VisibleChildrenCount_Correctly()
        {
            var treeListView = new TreeListView();
            var root = new TreeNode(treeListView, null);

            var rootChild1 = new TreeNode(treeListView, null);
            var rootChild2 = new TreeNode(treeListView, null);

            root.Children.Add(rootChild1);
            root.Children.Add(rootChild2);

            root.AssignIsExpanded(true);
            var hasExpanded = root.VisibleChildrenCount == 2;

            root.AssignIsExpanded(false);
            var hasCollapsed = root.VisibleChildrenCount == 0;

            Assert.IsTrue(hasExpanded);
            Assert.IsTrue(hasCollapsed);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_AssignIsExpanded_Expands_Node()
        {
            var treeListView = new TreeListView();
            var root = new TreeNode(treeListView, null);
            var initiallyExpanded = root.IsExpanded;

            root.AssignIsExpanded(true);
            var hasExpanded = root.IsExpanded;

            root.AssignIsExpanded(false);
            var hasCollapsed = !root.IsExpanded;

            Assert.IsFalse(initiallyExpanded);
            Assert.IsTrue(hasExpanded);
            Assert.IsTrue(hasCollapsed);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Expand_Expands_DirectChildren_Only()
        {
            var treeListView = new TreeListView();
            var root = new TreeNode(treeListView, null);

            var rootChild1 = new TreeNode(treeListView, null);
            var rootChild2 = new TreeNode(treeListView, null);

            var childOfChild1 = new TreeNode(treeListView, null);
            var childOfChild2 = new TreeNode(treeListView, null);

            root.Children.Add(rootChild1);
            root.Children.Add(rootChild2);
            rootChild1.Children.Add(childOfChild1);
            rootChild2.Children.Add(childOfChild2);

            root.IsExpanded = true;

            Assert.AreEqual(2, root.VisibleChildrenCount);
            Assert.AreEqual(0, rootChild1.VisibleChildrenCount);
            Assert.AreEqual(0, rootChild2.VisibleChildrenCount);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
        public void TreeNode_Tag_Is_Returned_Via_Tag_Property()
        {
            var testEntity = new TestEntity() { Id = 5 };
            var treeListView = new TreeListView();
            var root = new TreeNode(treeListView, null);

            var rootChild1 = new TreeNode(treeListView, testEntity);
            var taggedEntity = rootChild1.Tag as TestEntity;
            Assert.IsNotNull(taggedEntity);
            Assert.AreSame(testEntity, taggedEntity);
            Assert.AreEqual(5, testEntity.Id);
        }

        [Test]
        [Apartment(ApartmentState.STA)]
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

        
        private class TestEntity
        {
            public int Id { get; set; }
        }

        private static TreeNode CreateTreeNode(int depth, int count)
        {
            TreeListView treeListView = new TreeListView();
            TreeNode root = new TreeNode(treeListView, null);
            if (depth > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    root.Children.Add(CreateTreeNode(depth - 1, count));
                }
            }
            return root;
        }
    }
}
