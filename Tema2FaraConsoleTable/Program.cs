using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Incercare2Tema2
{
    public class Tables
    {
        public Dictionary<string, string> ActionTable { get; set; }
        public Dictionary<string, string> GotoTable { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Calea către fișierele JSON
            string actionTableJson = File.ReadAllText("Resources/tables.json");
            string inputJson = File.ReadAllText("Resources/input.json");

            // Deserializarea fișierelor JSON
            Tables tables = JsonConvert.DeserializeObject<Tables>(actionTableJson);
            InputModel inputModel = JsonConvert.DeserializeObject<InputModel>(inputJson);

            // Afișează tabelele de acțiune și Goto pentru debugging
            Console.WriteLine("ActionTable:");
            foreach (var entry in tables.ActionTable)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            Console.WriteLine("GotoTable:");
            foreach (var entry in tables.GotoTable)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            // Inputul pentru parsare
            string input = "i+i*i"; // Poți schimba acest input pentru a testa diferite expresii

            // Apelarea funcției de parsare
            LRParser.Parse(inputModel, new Table(tables.ActionTable, tables.GotoTable), input);
        }
    }
}
