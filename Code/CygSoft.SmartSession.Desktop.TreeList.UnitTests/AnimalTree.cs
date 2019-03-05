using CygSoft.SmartSession.Desktop.TreeList.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CygSoft.SmartSession.Desktop.TreeList.UnitTests
{
    /* *****************************************************

	└───Animals
		├───Invertebrates
		│   ├───Jointed Legs
		│   │   ├───3 Pairs of Legs
		│   │   │   ├───Ant
		│   │   │   ├───Cockroach
		│   │   │   └───Ladybug
		│   │   └───More than 3 Pairs of Legs
		│   │       ├───Scorpion
		│   │       └───Spider
		│   └───Without Legs
		│       ├───Not-worm-like
		│       └───Worm-like
		│           └───Earthworm
		└───Vertebrates
			├───Cold Blooded
			│   ├───Amphibians
			│   │   ├───Frog
			│   │   ├───Newt
			│   │   └───Toad
			│   ├───Fish
			│   │   ├───Goldfish
			│   │   ├───Guppy
			│   │   └───Salmon
			│   └───Reptiles
			│       ├───Crocodile
			│       ├───Lizzard
			│       └───Snake
			└───Warm Blooded
				├───Birds
				└───Mammals
					├───Cat
					│   ├───Lion
					│   └───Tiger
					├───Dog
					└───Dolphin
					
***************************************************** */

    public class AnimalTree
    {
        public static TreeNode GetTree(TreeListView treeListView)
        {
            var animals = new TreeNode(treeListView, "Animals");

            var invertebrates = new TreeNode(treeListView, "Invertebrates");
            animals.Children.Add(invertebrates);

            var jointedLegs = new TreeNode(treeListView, "Jointed Legs");
            invertebrates.Children.Add(jointedLegs);

            var moreThan3PairsOfLegs = new TreeNode(treeListView, "More than 3 Pairs of Legs");
            jointedLegs.Children.Add(moreThan3PairsOfLegs);

            moreThan3PairsOfLegs.Children.Add(new TreeNode(treeListView, "Scorpion"));
            moreThan3PairsOfLegs.Children.Add(new TreeNode(treeListView, "Spider"));

            var threePairsOfLegs = new TreeNode(treeListView, "3 Pairs of Legs");
            jointedLegs.Children.Add(threePairsOfLegs);

            threePairsOfLegs.Children.Add(new TreeNode(treeListView, "Ant"));
            threePairsOfLegs.Children.Add(new TreeNode(treeListView, "Cockroach"));
            threePairsOfLegs.Children.Add(new TreeNode(treeListView, "Ladybug"));

            var withoutLegs = new TreeNode(treeListView, "Without Legs");
            invertebrates.Children.Add(withoutLegs);

            var notWormLike = new TreeNode(treeListView, "Not-worm-like");
            var wormLike = new TreeNode(treeListView, "Worm-like");

            withoutLegs.Children.Add(notWormLike);
            withoutLegs.Children.Add(wormLike);

            wormLike.Children.Add(new TreeNode(treeListView, "Earthworm"));


            var vertebrates = new TreeNode(treeListView, "Vertebrates");
            animals.Children.Add(vertebrates);

            var coldBlooded = new TreeNode(treeListView, "Cold Blooded");
            var warmBlooded = new TreeNode(treeListView, "Warm Blooded");

            vertebrates.Children.Add(coldBlooded);
            vertebrates.Children.Add(warmBlooded);


            var amphibians = new TreeNode(treeListView, "Amphibians");
            var fish = new TreeNode(treeListView, "Fish");
            var reptiles = new TreeNode(treeListView, "Reptiles");

            coldBlooded.Children.Add(amphibians);
            coldBlooded.Children.Add(fish);
            coldBlooded.Children.Add(reptiles);


            var birds = new TreeNode(treeListView, "Birds");
            var mammals = new TreeNode(treeListView, "Mammals");

            warmBlooded.Children.Add(birds);
            warmBlooded.Children.Add(mammals);


            amphibians.Children.Add(new TreeNode(treeListView, "Frog"));
            amphibians.Children.Add(new TreeNode(treeListView, "Newt"));
            amphibians.Children.Add(new TreeNode(treeListView, "Toad"));

            fish.Children.Add(new TreeNode(treeListView, "Goldfish"));
            fish.Children.Add(new TreeNode(treeListView, "Guppy"));
            fish.Children.Add(new TreeNode(treeListView, "Salmon"));

            reptiles.Children.Add(new TreeNode(treeListView, "Crocodile"));
            reptiles.Children.Add(new TreeNode(treeListView, "Lizzard"));
            reptiles.Children.Add(new TreeNode(treeListView, "Snake"));

            var cat = new TreeNode(treeListView, "Cat");
            mammals.Children.Add(cat);
            cat.Children.Add(new TreeNode(treeListView, "Lion"));
            cat.Children.Add(new TreeNode(treeListView, "Tiger"));

            mammals.Children.Add(new TreeNode(treeListView, "Dog"));
            mammals.Children.Add(new TreeNode(treeListView, "Dolphin"));

            return animals;
        }
        public static TreeNode GetTree()
        {
            var treeListView = new TreeListView();
            return GetTree(treeListView);
        }
    }

}
