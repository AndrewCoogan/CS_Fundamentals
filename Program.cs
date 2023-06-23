// See https://aka.ms/new-console-template for more information

using utilities;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        // I am going to use this to test stuff, Ill make legit testing later.
        Console.WriteLine("Linked List Test.");

        var linkedList = new utilities.LinkedList<int>();

        linkedList.Add(3);
        linkedList.Add(4);
        linkedList.Add(5);
        linkedList.Add(6);
        linkedList.Print();
        Console.WriteLine(linkedList.Length());
    }
}