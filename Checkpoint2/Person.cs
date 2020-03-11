using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Checkpoint2
{
    public abstract class Person
    {
        public String Name { get; set; }
        public List<Event> Events { get; set; }

        public Person(String name)
        {
            Name = name;
            Events = new List<Event>();
        }

        public abstract override String ToString();
        
        public List<Event> DisplayEvents(string startDate, string endDate)
        {
            DateTime start = Event.ParseDate(startDate);
            DateTime finish = Event.ParseDate(endDate);
            List<Event> eventsToDisplay = new List<Event>();
            foreach (Event evento in Events)
            {
                if ((evento.StartTime > start) && (evento.EndTime < finish))
                {
                    Console.WriteLine("{0} - {1}. *Held on {2} - {3}", evento.Name, evento.Description, evento.StartTime, evento.EndTime);
                    eventsToDisplay.Add(evento);
                }
            }
            return eventsToDisplay;
        }
        public void FillEventList()
        {
            string procedure = "sp_eventsPerson";
            List<object[]> list = Database.GetEntriesFromDB(Name, procedure);
            foreach (object[] item in list)
            {
                Event evento = new Event();
                if (!(item[0] is DBNull))
                {
                    evento.Name = Convert.ToString(item[0]);
                }
                if (!(item[1] is DBNull))
                {
                    evento.StartTime = Convert.ToDateTime(item[1]);
                    evento.EndTime = evento.StartTime + TimeSpan.FromHours(20);
                }
                if (!(item[2] is DBNull))
                {
                    evento.Description = Convert.ToString(item[2]);
                }
                Events.Add(evento);
            }
        }
    }
}
