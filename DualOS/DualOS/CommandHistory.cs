using System;
using System.Collections.Generic;

namespace DualOS
{
    public class CommandHistory
    {
        private List<string> history = new List<string>();

        public void Add(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return;

            history.Add(command);

            if (history.Count > 5)
                history.RemoveAt(0);
        }

        public void Show()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No history available.");
                return;
            }

            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"{i}: {history[i]}");
            }
        }

        public string GetCommand(string input)
        {
            try
            {
                int index = int.Parse(input.Substring(1));

                if (index < 0 || index >= history.Count)
                {
                    Console.WriteLine("Invalid history index.");
                    return null;
                }

                return history[index];
            }
            catch
            {
                Console.WriteLine("Invalid history format.");
                return null;
            }
        }
    }
}