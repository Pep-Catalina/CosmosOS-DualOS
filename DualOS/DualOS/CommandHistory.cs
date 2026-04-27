using System;
using System.Collections.Generic;
using System.Text;

namespace DualOS
{
    public class CommandHistory
    {
        private List<string> history = new List<string>();

        public void Add(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                return;
            }

            history.Add(command);

            if (history.Count > 5)
            {
                history.RemoveAt(0);
            }
        }

        public string GetHistoryText()
        {
            if (history.Count == 0)
            {
                return "No history available.";
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < history.Count; i++)
            {
                sb.AppendLine(i + ": " + history[i]);
            }

            return sb.ToString();
        }

        public void Show()
        {
            Console.WriteLine(GetHistoryText());
        }

        public string GetCommand(string input)
        {
            try
            {
                int index = int.Parse(input.Substring(1));

                if (index < 0 || index >= history.Count)
                {
                    return null;
                }

                return history[index];
            }
            catch
            {
                return null;
            }
        }
    }
}
