using System;
using System.Collections.Generic;

public class LL1Parser
{
    private ParseTable parseTable;
    private Stack<string> stack;

    public LL1Parser(ParseTable parseTable)
    {
        this.parseTable = parseTable;
    }

    public bool Parse(string input)
    {
        stack = new Stack<string>();
        stack.Push("$");
        stack.Push("S");

        var inputTokens = input.Split(' ').ToList();
        inputTokens.Add("$");

        int index = 0;
        while (stack.Count > 0)
        {
            var top = stack.Peek();
            var currentToken = inputTokens[index];

            if (parseTable.IsTerminal(top))
            {
                if (top == currentToken)
                {
                    stack.Pop();
                    index++;
                }
                else
                {
                    Console.WriteLine($"Erro: Esperado '{top}', mas encontrado '{currentToken}'");
                    return false;
                }
            }
            else
            {
                var production = parseTable.GetProduction(top, currentToken);
                if (production != null)
                {
                    stack.Pop();
                    var symbols = production.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = symbols.Length - 1; i >= 0; i--)
                    {
                        if (symbols[i] != "ε")
                        {
                            stack.Push(symbols[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Erro: Nenhuma produção encontrada para '{top}' com entrada '{currentToken}'");
                    return false;
                }
            }
        }

        return index == inputTokens.Count - 1;
    }
}
