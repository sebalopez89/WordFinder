using System;
using System.Collections.Generic;
using System.Linq;

namespace WordFinderNamespace
{
    public class WordFinder
    {
        private readonly char[,] matrix;
        private Dictionary<string, int> wordCount = new Dictionary<string, int>();

        public WordFinder(IEnumerable<string>? matrix)
        {
            ValidateMatrix(matrix);
            int matrixSize = matrix!.First().Length;
            this.matrix = new char[matrixSize, matrixSize];

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    this.matrix[i, j] = matrix!.ElementAt(i)[j];
                }
            }
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            // Check horizontally and vertically
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string horizontalWord = "";
                string verticalWord = "";
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    horizontalWord += matrix[i, j];
                    verticalWord += matrix[j, i];
                }
                CountWord(horizontalWord, wordstream);
                CountWord(verticalWord, wordstream);
            }

            // Get top 10 most repeated words
            var topWords = wordCount.OrderByDescending(pair => pair.Value)
                                   .Take(10)
                                   .Select(pair => pair.Key);

            return topWords;
        }

        private void CountWord(string possibleWord, IEnumerable<string> wordstream)
        {
            foreach (var elem in wordstream.Where(word => possibleWord.Contains(word)))
            {
                if (!wordCount.ContainsKey(elem))
                {
                    wordCount[elem] = 1;
                }
                else
                {
                    wordCount[elem]++;
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
    }
}