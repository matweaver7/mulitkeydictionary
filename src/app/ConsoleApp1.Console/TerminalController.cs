using ConsoleApp1.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Terminal
{
    class TerminalController : ITerminal
    {
        IDictionaryService<string, string> _DictionaryService;
        bool isValidState;
        public TerminalController (IDictionaryService<string, string> dictionaryService)
        {
            _DictionaryService = dictionaryService;
            isValidState = true;
        }
        //todo: this is the weak point. I'd need better requirements listed
        // also remove magic numbers. Should be put in an enum. No time.
        public void Run()
        {
            while (isValidState)
            {
                Console.Out.Flush();
                var command = splitIntoArgs(Console.ReadLine());
                if (command.Length > 0)
                {
                    switch (command[0].ToLower())
                    {
                        case "add":
                            if (validArgs(command, 3))
                            {
                                var test = _DictionaryService.Add(command[1], command[2]);
                                Console.WriteLine(test);
                            }
                            break;
                        case "members":
                            if (validArgs(command, 2))
                            {
                                var list = _DictionaryService.Members(command[1]);
                                foreach (var item in list)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            break;
                        case "remove":
                            if (validArgs(command, 3))
                            {
                                var test = _DictionaryService.Remove(command[1], command[2]);
                                Console.WriteLine(test);
                            }
                            break;
                        case "keys":
                            if (validArgs(command, 1))
                            {
                                var list = _DictionaryService.Keys();
                                foreach (var item in list)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            break;
                        case "removeall":
                            if (validArgs(command, 2))
                            {
                                var test = _DictionaryService.RemoveAll(command[1]);
                                Console.WriteLine(test);
                            }
                            break;
                        case "clear":
                            if (validArgs(command, 1))
                            {
                                var test = _DictionaryService.Clear();
                                Console.WriteLine(test);
                            }
                            break;
                        case "keyexists":
                            if (validArgs(command, 2))
                            {
                                var test = _DictionaryService.KeyExists(command[1]);
                                Console.WriteLine(test);
                            }
                            break;
                        case "valueexists":
                            if (validArgs(command, 3))
                            {
                                var test = _DictionaryService.ValueExists(command[1], command[2]);
                                Console.WriteLine(test);
                            }
                            break;
                        case "allmembers":
                            if (validArgs(command, 1))
                            {
                                var list = _DictionaryService.AllMembers();
                                foreach (var item in list)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            break;
                        case "items":
                            if (validArgs(command, 1))
                            {
                                var list = _DictionaryService.Items();
                                foreach (var item in list)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            break;
                        case "exit":
                            isValidState = false;
                            break;
                        default:
                            Console.WriteLine("To exit type 'exit'");
                            break;
                    }
                }
            }
        }

        private bool validArgs(string[] args, int length)
        {
            if (args.Length != length)
            {
                Console.WriteLine("invalid number of arguments");
                return false;
            }
            return true;

        }

        private string[] splitIntoArgs(string args)
        {
            return args.Split(' ');
        }
    }
}
