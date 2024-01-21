using WordFinderNamespace;
using System;
using System.Collections.Generic;
using System.IO;

try
{
    var initialTime = DateTime.UtcNow;
    Console.WriteLine("Program started");
    // Read word matrix from file
    IEnumerable<string> wordMatrix = File.ReadLines("wordMatrix.txt");

    // Initialize WordFinder with the word matrix
    WordFinder wordFinder = new WordFinder(wordMatrix);

    // Read word stream from file
    IEnumerable<string> wordstream = File.ReadLines("wordStream.txt");

    // Find and print top 10 most repeated words
    var topWords = wordFinder.Find(wordstream);

    Console.WriteLine("Top 10 most repeated words:");
    foreach (var word in topWords)
    {
        Console.WriteLine(word);
    }
    TimeSpan ts = DateTime.UtcNow - initialTime;
    //var timeDif = DateTime.UtcNow.Ticks - initialTime;
    Console.WriteLine($"Time elapsed: {ts.TotalMilliseconds} milisecs");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}