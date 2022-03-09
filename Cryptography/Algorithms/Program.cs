using Algorithms;

var playfair = new Playfair(new KeyTable());

var key = "playfair";
var plainText = "Like most classical ciphers, the Playfair cipher can be easily cracked if there is enough text.";
var cipherText = "PRMGGTTNRYYQNCBYYREIKGCOQMNIAYFPPBBDEIKGBDPQIHHPNCAFDBYBMGIRDZKGIGCNNUNVHKNMZS";

playfair.GenerateKeyTable(key);

Console.WriteLine($"Key: {key}");
Console.WriteLine($"Plain text: {plainText}");
Console.WriteLine($"Encrypted text: {playfair.Encipher(plainText)}");
Console.WriteLine($"Decrypted text: {playfair.Decipher(cipherText)}");

/*
var type = args[0].ToLower();
var key = args[1];
var text = string.Join(" ", args[2..]);

playfair.GenerateKeyTable(key);

if (type == "encrypt")
{
    Console.WriteLine($"Encrypted text: {playfair.Encipher(text)}");
}
else if (type == "decrypt")
{
    Console.WriteLine($"Decrypted text: {playfair.Decipher(text)}");
}
*/