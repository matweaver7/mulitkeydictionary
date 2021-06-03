using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Contracts.Interfaces;
using ConsoleApp1.Data.Interfaces;

namespace ConsoleApp1.Application
{
    // NOTE: I don't love this implementation the application. I feel like a factory might be better here
    // but given the project criteria, this would be fine and a factory would be overkill
    // plus if you needed it it would be easy to adjust to handle an expansion of the service to support multiple types dynmically
    // passing the type as a parameter to the service and the factory would dynamically create it if not already existant
    public class DictionaryService : IDictionaryService<string, string>
    {
        ICustomDictionary<string, string> _DictionaryRepo;

        public DictionaryService(ICustomDictionary<string, string> dictionaryRepo)
        {
            _DictionaryRepo = dictionaryRepo;
        }

        public string Add(string key, string value)
        {
            return _DictionaryRepo.Add(key, value);
        }

        public IEnumerable<string> AllMembers()
        {
            var values = _DictionaryRepo.AllMembers();
            if (values.Count() <= 0)
            {
                return new string[1]{ "no members" };
            }
            return values.Select((x, index) => $"{index + 1}) {x}");
        }

        public string Clear()
        {
            _DictionaryRepo.Clear();

            return "Cleared";
        }

        public IEnumerable<string> Items()
        {
            var list = new List<string>();
            
            var dictionary = _DictionaryRepo.Items();
            if (dictionary.Count() <= 0)
            {
                return new string[1] { "no items" };
            }
            var index = 0;

            foreach (var map in dictionary)
            {
                foreach (var item in map.Value)
                {
                    list.Add($"{index + 1}) {map.Key}: {item}");
                    index++;
                }
            }

            return list;
        }

        public string KeyExists(string key)
        {
            return _DictionaryRepo.KeyExists(key).ToString();
        }

        public IEnumerable<string> Keys()
        {
            var keys = _DictionaryRepo.Keys();
            if (keys.Count() <= 0)
            {
                return new string[1] { "no keys" };
            }
            var newKeys = keys.Select((x, index) =>
            {
                return $"{index+1}) {x}";
            });
            return newKeys;
        }

        public IEnumerable<string> Members(string key)
        {
            var members = _DictionaryRepo.Members(key);
            if (members.Count() <= 0)
            {
                return new string[1] { "no members" };
            }
            return members.Select((x, index) => $"{index}) {x}");
        }

        public string Remove(string key, string value)
        {
            return _DictionaryRepo.Remove(key, value);
        }

        public string RemoveAll(string key)
        {
            return _DictionaryRepo.RemoveAll(key);
        }

        public string ValueExists(string key, string value)
        {
            return _DictionaryRepo.ValueExists(key, value).ToString();
        }
    }
}
