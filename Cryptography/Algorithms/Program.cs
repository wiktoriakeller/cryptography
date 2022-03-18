﻿using Algorithms.Playfair;

var key = "playfair example";
var plainText = "Like most classical ciphers, the Playfair cipher can be easily cracked if there is enough text. Obtaining the key is relatively straightforward if both plaintext and ciphertext are known. When only the ciphertext is known, brute force cryptanalysis of the cipher involves searching through the key space for matches between the frequency of occurrence of digrams and the known frequency of occurrence of digrams in the assumed language of the original message. Cryptanalysis of Playfair is similar to that of foursquare and two square ciphers, though the relative simplicity of the Playfair system makes identifying candidate plaintext strings easier. Most notably, a Playfair digraph and its reverse will decrypt to the same letter pattern in the plaintext. In English, there are many words which contain these reversed digraphs such as receiver and departed. Identifying nearby reversed digraphs in the ciphertext and matching the pattern to a list of known plaintext words containing the pattern is an easy way to generate possible plaintext strings with which to begin constructing the key. A different approach to tackling a Playfair cipher is the shotgun hill climbing method. This starts with a random square of letters. Then minor changes are introduced (i.e. switching letters, rows, or reflecting the entire square) to see if the candidate plaintext is more like standard plaintext than before the change (perhaps by comparing the digrams to a known frequency chart). If the new square is deemed to be an improvement, then it is adopted and then further mutated to find an even better candidate. Eventually, the plaintext or something very close is found to achieve a maximal score by whatever grading method is chosen. This is obviously beyond the range of typical human patience, but computers can adopt this algorithm to crack Playfair ciphers with a relatively small amount of text. Another aspect of Playfair that separates it from four square and two square ciphers is the fact that it will never contain a double letter digram. If there are no double letter digrams in the ciphertext and the length of the message is long enough to make this statistically significant, it is very likely that the method of encryption is Playfair.";
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
