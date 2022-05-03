using System;
using System.Collections.Generic;
using System.Threading;
using Driver;
namespace CS_Sample_Term
{
    static class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Driver.Program driver = new Driver.Program();
            // TODO: edit to only prompt for commands when connection rescode is successful
            driver.Connect();
            Console.WriteLine("Enter a steam of 5 commands at a time. Enter quit to terminate at any time.");
            for(int i = 0; i < args.Length; i++)
            {
                driver.MoveCommand(args[i]);
                // sleep for 10 seconds
                Thread.Sleep(10000);
            }

            driver.CloseConnect();
            Environment.Exit(0);
        }
    }
}