using System;
using System.Collections.Generic;

public class Table
{
    public Dictionary<string, string> ActionTable { get; set; }
    public Dictionary<string, string> GotoTable { get; set; }

    public Table(Dictionary<string, string> actionTable, Dictionary<string, string> gotoTable)
    {
        ActionTable = actionTable;
        GotoTable = gotoTable;
    }
}
