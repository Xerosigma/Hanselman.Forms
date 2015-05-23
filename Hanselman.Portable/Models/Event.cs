using System;
using System.Collections.Generic;

namespace Hanselman.Portable
{
	public class Event
	{
		public string Name { get; set; }
		public string Location { get; set; }
		public int FriendsGoing { get; set; }
		public List<Person> Attendees { get; set; }

		public Event ()
		{
		}
	}
}

