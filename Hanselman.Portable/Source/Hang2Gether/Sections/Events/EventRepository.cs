using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Core;

using Ecom.Hal.JSON;

namespace Hanselman.Portable
{
	public class EventRepository : RepositoryBase<Event>
	{
		public override string RESOURCE_URL { get{ return "http://10.0.0.9:8080/events"; } }
		//public override string RESOURCE_URL { get{ return "http://192.168.2.48:8080/events"; } }
		public override string RESOURCE_NAME { get{ return "event"; } }

		public EventRepository() : base(new HalResourceConverter())
		{
		}

		public class EventImageRepository : RepositoryBase<byte[]>
		{
			public override string RESOURCE_URL { get{ return "http://10.0.0.9:8080/events/image"; } }
			//public override string RESOURCE_URL { get{ return "http://192.168.2.48:8080/events"; } }
			public override string RESOURCE_NAME { get{ return "event"; } }
		}
	}
}

