using System;
using static System.Console;
using System.Globalization;
using System.Collections.Generic;

namespace PerfromanceTrackerApp
{
    //RaceResult: Stores a single race result
    public class RaceResult
    {
        public string MeetName { get; set; }

        public string EventName { get; set; }

        public double DistanceMiles { get; set; }

        public TimeSpan Time { get; set; }

        public int Placement { get; set; }

        public RaceResult(string meetName, string eventName,
                          double distanceMiles, TimeSpan time, int placement)
        {
            MeetName = meetName;
            EventName = eventName;
            DistanceMiles = distanceMiles;
            Time = time;
            Placement = placement;
        }

        public TimeSpan CalculatePace()
        {
            double paceSeconds = Time.TotalSeconds / DistanceMiles;
            return TimeSpan.FromSeconds(paceSeconds);
        }

        public override string ToString()
        {
            return $"Meet: {MeetName}\n" +
                    $"Event: {EventName}\n" +
                    $"Distance: {DistanceMiles} miles\n" +
                    $"Time: {Time}\n" +
                    $"Pace: {CalculatePace()}\n" +
                    $"Placement: {Placement}";
        }
    }

    //Athlete CLASS: Stores athlete name + list of race results

    public class Athlete
    {
        public string Name { get; set; }
        public List<RaceResult> Results { get; set; } = new List<RaceResult>();

        public Athlete(string name)
        {
            Name = name;
        }

        //Personal Record = fastest time
        public RaceResult GetPR()
        {
            if (Results.Count == 0) return null;
            RaceResult pr = Results[0];

            foreach (var r in Results)
            {
                if (r.Time < pr.Time)
                    pr = r;
            }
            return pr;
        }

        public double GetAverageSeconds()
        {
            double sum = 0;
            foreach (var r in Results)
                sum += r.Time.TotalSeconds;

            return sum / Results.Count;
        }
    }

    //MAIN PROGRAM

    public class Program
    {
        //Store athletes in a list
        static List<Athlete> athletes = new List<Athlete>();

        //Get event type using single array
        static string[] eventTypes = { "5K", "3200m", "1600m", "800m", "10K" };
        public static void Main()
        {
            RunMenu();
        }

        static void RunMenu()
        {
            int choice = 0;

            while (choice != 7)
            {
                WriteLine("\n RACE TRACKER MENU ");
                WriteLine("1. Add Athlete");
                WriteLine("2. Add a Race Result");
                WriteLine("3. View All Athletes + Results");
                WriteLine("4. Show Athlete's PR");
                WriteLine("5. Compare Two Athletes");
                WriteLine("6. Show Improvement for One Athlete");
                WriteLine("7. Exit program");

                Write("Enter choice: ");
                int.TryParse(ReadLine(), out choice);

                switch (choice)
                {
                    case 1: AddAthlete(); break;
                    case 2: AddRaceResult(); break;
                    case 3: ViewResults(); break;
                    case 4: ShowPR(); break;
                    case 5: CompareAthletes(); break;
                    case 6: ShowImprovement(); break;
                    case 7: WriteLine("Goodbye!"); break;
                    default: WriteLine("Invalid choice."); break;
                }
            }
        }

        //Add Athlete
        static void AddAthlete()
        {
            Write("Enter athlete name: ");
            string name = ReadLine();

            athletes.Add(new Athlete(name));
            WriteLine($"Athlete '{name}' added.");
        }

        //Add Race Result to a specific athlete

        static void AddRaceResult()
        {
            if (athletes.Count == 0)
            {
                WriteLine("Add an athlete first.");
                return;
            }

            WriteLine("\nSelect athlete:");
            for (int i = 0; i < athletes.Count; i++)
                WriteLine($"{i + 1}. {athletes[i].Name}");

            int index = int.Parse(ReadLine()) - 1;
            Athlete athlete = athletes[index];

            Write("Meet name: ");
            string meet = ReadLine();

            WriteLine("\nSelect event type:");
            for (int i = 0; i < eventTypes.Length; i++)
                WriteLine($"{i + 1}. {eventTypes[i]}");

            int eventChoice;
            while (!int.TryParse(ReadLine(), out eventChoice) ||
                   eventChoice < 1 ||
                   eventChoice > eventTypes.Length)
            {
                WriteLine("Invalid choice. Try again:");
            }

            string eventName = eventTypes[eventChoice - 1];

            Write("Distance in miles: ");
            double distance = double.Parse(ReadLine());

            Write("Enter finishing time (mm:ss): ");
            TimeSpan time = TimeSpan.ParseExact(ReadLine(), @"m\:ss", CultureInfo.InvariantCulture);

            Write("Placement: ");
            int place = int.Parse(ReadLine());

            athlete.Results.Add(new RaceResult(meet, eventName, distance, time, place));

            WriteLine("Race added successfully!");
        }

        //View all athletes + their results

        static void ViewResults()
        {
            if (athletes.Count == 0)
            {
                WriteLine("No athletes added yet.");
                return;
            }

            foreach (var a in athletes)
            {
                WriteLine($"\n===== {a.Name} =====");
                if (a.Results.Count == 0)
                {
                    WriteLine("No race results yet.");
                }
                else
                {
                    for (int i = 0; i < a.Results.Count; i++)
                    {
                        WriteLine($"\nRace #{i + 1}");
                        WriteLine(a.Results[i]);
                    }
                }
            }
        }

        //Show PR for a selected athlete

        static void ShowPR()
        {
            if (athletes.Count == 0) { WriteLine("No athletes yet."); return; }

            WriteLine("Select athlete:");
            for (int i = 0; i < athletes.Count; i++)
                WriteLine($"{i + 1}. {athletes[i].Name}");

            Athlete a = athletes[int.Parse(ReadLine()) - 1];
            RaceResult pr = a.GetPR();

            if (pr == null)
            {
                WriteLine("This athlete has no races yet.");
                return;
            }

            WriteLine($"\n--- {a.Name}'s Personal Record ---");
            WriteLine(pr);
        }

        //Compare two athletes

        static void CompareAthletes()
        {
            if (athletes.Count < 2)
            {
                WriteLine("Not enough athletes to compare.");
                return;
            }

            WriteLine("Select first athlete:");
            for (int i = 0; i < athletes.Count; i++)
                WriteLine($"{i + 1}. {athletes[i].Name}");
            Athlete a1 = athletes[int.Parse(ReadLine()) - 1];

            WriteLine("Select second athlete:");
            for (int i = 0; i < athletes.Count; i++)
                WriteLine($"{i + 1}. {athletes[i].Name}");
            Athlete a2 = athletes[int.Parse(ReadLine()) - 1];

            RaceResult pr1 = a1.GetPR();
            RaceResult pr2 = a2.GetPR();

            WriteLine("\n Athlete Comparison \n");
            WriteLine($"{a1.Name} PR: {(pr1 != null ? pr1.Time.ToString() : "N/A")}");
            WriteLine($"{a2.Name} PR: {(pr2 != null ? pr2.Time.ToString() : "N/A")}");
            WriteLine();

            if (pr1 != null && pr2 != null)
            {
                if (pr1.Time < pr2.Time)
                    WriteLine($"{a1.Name} is faster.");
                else if (pr2.Time < pr1.Time)
                    WriteLine($"{a2.Name} is faster.");
                else
                    WriteLine("They have identical PRs!");
            }

            WriteLine("\n========================================");
        }

        //Show Improvement for a selected athlete

        static void ShowImprovement()
        {
            if (athletes.Count == 0)
            {
                WriteLine("No athletes added yet.");
                return;
            }

            WriteLine("Select athlete:");
            for (int i = 0; i < athletes.Count; i++)
                WriteLine($"{i + 1}. {athletes[i].Name}");
            Athlete a = athletes[int.Parse(ReadLine()) - 1];

            if (a.Results.Count < 2)
            {
                WriteLine("Not enough races to show improvement.");
                return;
            }

            WriteLine($"\n--- {a.Name}'s Improvement ---");

            for (int i = 1; i < a.Results.Count; i++)
            {
                TimeSpan diff = a.Results[i - 1].Time - a.Results[i].Time;
                WriteLine($"Race {i} → Race {i + 1}: Change = {diff}");
            }
        }
    }
}
