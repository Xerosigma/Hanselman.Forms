using System;
using System.Collections.Generic;

using Ecom.Hal;
using Ecom.Hal.Attributes;

namespace Hanselman.Portable
{
	public class Event : HalResource
	{
		public string Name { get; set; }
		public string Location { get; set; }

		[HalEmbedded("attendees")]
		public List<Attendee> Attendees { get; set; }
	}
}

