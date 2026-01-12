using System;
using static System.Console;

namespace CIS205_Master_Project.Assignments.Midterm
{
    internal class GPACalculator
    {
        static void Main()
        {
            Write("Welcome to the Flagler GPA Calculator \n");

            int totalCreditHours = 0;
            double totalQualityPoints = 0.0;
            int totalCourses = 0;
            int gradePoints;

            bool continueInput = true;
            
            while (continueInput)
            {

                string grade;
                while (true)
                {
                    Write("Enter course letter grade (A, B, C, D, F): ");
                    grade = ReadLine().Trim().ToUpper();

                    if (grade == "A" || grade == "B" || grade == "C" || grade == "D" || grade == "F")
                    {
                        break;
                    }
                    else
                    {
                        WriteLine("Invalid grade. Please enter A, B, C, D, or F.");
                    }
                }
                int creditHours;
                while (true)
                {
                    Write("Enter the number of credit hours for this course: ");
                    string input = ReadLine();

                    if (int.TryParse(input, out creditHours) && creditHours > 0)
                    {
                        break;
                    }
                    else
                    {
                        WriteLine("Invalid entry. Credit hours must be a positive integer.");
                    }
                }

                
                switch (grade)
                {
                    case "a":
                    case "A":
                        gradePoints = 4;
                        break;
                    case "b":
                    case "B":
                        gradePoints = 3;
                        break;
                    case "c":
                    case "C":
                        gradePoints = 2;
                        break;
                    case "d":
                    case "D":
                        gradePoints = 1;
                        break;
                    case "f":
                    case "F":
                        gradePoints = 0;
                        break;
                }

                double qualityPoints = gradePoints * creditHours;
                totalQualityPoints += qualityPoints;
                totalCourses++;
                totalCreditHours += creditHours;

                
                Write("\nDo you want to enter another course? (Y to continue, any other key to stop): ");
                string choice = ReadLine().Trim().ToUpper();
                if (choice != "Y")
                {
                    continueInput = false;
                }

             
            }

                double gpa = 0.0;
                if (totalCreditHours > 0)
                {
                    gpa = totalQualityPoints / totalCreditHours;
                }

            WriteLine("\n===========================================");
            WriteLine("             GPA Calculator Summary");
            WriteLine("===========================================\n");

            WriteLine("{0,-50}{1,10}", "Total number of courses:", totalCourses);
            WriteLine("{0,-50}{1,10}", "Total credit hours:", totalCreditHours);
            WriteLine("{0,-50}{1,10:F2}", "Total quality points:", totalQualityPoints);
            WriteLine("{0,-50}{1,10:F2}", "Calculated GPA (rounded to 2 decimals):", Math.Round(gpa, 2));

            WriteLine("\n-------------------------------------------");

            if (gpa >= 3.8)
            {
                WriteLine("Outstanding! You’re on the President’s List — keep up the excellent work!");
            }
            else if (gpa >= 3.5)
            {
                WriteLine("Great job! You’re on the Dean’s List — strong academic performance!");
            }
            else if (gpa >= 3.0)
            {
                WriteLine("Good work! You’re maintaining solid academic standing.");
            }
            else if (gpa >= 2.0)
            {
                WriteLine("Keep improving! You’re passing, but there’s room to grow.");
            }
            else
            {
                WriteLine("Warning: You’re below satisfactory progress. Seek academic support early.");
            }

            WriteLine("-------------------------------------------\n");


        }
    }
}
       

