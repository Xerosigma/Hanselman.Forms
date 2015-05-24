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
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Core
{
	public abstract class RepositoryBase<T> : Repository<T>
	{
		public abstract string RESOURCE_URL { get; }
		public JsonConverter[] Converters { get; set; }



		public RepositoryBase () {}

		public RepositoryBase(params JsonConverter[] converters)
		{
			Converters = converters;
		}

		// TODO: Implement
		public async Task<RequestResponse<T>> Post()
		{
			return await GetObjectFromStream ();
		}

		public async Task<RequestResponse<T>> Get()
		{
			return await GetObjectFromStream ();
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
	}
}

