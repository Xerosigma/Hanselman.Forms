using System;

using Core;

using Ecom.Hal.JSON;

namespace Hanselman.Portable
{
	public class EventRepository : RepositoryBase<Events>
	{
		public override string RESOURCE_URL { get{ return "http://localhost:8080/events"; } }

		public EventRepository() : base(new HalResourceConverter())
		{
		}
	}
}

