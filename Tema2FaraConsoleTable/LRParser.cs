using System;
using System.Collections.Generic;
using System.Linq;

public static class LRParser
{
    public static void Parse(InputModel model, Table tables, string input)
    {
        Stack<(char, int)> stack = new();
        stack.Push(('$', 0)); // Stiva începe cu simbolul de final '$' și starea 0
        input += '$'; // Adăugăm simbolul de final în input
        var actionTable = tables.ActionTable;
        var gotoTable = tables.GotoTable;

        while (true)
        {
            var currentS = stack.Peek();
            var currentIn = input[0];

            var actionKey = (currentS.Item2.ToString(), currentIn.ToString());
            string action;

            // Verificăm acțiunea corespunzătoare pentru combinația (stare, simbol)
            if (actionTable.TryGetValue(actionKey.ToString(), out action))
            {
                if (action[0] == 'd') // 'd' pentru deplasare (shift)
                {
                    int nextState = int.Parse(action.Substring(1));
                    stack.Push((currentIn, nextState)); // Push pe stivă simbolul și starea
                    input = input[1..]; // Consumăm primul caracter din input
                    Console.WriteLine($"Deplasare: {currentIn} | Stiva: {string.Join(" ", stack.Reverse())}");
                }
                else if (action[0] == 'r') // 'r' pentru reducere (reduce)
                {
                    int productionNumber = int.Parse(action.Substring(1));
                    var production = model.Productions[productionNumber - 1];
                    Console.WriteLine($"Reducere: {production.Left} -> {production.Right}");

                    // Pop pe stivă pentru simbolurile din partea dreaptă a producției
                    for (int i = 0; i < production.Right.Length; i++)
                    {
                        stack.Pop();
                    }

                    // Obținem starea curentă de pe vârful stivei
                    var topState = stack.Peek().Item2;

                    // Obținem cheia pentru tabela de 'goto' pe baza non-terminalului
                    var gotoKey = (topState.ToString(), production.Left);
                    string newStateStr = gotoTable[gotoKey.ToString()];
                    int newState = int.Parse(newStateStr);

                    // Push pe stivă non-terminalul și noua stare
                    stack.Push((production.Left[0], newState));
                    Console.WriteLine($"Reducere finalizată: Stiva: {string.Join(" ", stack.Reverse())}");
                }
                else if (action == "acc") // 'acc' pentru acceptare
                {
                    Console.WriteLine("Input acceptat.");
                    break;
                }
            }
            else
            {
                // Dacă nu găsim cheia în tabelul de acțiune
                Console.WriteLine($"[ERORARE] Cheia {actionKey} nu există în tabelul de acțiune.");
                break;
            }
        }
    }
}
