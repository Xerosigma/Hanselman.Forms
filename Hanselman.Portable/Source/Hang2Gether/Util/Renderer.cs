using System;

using Xamarin.Forms;

namespace Hanselman.Portable.Helpers
{
    public interface Renderer
    {
		void ShowSnack(string message, Command command);

		void SetViewData(string viewName, Object data);
    }
}

