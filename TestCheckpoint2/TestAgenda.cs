using NUnit.Framework;
using System;
using Checkpoint2;
using System.Collections.Generic;
using System.Linq;

namespace TestCheckpoint2
{
    [TestFixture]
    public class TestAgenda
    {
		private Person _prof;
		private Event event1;
		private Event event2;
		private Event event3;

		[SetUp]
		public void SetUp()
		{
			_prof = new Prof("Marabou");

			event1 = new Event();
			event1.Name = "Caramba Day";
			event1.StartTime = new DateTime(2015, 5, 1);
			event1.EndTime = new DateTime(2015, 5, 2);
			event1.Description = "Come feel the Caramba vibe today, Hohohoh!!!!";
			_prof.Events.Add(event1);

			event2 = new Event();
			event2.Name = "Groundhog Day";
			event2.StartTime = new DateTime(2019, 2, 2);
			event2.EndTime = new DateTime(2019, 2, 3);
			event2.Description = "Come witness the little furry bastard see its shadow. Or not";
			_prof.Events.Add(event2);

			event3 = new Event();
			event3.Name = "Freshers defloration";
			event3.StartTime = new DateTime(2020, 9, 2);
			event3.EndTime = new DateTime(2020, 9, 3);
			event3.Description = "College student freshers wanted urgently audition start new hindi Tv";
			_prof.Events.Add(event3);
		}

		[Test]
		public void TestAgendaForAPeriod()
		{
			List<Event> returnedEvents = _prof.GetEventsByDate(new DateTime(2017, 2, 2), new DateTime(2023, 2, 2));
			List<Event> expectedEvents = new List<Event>() { event2, event3 };
			Assert.That(returnedEvents, Is.EquivalentTo(expectedEvents));
		}
	}
}
