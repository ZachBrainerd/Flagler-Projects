using System;
using static System.Console;

namespace CIS205_Master_Project.Assignments.Project_5
{
    internal class ResortPrices
    {
        static void Main()
        {
            int[] nights = { 1, 3, 5, 8 };
            double[] prices = { 200.0, 180.0, 160.0, 145.0 }; //Night rates

            //Input
            Write("Enter the number of nights for your resort stay: ");
            int stayNights = Convert.ToInt32(ReadLine());

            //Find the correct nightly rate
            double ratePerNight = prices[0]; 
            for (int i = nights.Length - 1; i >= 0; i--) 
            {
                if (stayNights >= nights[i])
                {
                    ratePerNight = prices[i];
                    break;
                }
            }

            if (stayNights <= 0)
            {
                WriteLine("Error: Number of nights must be greater than zero.");
                return;
            }

            //Total price
            double totalPrice = stayNights * ratePerNight;

            //Results
            WriteLine($"Number of nights {stayNights}");
            WriteLine($"Price per night is ${ratePerNight:F2}");
            WriteLine($"Total for {stayNights} night(s) ${totalPrice:F2}");

            //If more than 2 nights, show how much money they saved 
            double baseRate = prices[0]; 
            if (stayNights > 2 && ratePerNight < baseRate)
            {
                double savingsPerNight = baseRate - ratePerNight;
                double totalSavings = savingsPerNight * stayNights;

                WriteLine($"\nYou saved ${savingsPerNight:F2} per night!");
                WriteLine($"Total savings for your stay: ${totalSavings:F2}");
            }

            //Exit the application
            WriteLine("\nPress any key to exit...");
            ReadKey();

        }     
     }
}
