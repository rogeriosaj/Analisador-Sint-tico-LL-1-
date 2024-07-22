using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ParseTable
{
    private Dictionary<string, Dictionary<string, string>> table;

    public ParseTable(string csvFilePath)
    {
        table = new Dictionary<string, Dictionary<string, string>>();
        LoadFromCsv(csvFilePath);
    }

    private void LoadFromCsv(string csvFilePath)
    {
        var lines = File.ReadAllLines(csvFilePath);
        var header = lines[0].Split(',').Select(x => x.Trim()).ToArray();

        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',').Select(x => x.Trim()).ToArray();
            var nonTerminal = parts[0];
            table[nonTerminal] = new Dictionary<string, string>();

            for (int j = 1; j < parts.Length; j++)
            {
                if (!string.IsNullOrEmpty(parts[j]))
                {
                    table[nonTerminal][header[j]] = parts[j];
                }
            }
        }
    }

    public string GetProduction(string nonTerminal, string terminal)
    {
        if (table.ContainsKey(nonTerminal) && table[nonTerminal].ContainsKey(terminal))
        {
            return table[nonTerminal][terminal];
        }
        return null;
    }

    public bool IsTerminal(string symbol)
    {
        return !table.ContainsKey(symbol);
    }
}
