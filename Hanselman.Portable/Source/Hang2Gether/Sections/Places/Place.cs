using System;
using System.Collections.Generic;

using Ecom.Hal;
using Ecom.Hal.Attributes;

using Newtonsoft.Json;

using Core;

namespace Hanselman.Portable
{
	public class Place
	{
		public Place ()
		{
			Address = new Address ();
			Location = new GeoJsonPoint ();
			Managers = new Dictionary<string, ManagerRole> ();
		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("caption")]
		public string Caption { get; set; }

		[JsonProperty("address")]
		public Address Address { get; set; }

		[JsonProperty("location")]
		public GeoJsonPoint Location { get; set; }

		[JsonProperty("managers")]
		public Dictionary<string, ManagerRole> Managers { get; set; }
	}
}

