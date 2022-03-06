using Algorithms;

var playfair = new Playfair(new DuplicateRemover());
playfair.GenerateKeyTable("playfair example");

Console.WriteLine(playfair.Encipher("hide the gold in the tree stump"));
