using System;
using System.Collections.Generic;
using System.Diagnostics;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Core
{
	internal static class ParsingUtils
	{
		public static T DeserializeFromStream<T>(Stream stream)
		{
			// Method 1: Automatted
			string jsonString = new StreamReader(stream).ReadToEnd();
			T resourceObject = JsonConvert.DeserializeObject<T> (jsonString);
			return resourceObject;


			// Method 2: Parse string into JObject, a key/value lookup. (jobj["roster"])
			/*string jsonString = new StreamReader(stream).ReadToEnd();
			JObject jobj = JObject.Parse (jsonString);
			// Properties can be added to the object on the fly.
			jobj.Add("received", DateTime.Now);
			T roster = jobj.ToObject<T>();
			return roster;*/

			// Method 3: Item by item manual parsing of stream.
			/*T roster;
			using (StreamReader sr = new StreamReader(stream))
			using (JsonTextReader jsonReader = new JsonTextReader(sr))
			{
				roster = GetRoster<T>(jsonReader);
			}
			return roster;*/
		}

		/**
		 * FIXME: Handle HAL
		 * Need to expose HalResourceConverter in:
		 * https://github.com/Xerosigma/halclient
		 */
		public static T DeserializeFromStream<T>(Stream stream, params JsonConverter[] converters)
		{
			string jsonString = new StreamReader(stream).ReadToEnd();
			T resourceObject = JsonConvert.DeserializeObject<T> (jsonString, converters);
			return resourceObject;
		}

		/// <summary>
		/// TODO: Complete
		/// Gets the resource.
		/// </summary>
		/// <returns>The resource.</returns>
		/// <param name="jsonReader">Json reader.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T GetResource<T>(JsonTextReader jsonReader)
		{
			List<T> employees = new List<T> ();
			T roster = default(T);

			while (jsonReader.Read ())
			{
				if (jsonReader.Value != null) {
					//Debug.WriteLine ("Token: {0}, Value: {1}", jsonReader.TokenType, jsonReader.Value);

					if (jsonReader.Value.ToString() == "roster") {
						//Debug.WriteLine ("Token: {0}", jsonReader.);
					}

				} else {
					//Debug.WriteLine ("Token: {0}", jsonReader.TokenType);
				}
				//roster = HandleToken<T>(jsonReader.TokenType, jsonReader.Value);
			}
			return roster;
		}

		/// <summary>
		/// TODO: Complete.
		/// Handles the token.
		/// </summary>
		/// <returns>The token.</returns>
		/// <param name="token">Token.</param>
		/// <param name="value">Value.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T HandleToken<T>(JsonToken token, object value)
		{
			T roster = default(T);

			switch (token) {

			case(JsonToken.Boolean):
				Debug.WriteLine ("Boolean");
				break;

			case(JsonToken.Bytes):
				Debug.WriteLine ("Bytes");
				break;

			case(JsonToken.Comment):
				Debug.WriteLine ("Comment");
				break;

			case(JsonToken.EndArray):
				Debug.WriteLine ("EndArray");
				break;

			case(JsonToken.EndConstructor):
				Debug.WriteLine ("EndConstructor");
				break;

			case(JsonToken.EndObject):
				Debug.WriteLine ("EndObject");
				break;

			case(JsonToken.Float):
				Debug.WriteLine ("Float");
				break;

			case(JsonToken.Integer):
				Debug.WriteLine ("Integer");
				break;

			case(JsonToken.None):
				Debug.WriteLine ("Boolean");
				break;

			case(JsonToken.Null):
				Debug.WriteLine ("Null");
				break;

			case(JsonToken.PropertyName):
				Debug.WriteLine ("PropertyName");
				break;

			case(JsonToken.Raw):
				Debug.WriteLine ("Raw");
				break;

			case(JsonToken.StartArray):
				Debug.WriteLine ("StartArray");
				break;

			case(JsonToken.StartConstructor):
				Debug.WriteLine ("StartConstructor");
				break;

			case(JsonToken.StartObject):
				Debug.WriteLine ("StartObject");
				break;

			case(JsonToken.String):
				Debug.WriteLine ("String");
				break;

			case(JsonToken.Undefined):
				Debug.WriteLine ("Undefined");
				break;

			}
			return roster;
		}
	}
}

