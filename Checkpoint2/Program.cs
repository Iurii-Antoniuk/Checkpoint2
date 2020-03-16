using System;
using System.Collections.Generic;
using CommandLine;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Concurrent;

namespace Checkpoint2
{
    public class Program
    {
        [Verb("events", HelpText = "Get events for a specified period")]
        class EventOptions
        {
            [Option('n', "name", Required = true, HelpText = "Person's name")]
            public String Name { get; set; }

            [Option('b', "begin", Required = true, HelpText = "Start date")]
            public String StartDate { get; set; }

            [Option('e', "end", Required = true, HelpText = "End date")]
            public String EndDate { get; set; }

        }

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<EventOptions>(args)
                 .WithParsed<EventOptions>(RunEventOptions);


            //Person stud = new Student("Student 18");
            //stud.FillEventList();
            //stud.DisplayEvents("2019-01-05", "2020-08-15");

            //Console.WriteLine();

            //Person prof = new Prof("Mestre Bimba");
            //prof.FillEventList();
            //prof.DisplayEvents("2019-01-05", "2020-02-15");

            //Event newEvent = new Event("Important meeting");
            //newEvent.StartTime = DateTime.Now;
            //newEvent.EndTime = DateTime.Now + TimeSpan.FromHours(1);
            //newEvent.Postpone(TimeSpan.FromHours(1));
            //Console.WriteLine("Another meeting is postponed");
        }

        static void RunEventOptions(EventOptions opts)
        {
            Person pers = PersonFactory.Create(opts.Name);
            pers.FillEventList();
            DateTime start = ParseDate(opts.StartDate);
            DateTime finish = ParseDate(opts.EndDate);
            List<Event> eventToDisplay = pers.GetEventsByDate(start, finish);
            foreach (Event evento in eventToDisplay)
            {
                Console.WriteLine("{0} - {1}. *Held on {2} - {3}", evento.Name, evento.Description, evento.StartTime, evento.EndTime);
            }
            
        }

        private static DateTime ParseDate(string eventDate)
        {

            DateTime dateValue = DateTime.ParseExact(eventDate,
                                        "yyyy-MM-dd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None); ;
            return dateValue;
        }
    }
}
