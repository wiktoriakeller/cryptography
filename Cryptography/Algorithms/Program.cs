using Algorithms.Playfair;

/*
var key = "playfair example";
var plainText = "Like most classical ciphers, the Playfair cipher can be easily cracked if there is enough text.";
var playfair = new Playfair(new KeyTable());
playfair.LeaveOnlyLetters(false);
playfair.GenerateKeyTable(key);

var enciphered = playfair.Encipher(plainText);
var deciphered = playfair.Decipher(enciphered);

Console.WriteLine($"Key: {key}");
Console.WriteLine($"Plain text: {plainText}");
Console.WriteLine();
Console.WriteLine($"Encrypted text: {enciphered}");
Console.WriteLine();
Console.WriteLine($"Decrypted text: {deciphered}");
Console.WriteLine();

var playfairExtended = new PlayfairExtended(new KeyTableExtended());
playfairExtended.GenerateKeyTable(key);
var enciphered2 = playfairExtended.Encipher(plainText);
var deciphered2 = playfairExtended.Decipher(enciphered2);

Console.WriteLine($"Encrypted text 2: {enciphered2}");
Console.WriteLine();
Console.WriteLine($"Decrypted text 2: {deciphered2}");
*/

var type = args[0].ToLower();
var method = args[1].ToLower();
var key = args[2];
var text = string.Join(" ", args[3..]);
PlayfairBase playfair;

if(type == "extended")
{
    playfair = new PlayfairExtended(new KeyTableExtended());
}
else
{
    playfair = new Playfair(new KeyTable());
    var normalPlayfair = playfair as Playfair;
    normalPlayfair?.LeaveOnlyLetters(false);
}

playfair.GenerateKeyTable(key);

if (method == "encrypt")
{
    Console.WriteLine($"Encrypted text: {playfair.Encipher(text)}");
}
else
{
    Console.WriteLine($"Decrypted text: {playfair.Decipher(text)}");
}
