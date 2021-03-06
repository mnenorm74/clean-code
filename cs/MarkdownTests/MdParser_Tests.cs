﻿using System.Linq;
using FluentAssertions;
using Markdown;
using NUnit.Framework;

namespace MarkdownTests
{
    public class MdParser_Tests
    {
        [TestCase("just text", 1)]
        [TestCase("text _emTag_", 2)]
        [TestCase("text _cutEmTag", 2)]
        [TestCase("text __strongTag__ _cutEmTag", 4)]
        [TestCase("_emStart __strongTagInEmTag__ emFinish_", 1)]
        [TestCase("__strongStart _emInStrong_ strongFinish__", 2)]
        [TestCase("_emStart __strongInEm__ emFinish_", 1)]
        public void MdParser_GetTokens_ReturnRightTokensCount(string text, int tokensCount)
        {
            new MdParser().GetTokens(text).Count().Should().Be(tokensCount);
        }

        [TestCase("just text", 0)]
        [TestCase("text _emTag_", 0, 5)]
        [TestCase("text _cutEmTag", 0, 5)]
        [TestCase("text __strongTag__ _cutEmTag", 0, 5, 18, 19)]
        [TestCase("_emStart __strongTagInEmTag__ emFinish_", 0)]
        [TestCase("__strongStart _emInStrong_ strongFinish__", 14, 0)]
        [TestCase("_emStart __strongInEm__ emFinish_", 0)]
        public void MdParser_GetTokens_ReturnRightTokensPositions(string text, params int[] expectedPositions)
        {
            var tokens = new MdParser().GetTokens(text);
            var positions = tokens.Select(token => token.Position).ToArray();
            positions.Should().BeEquivalentTo(expectedPositions);
        }

        [TestCase("just text", 9)]
        [TestCase("text _emTag_", 5, 7)]
        [TestCase("text _cutEmTag", 5, 9)]
        [TestCase("text __strongTag__ _cutEmTag", 5, 13, 1, 9)]
        [TestCase("_emStart __strongTagInEmTag__ emFinish_", 39)]
        [TestCase("__strongStart _emInStrong_ strongFinish__", 12, 41)]
        [TestCase("_emStart __strongInEm__ emFinish_", 33)]
        public void MdParser_GetTokens_ReturnRightTokensLength(string text, params int[] expectedLengths)
        {
            var tokens = new MdParser().GetTokens(text);
            var positions = tokens.Select(token => token.Length).ToArray();
            positions.Should().BeEquivalentTo(expectedLengths);
        }

        [TestCase("just text", 0)]
        [TestCase("text _emTag_", 0, 0)]
        [TestCase("text _cutEmTag", 0, 0)]
        [TestCase("text __strongTag__ _cutEmTag", 0, 0, 0, 0)]
        [TestCase("_emStart __strongTagInEmTag__ emFinish_", 0)]
        [TestCase("__strongStart _emInStrong_ strongFinish__", 1, 1)]
        [TestCase("_emStart __strongInEm__ emFinish_", 0)]
        public void MdParser_GetTokens_ReturnRightTokensNestedCount(string text, params int[] expectedNestedCount)
        {
            var tokens = new MdParser().GetTokens(text);
            var nesting = tokens.Select(token => token.NestedTokenCount).ToArray();
            nesting.Should().BeEquivalentTo(expectedNestedCount);
        }
    }
}