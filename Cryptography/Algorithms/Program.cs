using Algorithms.Playfair;

var key = "tree";
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
