using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using ModernHttpClient;
using Newtonsoft.Json;

namespace Core
{
	internal static class HTTPUtils
	{
		public static string HTTP_CONTENT_TYPE_JSON = "application/json";
		public static string HTTP_CONTENT_TYPE_HAL = "application/hal+json";
		public static string HTTP_CONTENT_DISPOSITION_FORM = "form-data";

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
		public static async Task<RequestResponse<byte[]>> GetStream(string url)
		{
			byte[] result = null;

			HttpClient client = new HttpClient (new NativeMessageHandler ());
			HttpResponseMessage response = await client.GetAsync(url);
			using (HttpContent content = response.Content)
			{
				result = await content.ReadAsByteArrayAsync();
				content.Dispose ();
			}

			return new RequestResponse<byte[]>(response, result);
		}

		public static async Task<RequestResponse<T>> PostMultipart<T>(string url, JsonConverter[] converters, List<HttpContent> contents)
		{
			T result = default(T);

			HttpContent httpContent;
			if (contents.Count == 1) {
				httpContent = contents [0];
			}

			else {
				MultipartFormDataContent multipartContent = new MultipartFormDataContent ();
				for(int ii = 0; ii < contents.Count; ii++) {
					multipartContent.Add (contents[ii]);
				}
				httpContent = multipartContent;
			}

			HttpClient client = new HttpClient (new NativeMessageHandler ());
			HttpRequestMessage httpRequestMessage = new HttpRequestMessage (HttpMethod.Post, url);
			httpRequestMessage.Content = httpContent;

			HttpResponseMessage response = await client.SendAsync (httpRequestMessage);
			using (HttpContent content = response.Content)
			{
				Stream stream = await content.ReadAsStreamAsync();
				result = ParsingUtils.DeserializeFromStream<T>(stream, converters);
			}

			return new RequestResponse<T>(response, result);
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

