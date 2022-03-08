using Algorithms;

var playfair = new Playfair(new KeyTable());

playfair.GenerateKeyTable("playfair");
Console.WriteLine(playfair.Encipher("playfair is great"));
Console.WriteLine(playfair.Decipher("LAYFPYRBCNOGHPSZ"));

/*
var type = args[0].ToLower();
var key = args[1];
var text = string.Join(" ", args[2..]);

playfair.GenerateKeyTable(key);

if(type == "encrypt")
{
    Console.WriteLine(playfair.Encipher(text));
}
else if(type == "decrypt")
{
    Console.WriteLine(playfair.Decipher(text));
}
*/