using Algorithms;

var playfair = new Playfair(new KeyTable());

playfair.GenerateKeyTable("playfair example");
Console.WriteLine(playfair.Encipher("playfair is great"));
Console.WriteLine(playfair.Decipher("LAYFPYREMKCXDEWI"));


/*
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
*/
