// See https://aka.ms/new-console-template for more information

using utilities;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello.");

        var tt = new utilities.LinkedList<int>();

        tt.Add(3);
        tt.Add(4);
        tt.Add(5);

        Console.WriteLine(tt.Length());
    }
}