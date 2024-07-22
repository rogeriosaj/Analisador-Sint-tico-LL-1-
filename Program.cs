using System;

class Program
{
    static void Main(string[] args)
    {
        var parseTable = new ParseTable("parse_table.csv");
        var parser = new LL1Parser(parseTable);

        Console.WriteLine("Digite a entrada:");
        var input = Console.ReadLine();

        if (parser.Parse(input))
        {
            Console.WriteLine("Entrada aceita com sucesso!");
        }
        else
        {
            Console.WriteLine("Erro ao processar a entrada.");
        }
    }
}
