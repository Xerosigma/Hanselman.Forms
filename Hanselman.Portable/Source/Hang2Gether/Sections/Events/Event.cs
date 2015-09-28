using System;
using System.Collections.Generic;

using Ecom.Hal;
using Ecom.Hal.Attributes;

using Newtonsoft.Json;

namespace Hanselman.Portable
{
	
	public class Event : HalResource
	{
		public Event()
		{
			Place = new Place ();
			StartTime = DateTime.Now;
			EndTime = DateTime.Now;
			Managers = new Dictionary<string, ManagerRole> ();
			SoftLocation = new GeoJsonPoint ();
		}

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("place")]
		public Place Place { get; set; }

		[JsonProperty("rating")]
		public long Rating { get; set; }

		[JsonProperty("startTime")]
		public DateTime StartTime { get; set; }

		[JsonProperty("endTime")]
		public DateTime EndTime { get; set; }

		[JsonProperty("hosts")]
		public List<Attendee> Hosts { get; set; }

		[JsonProperty("attendees")]
		public List<Attendee> Attendees { get; set; }

		[JsonProperty("managers")]
		public Dictionary<string, ManagerRole> Managers { get; set; }

		[JsonProperty("imageId")]
		public String ImageId { get; set; }

		private GeoJsonPoint _softLocation;
		[JsonProperty("softLocation")]
		public GeoJsonPoint SoftLocation {
			get {
				return _softLocation;
			}
			set {
				Place.Location = value;
				_softLocation = value;
			}
		} 
	}
}

