using System;
using System.Net.Http;

using Core;

using Ecom.Hal.JSON;

namespace Hanselman.Portable
{
	public class PlaceRepository : RepositoryBase<Place>
	{
		public override string RESOURCE_URL { get{ return "http://10.0.0.9:8080/place"; } }
		//public override string RESOURCE_URL { get{ return "http://192.168.2.48:8080/place"; } }
		public override string RESOURCE_NAME { get{ return "place"; } }

		public PlaceRepository() : base(new HalResourceConverter())
		{
		}
	}
}

