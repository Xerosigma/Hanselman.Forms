using System;
using System.Net.Http;

using Core;

using Ecom.Hal.JSON;

namespace Hanselman.Portable
{
	public class EventsRepository : RepositoryBase<EventsResource>
	{
		public override string RESOURCE_URL { get{ return "http://10.0.0.9:8080/events"; } }
		//public override string RESOURCE_URL { get{ return "http://192.168.2.48:8080/events"; } }
		public override string RESOURCE_NAME { get{ return "events"; } }

		public EventsRepository() : base(new HalResourceConverter())
		{
		}
	}
}

