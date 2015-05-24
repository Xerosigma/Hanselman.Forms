using System;
using System.Collections.Generic;

using Ecom.Hal.Attributes;
using Ecom.Hal.JSON;
using Ecom;

namespace Hanselman.Portable
{
	public class Event /*: HalResource*/
	{
		public string Name { get; set; }
		public string Location { get; set; }

		//[HalEmbedded("attendees")]
		public List<Attendee> Attendees { get; set; }
	}
}

