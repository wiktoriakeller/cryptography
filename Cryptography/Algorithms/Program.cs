using Algorithms;

var playfair = new Playfair(new DuplicateRemover());

/*
playfair.GenerateKeyTable("playfair example");
Console.WriteLine(playfair.Encipher("hide the gold in the tree stump"));
Console.WriteLine(playfair.Decipher("BMODZBXDNABEKUDMUIXMMOUVIF"));
*/

var type = args[0].ToLower();
var key = args[1];
var text = args[2];

playfair.GenerateKeyTable(key);

if(type == "encrypt")
{
    Console.WriteLine(playfair.Encipher(text.ToLower()));
}
else if(type == "decrypt")
{
    Console.WriteLine(playfair.Decipher(text.ToUpper()));
}