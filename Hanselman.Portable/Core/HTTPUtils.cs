using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using ModernHttpClient;
using Newtonsoft.Json;

namespace Core
{
	internal static class HTTPUtils
	{
		public static async Task<RequestResponse<T>> Get<T>(string url, params JsonConverter[] converters)
		{
			T result = default(T);

			HttpClient client = new HttpClient (new NativeMessageHandler ());
			HttpResponseMessage response = await client.GetAsync(url);
			using (HttpContent content = response.Content)
			{
				Stream stream = await content.ReadAsStreamAsync();
				result = ParsingUtils.DeserializeFromStream<T>(stream, converters);
			}

			return new RequestResponse<T>(response, result);
		}

		/// <summary>
		/// Gets resource as stream.
		/// </summary>
		/// <returns>The stream.</returns>
		/// <param name="url">URL.</param>
		public static async Task<RequestResponse<Stream>> GetStream(string url)
		{
			Stream result = null;

			HttpClient client = new HttpClient (new NativeMessageHandler ());
			HttpResponseMessage response = await client.GetAsync(url);
			using (HttpContent content = response.Content)
			{
				result = await content.ReadAsStreamAsync();
			}

			return new RequestResponse<Stream>(response, result);
		}
	}

	/// <summary>
	/// An object for holding the raw HttpResponseMessage
	/// and deserialized content.
	/// </summary>
	public class RequestResponse<T>
	{
		public HttpResponseMessage response { get; private set; }
		public T content { get; private set; }

		public RequestResponse(HttpResponseMessage response, T content)
		{
			this.response = response;
			this.content = content;
		}
	}
}

