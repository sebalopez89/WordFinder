﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinderNamespace
{
    public class WordFinder
    {
        private List<string>? possibleWords;
        private Dictionary<string, int> wordCount = new Dictionary<string, int>();

        public WordFinder(IEnumerable<string>? matrix)
        {
            ValidateMatrix(matrix);
            int matrixSize = matrix!.First().Length;

            possibleWords = new List<string>();
            for (int i = 0; i < matrixSize; i++)
            {
                possibleWords.Add(new string(matrix!.Select(word => word[i]).ToArray()));
            }
            possibleWords.AddRange(matrix!);
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordDict = new Dictionary<string, string>();
            wordstream.ToList().ForEach(word => wordDict.Add(word, ReverseWord(word)));
            foreach (var possibleWord in possibleWords!)
            {
                CountWord(possibleWord, wordDict);
            }

            // Get top 10 most repeated words
            var topWords = wordCount.OrderByDescending(pair => pair.Value)
                                   .Take(10)
                                   .Select(pair => pair.Key);

            return topWords;
        }

        private void CountWord(string possibleWord, Dictionary<string, string> wordsDict)
        {
            foreach (KeyValuePair<string, string> entry in wordsDict)
            {
                if (possibleWord.Contains(entry.Key) || possibleWord.Contains(entry.Value))
                {
                    if (!wordCount.ContainsKey(entry.Key))
                    {
                        wordCount[entry.Key] = 1;
                    }
                    else
                    {
                        wordCount[entry.Key]++;
                    }
                }
            }
        }

        private void ValidateMatrix(IEnumerable<string>? matrix)
        {
            if (matrix == null || !matrix.Any())
            {
                throw new ArgumentException("Matrix cannot be null or empty.");
            }

            int expectedSize = matrix.Count();

            if (expectedSize > 64)
            {
                throw new ArgumentException("The matrix size should not exceed 64x64,");
            }

            if (matrix.Any(row => row.Length != expectedSize))
            {
                throw new ArgumentException("Matrix must be square (all rows have the same length).");
            }
        }

        private string ReverseWord(string word)
        {
            char[] charArray = word.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}