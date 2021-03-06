﻿using FluentAssertions;
using Markdown;
using NUnit.Framework;

namespace MarkdownTests
{
    public class StrongTagType_Tests
    {
        [TestCase("_asasv dvd", 0, false)]
        [TestCase("__asasv dvd", 0, true)]
        [TestCase("__asasv __dvd", 8, true)]
        [TestCase("__asasv__ __dvd", 7, false)]
        public void StrongTagType_IsOpenedTag_ReturnRightResult(string text, int position, bool expected)
        {
            var strong = new StrongTagType();
            strong.IsOpenedTag(text, position).Should().Be(expected);
        }
        
        [TestCase("_asasv dvd", 0, false)]
        [TestCase("__asasv dvd", 0, false)]
        [TestCase("__asasv __dvd", 8, false)]
        [TestCase("__asasv__ __dvd", 7, true)]
        [TestCase("__as__ asv__ __dvd", 4, true)]
        public void StrongTagType_IsClosedTag_ReturnRightResult(string text, int position, bool expected)
        {
            var strong = new StrongTagType();
            strong.IsClosedTag(text, position).Should().Be(expected);
        }
    }
}