using System.Collections.Generic;
using ConsoleApp1.Contracts.Interfaces;

namespace ConsoleApp1.Data.Interfaces
{
    /// <summary>
    /// Add a member to a collection for a given key. Displays an error if the value already existed in the collection.
    /// </summary>
    public interface ICustomDictionary<k, v>
    {
        /// <summary>
        /// Add a member to a collection for a given key. Displays an error if the value already existed in the collection.
        /// </summary>
        string Add(k key, v value);

        /// <summary>
        /// Removes a value from a key. If the last value is removed from the key, they key is removed from the dictionary. If the key or value does not exist, displays an error
        /// </summary>
        string Remove(k key, v value);

        /// <summary>
        /// Removes all value for a key and removes the key from the dictionary. Returns an error if the key does not exist.
        /// </summary>
        string RemoveAll(k key);

        /// <summary>
        /// Removes all keys and all values from the dictionary.
        /// </summary>
        void Clear();

        /// <summary>
        /// Returns the collection of strings for the given key. Return order is not guaranteed. Returns an error if the key does not exists.
        /// </summary>
        IList<v> Members(k key);

        /// <summary>
        /// Returns all the keys in the dictionary. Order is not guaranteed.
        /// </summary>
        IList<k> Keys();

        /// <summary>
        ///  Returns whether a key exists or not.
        /// </summary>
        bool KeyExists(k key);

        /// <summary>
        ///  Returns whether a value exists within a key. Returns false if the key does not exist
        /// </summary>
        bool ValueExists(k key, v value);

        /// <summary>
        ///  Returns all the values in the dictionary. Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        IEnumerable<v> AllMembers();

        /// <summary>
        ///  Returns all keys in the dictionary and all of their values. Returns nothing if there are none. Order is not guaranteed.
        /// </summary>
        IDictionary<k, HashSet<v>> Items();
    }
}
