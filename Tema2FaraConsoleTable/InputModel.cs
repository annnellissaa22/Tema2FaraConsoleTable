using System;
using System.Collections.Generic;

public class InputModel
{
    public List<Production> Productions { get; set; }
    public List<string> NonTerminals { get; set; }
    public List<string> Terminals { get; set; }
    public string StartSymbol { get; set; }
}

public class Production
{
    public string Left { get; set; }
    public string Right { get; set; }
}
