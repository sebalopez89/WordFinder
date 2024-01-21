using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class WordFinder2
{
    private readonly char[,] matrix;
    private Dictionary<string, int> wordCount = new Dictionary<string, int>();

    public WordFinder2(IEnumerable<string> matrix)
    {
        int matrixSize = matrix.First().Length;
        this.matrix = new char[matrixSize, matrixSize];

        for (int i = 0; i < matrixSize; i++)
        {
            for (int j = 0; j < matrixSize; j++)
            {
                this.matrix[i, j] = matrix.ElementAt(i)[j];
            }
        }
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var wordStreamSet = new HashSet<string>(wordstream);

        Parallel.For(0, matrix.GetLength(0), i =>
        {
            string horizontalWord = "";
            string verticalWord = "";
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                horizontalWord += matrix[i, j];
                verticalWord += matrix[j, i];
            }
            CountWord(horizontalWord, wordStreamSet);
            CountWord(verticalWord, wordStreamSet);
        });

        var topWords = wordCount.OrderByDescending(pair => pair.Value)
                               .Take(10)
                               .Select(pair => pair.Key);

        return topWords;
    }

    private void CountWord(string possibleWord, HashSet<string> wordstreamSet)
    {
        foreach (var word in wordstreamSet.Where(possibleWord.Contains))
        {
            lock (wordCount)
            {
                if (!wordCount.ContainsKey(word))
                {
                    wordCount[word] = 1;
                }
                else
                {
                    wordCount[word]++;
                }
            }
        }
    }
}
