using System;

using Core;

using Ecom.Hal.JSON;

namespace Hanselman.Portable
{
	public class EventRepository : RepositoryBase<EventsResource>
	{
		public override string RESOURCE_URL { get{ return "http://10.0.0.43:8080/events"; } }

		public EventRepository() : base(new HalResourceConverter())
		{
		}
	}
}

