using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Data.Interfaces;
using ConsoleApp1.Contracts.Interfaces;

namespace ConsoleApp1.Data
{
    // TODO : pull out the validation and put it in the service layer with it's own class (but probably won't because of time...though if I had 3 hours instead of 2)
    // this would also help with the exception handling. Because we really shouldn't do it here. Anything messing with data should already be validated.
    public class MemoryDictionary<k, v> : ICustomDictionary<k, v>
    {
        IDictionary<k, HashSet<v>> _Dictionary;
        public MemoryDictionary(){
            _Dictionary = new Dictionary<k, HashSet<v>>();
        }

        public MemoryDictionary(IDictionary<k, HashSet<v>> dictionary)
        {
            _Dictionary = dictionary;
        }

        /// <summary>
        /// Add a member to a collection for a given key. Displays an error if the value already existed in the collection.
        /// </summary>
        public string Add(k key, v value)
        {
            if (_Dictionary.TryGetValue(key, out var dictionaryElement)) {
                if (dictionaryElement.Contains(value))
                {
                    throw new Exception($"Value Already exists in collection Value:{value}");
                }
                dictionaryElement.Add(value);
            }
            else
            {
                _Dictionary.Add(
                    key,
                    new HashSet<v> {
                        value
                    }
                );
            }
            

            return "Added";
        }

        /// <summary>
        ///  Returns all the values in the dictionary. Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        public IEnumerable<v> AllMembers()
        {
            var hashmaps = _Dictionary.Select(x => x.Value);
            var list = new List<v>();
            foreach (var map in hashmaps)
            {
                foreach (var item in map)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Removes all keys and all values from the dictionary.
        /// </summary>
        public void Clear()
        {
            _Dictionary = new Dictionary<k, HashSet<v>>();
        }

        /// <summary>
        ///  Returns whether a key exists or not.
        /// </summary>
        public bool KeyExists(k key)
        {
            if (_Dictionary.TryGetValue(key, out _))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns all the keys in the dictionary. Order is not guaranteed.
        /// </summary>
        public IList<k> Keys()
        {
            return _Dictionary.Select(x => x.Key).ToList();
        }

        /// <summary>
        /// Returns the collection of strings for the given key. Return order is not guaranteed. Returns an error if the key does not exists.
        /// </summary>
        public IList<v> Members(k key)
        {
            if (_Dictionary.TryGetValue(key, out var dictionaryElement))
            {
                return dictionaryElement.ToList();
            }

            throw new Exception($"Key does not exist in collection Key:{key}");
        }

        /// <summary>
        /// Removes a value from a key. If the last value is removed from the key, they key is removed from the dictionary. If the key or value does not exist, displays an error
        /// </summary>
        public string Remove(k key, v value)
        {
            if (_Dictionary.TryGetValue(key, out var dictionaryElement))
            {
                if (dictionaryElement.Contains(value))
                {
                    dictionaryElement.Remove(value);
                    if (dictionaryElement.Count() <= 0)
                    {
                        _Dictionary.Remove(key);
                    }
                    return "removed";
                }
                
                throw new Exception($"Value does not exist in collection Value:{value}");
            }
            
            throw new Exception($"Key does not exist in collection Key:{key}");
        }

        /// <summary>
        /// Removes all value for a key and removes the key from the dictionary. Returns an error if the key does not exist.
        /// </summary>
        public string RemoveAll(k key)
        {
            if (_Dictionary.TryGetValue(key, out _))
            {
                _Dictionary.Remove(key);
                return "removed";
            }

            throw new Exception($"Key does not exist in collection Key:{key}");
        }

        /// <summary>
        ///  Returns whether a value exists within a key. Returns false if the key does not exist
        /// </summary>
        public bool ValueExists(k key, v value)
        {
            if (_Dictionary.TryGetValue(key, out var dictionaryElement))
            {
                if(dictionaryElement.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///  Returns all keys in the dictionary and all of their values. Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        public IDictionary<k, HashSet<v>> Items()
        {
            return _Dictionary;
        }
    }
}
