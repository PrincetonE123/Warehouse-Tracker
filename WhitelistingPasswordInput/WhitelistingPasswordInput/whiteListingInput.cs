// See https://aka.ms/new-console-template for more information


using System.Collections.Generic;
using System.Linq;

/*
Whitelist should include
A-Z, @,%,


Do not include in whitelist
=, 1, 0, \, /, !, &

 */

class GUI
{
    //Checks an input string against a set of characters, symbols, and numbers. 
    // Outputs to Console if the inputted string contains or does not contain the elements in the set.
    //The set is includes the English alaphabet by default; it is hardcoded.
    static bool checkInputAgainstWhitlelist(string userInput, char[] whitelistedChars)
    {


        // whiteList contains all lowercase and uppercase english letters and the symbols given in the 2nd.Concat()

        //Range('a', 26) is creates the numebers used in ACSII to represent characters. 
        //.Select( n => (char)n converts these numbers into characters.
        var whitelist = Enumerable.Range('a', 26).Select(n => (char)n)
            .Concat(Enumerable.Range('A', 26).Select(n => (char)n))
            .Concat(whitelistedChars)
            .ToHashSet();   //Makes effort for comparing a single character against whitelist equal O(1),
                            // instead of O(length of whitelist) for validating a single input character

        for (int i = 0; i < userInput.Length; i++)
        {

            if (!whitelist.Contains(userInput[i]))
            {

                Console.Write("\nInput contains an invalid character: " + userInput[i].ToString() + " at position " + (i + 1).ToString() + "!");

                return false;
            }
        }

        return true;

    }

    static void Main()
    {
        char[] whitelistedChars = { '2', '3', '4', '5', '6', '7', '8', '9' }; //Doesn't have any symbols.

        Console.WriteLine("please enter a string to be validated. \nThis program should be able to accept any type of text encoding that may be used later.\n\n" +
            "Acceptable input can include the English alaphabet, and the numbers and symbols given here: " + new string(whitelistedChars));
        string? userInput = Console.ReadLine();


        if (userInput == null)
        {
            Console.WriteLine("User input null");
        }
        else if (userInput == "")
        {
            Console.WriteLine("User has not given input.");
        }
        else if (checkInputAgainstWhitlelist(userInput, whitelistedChars) == true)
        {
            Console.WriteLine("Input valid");
        }
    }
}