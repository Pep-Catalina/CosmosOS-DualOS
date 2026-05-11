using System;
using System.Collections.Generic;
using System.Text;

namespace DualOS
{
    public class CommandHistory
    {
        // Llista on es guarden les últimes comandes executades
        private List<string> history = new List<string>();

        // Afegeix una nova comanda a l'historial

        public void Add(string command)
        {
            // Comprova que la comanda no sigui buida
            if (string.IsNullOrWhiteSpace(command))
            {
                return;
            }

            // Guarda la comanda a la llista
            history.Add(command);

            if (history.Count > 5)
            {
                history.RemoveAt(0);
            }
        }

        // Retorna l'historial en format text
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

        // Mostra l'historial directament per consola
        public void Show()
        {
            Console.WriteLine(GetHistoryText());
        }

        // Obté una comanda concreta a partir d'un input
        // Exemple: "!2" retorna la comanda de la posició 2
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
