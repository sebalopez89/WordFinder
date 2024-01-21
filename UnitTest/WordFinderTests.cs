using WordFinderNamespace;
using System;
using System.Collections.Generic;

namespace WordFinderTests
{
    [TestFixture]
    public class WordFinderTests
    {
        [Test]
        public void Constructor_ValidMatrix_DoesNotThrowException()
        {
            // Arrange
            IEnumerable<string> validMatrix = new List<string>
            {
                "helloo",
                "worlds",
                "exampl",
                "matrix",
                "dotnet",
                "square"
            };

            // Act and Assert
            Assert.DoesNotThrow(() => new WordFinder(validMatrix));
        }

        [Test]
        public void Constructor_NullMatrix_ThrowsArgumentException()
        {
            // Arrange
            IEnumerable<string>? nullMatrix = null;

            // Act and Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(nullMatrix));
        }

        [Test]
        public void Constructor_EmptyMatrix_ThrowsArgumentException()
        {
            // Arrange
            IEnumerable<string> emptyMatrix = new List<string>();

            // Act and Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(emptyMatrix));
        }

        [Test]
        public void Constructor_NonSquareMatrix_ThrowsArgumentException()
        {
            // Arrange
            IEnumerable<string> nonSquareMatrix = new List<string>
            {
                "hello",
                "world",
                "example"
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() => new WordFinder(nonSquareMatrix));
        }

        [Test]
        public void Find_TopWordsInMatrixAndStream_ReturnsTopWords()
        {
            // Arrange
            IEnumerable<string> wordMatrix = new List<string>
            {
                "hellooo",
                "woorlld",
                "hellooo",
                "woorlld",
                "example",
                "example",
                "letters"
            };

            IEnumerable<string> wordStream = new List<string>
            {
                "hello",
                "ododees",
                "example",
                "word"
            };

            WordFinder wordFinder = new WordFinder(wordMatrix);

            // Act
            var topWords = wordFinder.Find(wordStream);

            // Assert
            CollectionAssert.AreEquivalent(new[] { "hello", "example", "ododees" }, topWords);
        }
    }
}