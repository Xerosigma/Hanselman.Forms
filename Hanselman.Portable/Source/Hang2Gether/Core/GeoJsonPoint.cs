using System;
using System.Collections.Generic;

using Ecom.Hal;
using Ecom.Hal.Attributes;

using Newtonsoft.Json;

namespace Hanselman.Portable
{
	[JsonObject(MemberSerialization.OptIn)]
	public class GeoJsonPoint
	{
		[JsonProperty("x")]
		public double X { get; set; }

		[JsonProperty("y")]
		public double Y { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("coordinates")]
		public double[] Coordinates
		{
			get { return new double[]{X,Y}; }
		}

		public GeoJsonPoint ()
		{
			Type = "Point";
		}

		public GeoJsonPoint (double x, double y)
		{
			X = x;
			Y = y;
			Type = "Point";
		}
	}
}

