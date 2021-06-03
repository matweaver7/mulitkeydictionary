using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ConsoleApp1.Data.Interfaces;
using ConsoleApp1.Data;

namespace ConsoleApp1.UnitTests.Data
{
    // We would add a test per repo type. Right now it's in memory but we could easily put in in a db.
    // TO-DO: I would normally finish this for every case and endpoints. But you get the idea below.
    public class MemoryDictionaryTests
    {
        [Fact]
        public void MemoryDictionary_AddOk()
        {
            GetMemoryDictionary(out var mockDictionary, out var repo);
            repo.Add("foo", "bar");
            Assert.Single(mockDictionary);
            Assert.True(mockDictionary.ContainsKey("foo"));
            mockDictionary.TryGetValue("foo", out var hash);
            Assert.Single(hash);
            Assert.Contains("bar", hash);
        }
        
        [Fact]
        public void MemoryDictionary_AddsMultipleSameOk()
        {
            GetMemoryDictionary(out var mockDictionary, out var repo);
            repo.Add("foo", "bar");
            repo.Add("foo", "baz");
            Assert.Single(mockDictionary);
            Assert.True(mockDictionary.ContainsKey("foo"));
            mockDictionary.TryGetValue("foo", out var hash);
            Assert.Equal(2, hash.Count);
            Assert.Contains("bar", hash);
            Assert.Contains("baz", hash);
        }
        
        [Fact]
        public void MemoryDictionary_AddsMultipleException()
        {
            GetMemoryDictionary(out var mockDictionary, out var repo);
            repo.Add("foo", "bar");
            Exception ex = Assert.Throws<Exception>(() => { repo.Add("foo", "bar"); });
            Assert.Equal("Value Already exists in collection Value:bar", ex.Message);
        }

        [Fact]
        public void MemoryDictionary_KeysOk()
        {
            GetMemoryDictionary(out var mockDictionary, out var repo);
            mockDictionary.Add("foo", new HashSet<string> { "bar" });

            var keys = repo.Keys();
            Assert.Single(keys);
            Assert.Contains("foo", keys);
        }

        [Fact]
        public void MemoryDictionary_KeysDuplicateOk()
        {
            GetMemoryDictionary(out var mockDictionary, out var repo);
            mockDictionary.Add("foo", new HashSet<string> { "bar" });
            mockDictionary.TryGetValue("foo", out var element);
            element.Add("baz");

            var keys = repo.Keys();
            Assert.Single(keys);
            Assert.Contains("foo", keys);
        }

        [Fact]
        public void MemoryDictionary_KeysMultipleOk()
        {
            GetMemoryDictionary(out var mockDictionary, out var repo);
            mockDictionary.Add("foo", new HashSet<string> { "bar" });
            mockDictionary.Add("woot", new HashSet<string> { "baz" });

            var keys = repo.Keys();
            Assert.Equal(2, keys.Count);
            Assert.Contains("foo", keys);
            Assert.Contains("woot", keys);
        }

        [Fact]
        public void MemoryDictionary_KeysMultipleEmpty()
        {
            GetMemoryDictionary(out var mockDictionary, out var repo);

            var keys = repo.Keys();
            Assert.Equal(0, keys.Count);
        }

        private void GetMemoryDictionary(
            out Dictionary<string, HashSet<string>> mockDictionary,
            out MemoryDictionary<string, string> mockRepository
        )
        {
            mockDictionary = new Dictionary<string, HashSet<string>>();
            mockRepository = new MemoryDictionary<string, string>(mockDictionary);
        }
    }
}
