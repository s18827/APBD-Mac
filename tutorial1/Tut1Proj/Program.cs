using System; // like import in Java
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Collections.Generic;
//using static System.Console; // for command line Console.Write

namespace Tutorial1Proj
{
    public class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            //SHIFT + ALT + F - allign code
            //int age = 20;
            //bool b = true;

            // Integer - used in order a variable can be nullable (be = null)
            //Integer in C# = int?
            //int? c = null;
            //string d = "hi";

            ////cw, TAB, TAB - short for Console.Write

            //program that prints every email written on a webpage

            //var response = await httpClient.GetAsync(url); // asynchronous method - doesn't wait for response

            //FUN WITH COMMAND LINE ARGS:
            /*
            if (args.Length == 1)
            {
                WriteLine($"There is {args[0]} to be checked for contained emails.");
            }
            else
            {
                WriteLine($"There are {args.Length} pages to be checked for contained emails.");
            }
            for (int i = 0; i < args.Length; i++) //no need for exception handling
            {
                System.Console.WriteLine(i+1 + ". " + args[i] + ": ");
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(args[i])) // using(){} - automatically manages resources - like finally in Java
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var regex = new Regex("[a-z]+[a-z0-9]*@[a-z]+\\.[a-z]+", RegexOptions.IgnoreCase);

                        var matches = regex.Matches(content); //Matches finds multiple occurances, Match - just one

                        foreach (var match in matches) // foreach + TAB + TAB - shortcut
                        {
                            Console.WriteLine(match.ToString());
                        }
                    }
                }
                System.Console.WriteLine();
            }
            */

            //task 3 & 4
            //var webAddress = new string("https://www.pja.edu.pl"); // dor debugging
            var emailAddressList = new List<string>();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(args[0])) // using(){} - automatically manages resources - like finally in Java
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        var regex = new Regex("[a-z]+[a-z0-9]*@[a-z]+\\.[a-z]+", RegexOptions.IgnoreCase);

                        var matches = regex.Matches(content); //Matches finds multiple occurances, Match - just one

                        foreach (var match in matches) // foreach + TAB + TAB - shortcut
                        {
                            //Console.WriteLine(match.ToString());
                            if(!emailAddressList.Contains(match.ToString()))
                            {
                            emailAddressList.Add(match.ToString());
                            }
                        }
                        foreach (var email in emailAddressList)
                        {
                            Console.WriteLine(email);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"{ex.GetType()} says {ex.Message}");
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                Console.WriteLine($"{ex.GetType()} says Error while downloading the page");
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine($"{ex.GetType()} says No email addresses found");
            }
        }

    }
}