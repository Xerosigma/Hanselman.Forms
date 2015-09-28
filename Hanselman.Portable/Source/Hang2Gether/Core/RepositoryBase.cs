//
//  Copyright 2014  nestorledon
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Core
{
	public abstract class RepositoryBase<T> : Repository<T>
	{
		public abstract string RESOURCE_URL { get; }
		public abstract string RESOURCE_NAME { get; }
		public JsonConverter[] Converters { get; set; }


		public RepositoryBase () {}

		public RepositoryBase(params JsonConverter[] converters)
		{
			Converters = converters;
		}

		// FIXME: Handle exceptions!
		public async Task<RequestResponse<T>> Post(T resource)
		{
			string jsonContent = ParsingUtils.SerializeToJson (resource);

			List<HttpContent> content = new List<HttpContent>();
			content.Add (new StringContent (jsonContent));

			return await HTTPUtils.PostMultipart<T> (RESOURCE_URL, Converters, content);
		}

		// FIXME: Handle exceptions!
		public async Task<RequestResponse<T>> Post(T resource, Dictionary<string, object> parts)
		{
			List<HttpContent> content = new List<HttpContent>();

			string jsonContent = ParsingUtils.SerializeToJson (resource);
			StringContent stringContent = new StringContent (jsonContent, Encoding.UTF8, HTTPUtils.HTTP_CONTENT_TYPE_JSON);

			stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue(HTTPUtils.HTTP_CONTENT_DISPOSITION_FORM)
			{
				Name = RESOURCE_NAME
			};

			content.Add (stringContent);

			if (null != parts) {
				foreach (KeyValuePair<string, object> part in parts) {

					if (part.Value is byte[]) {
						ByteArrayContent byteArrayContent = new ByteArrayContent ((byte[])part.Value);
						content.Add (byteArrayContent);
						byteArrayContent.Headers.Add ("Content-Type", "image/jpg");

						byteArrayContent.Headers.ContentDisposition = new ContentDispositionHeaderValue(HTTPUtils.HTTP_CONTENT_DISPOSITION_FORM)
						{
							Name = part.Key,
							FileName = part.Key + ".jpg"
						};
					} else if (part.Value is Stream) {
						StreamContent streamContent = new StreamContent ((Stream)part.Value);
						content.Add (streamContent);
					}

				}
			}

			return await HTTPUtils.PostMultipart<T> (RESOURCE_URL, Converters, content);
		}

		public async Task<RequestResponse<T>> Get()
		{
			return await GetObjectFromStream ();
		}

		public async Task<RequestResponse<T>> Get(params KeyValuePair<string, string>[] list)
		{
			string url = RESOURCE_URL;
			if (!url.EndsWith ("?")) {
				url += "?";
			}

			string inclusion = "";
			for(int ii = 0; ii < list.Length; ii++) {
				KeyValuePair<string, string> kvp = list[ii];
				inclusion = string.Format ("{0}={1}", kvp.Key, kvp.Value);
				if(ii != list.Length) {
					inclusion += "&";
				}
			}

			string query = url + inclusion;
			return await GetObjectFromStream (query);
		}

		// TODO: Implement
		public async Task<RequestResponse<T>> Update()
		{
			return await GetObjectFromStream ();
		}

		// TODO: Implement
		public async Task<RequestResponse<T>> Delete()
		{
			return await GetObjectFromStream ();
		}

		public async Task<RequestResponse<T>> GetObjectFromStream()
		{
			RequestResponse<T> result = null;

			result = await HTTPUtils.Get<T> (RESOURCE_URL, Converters);

			return result;
		}

		public async Task<RequestResponse<T>> GetObjectFromStream(string path)
		{
			RequestResponse<T> result = null;

			result = await HTTPUtils.Get<T> (path, Converters);

			return result;
		}

		public async Task<RequestResponse<byte[]>> GetStream(string path)
		{
			RequestResponse<byte[]> result = null;

			result = await HTTPUtils.GetStream (path);

			return result;
		}
	}
}

