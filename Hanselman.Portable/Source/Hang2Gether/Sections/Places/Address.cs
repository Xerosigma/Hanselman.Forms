using System;

using Newtonsoft.Json;

namespace Hanselman.Portable
{
	public class Address
	{
		[JsonProperty("street")]
		public string Street { get; set; }

		[JsonProperty("optional")]
		public string Optional { get; set; }

		[JsonProperty("city")]
		public string City { get; set; }

		[JsonProperty("state")]
		public string State { get; set; }

		[JsonProperty("zip")]
		public string Zip { get; set; }

		public override string ToString()
		{
			return string.Format ("{0} {1} {2} {3}", Street, City, State, Zip);
		}
	}
}

