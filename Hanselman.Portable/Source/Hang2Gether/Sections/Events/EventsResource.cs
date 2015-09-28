using System;
using System.Collections.Generic;

using Ecom.Hal;
using Ecom.Hal.Attributes;

namespace Hanselman.Portable
{
	public class EventsResource : HalResource
	{
		[HalEmbedded("events")]
		public List<Event> Events { get; set; }

		public EventsResource ()
		{
		}
	}
}

