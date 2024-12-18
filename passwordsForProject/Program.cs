using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

class Program
{
    static void Main()
    {
        string myPassword = "fastcars";
        //string saltedPassword = myPassword + randomString;
        string hashedPass = BCrypt.Net.BCrypt.HashPassword(myPassword,13);

        Console.WriteLine(hashedPass);

    }

} 