﻿using PaderConference.Core.Extensions;
using Xunit;

namespace PaderConference.Core.Tests.Extensions
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("Test", "test")]
        [InlineData("test", "test")]
        [InlineData("HelloWorld", "helloWorld")]
        [InlineData("", "")]
        [InlineData("0", "0")]
        public void TestToCamelCase(string s, string expected)
        {
            Assert.Equal(expected, s.ToCamelCase());
        }

        [Theory]
        [InlineData("Test", "test")]
        [InlineData("test", "test")]
        [InlineData("HelloWorld", "helloWorld")]
        [InlineData("", "")]
        [InlineData("0", "0")]
        [InlineData("/0", "/0")]
        [InlineData("/Hello", "/hello")]
        [InlineData("/Hello/World/0", "/hello/world/0")]
        [InlineData("Test/Test2", "test/test2")]
        public void TestToCamelCasePath(string s, string expected)
        {
            Assert.Equal(expected, s.ToCamelCasePath());
        }

        [Theory]
        [InlineData("helloworld", "world", "hello")]
        [InlineData("hello", "world", "hello")]
        [InlineData("", "world", "")]
        public void TestTrimEnd(string s, string trim, string expected)
        {
            Assert.Equal(expected, s.TrimEnd(trim));
        }
    }
}