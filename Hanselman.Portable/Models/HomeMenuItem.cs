using System;

namespace Hanselman.Portable
{
	public enum MenuType
	{
		Events,
		CreateEvent,
	}
	public class HomeMenuItem : BaseModel
	{
		public HomeMenuItem ()
		{
			MenuType = MenuType.Events;
		}
		public string Icon {get;set;}
		public MenuType MenuType { get; set; }
	}
}

