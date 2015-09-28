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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
	public interface Repository<T>
	{
		string RESOURCE_URL { get; }

		/// <summary>
		/// Returns the name used in Content-Disposition of a multipart HTTP request.
		/// spring.data.rest will map this value to controller method parameters:
		/// Ex: @RequestPart Event event
		/// 
		/// And the raw part should look something like this:
		/// Content-Disposition: form-data; name="event"
		/// </summary>
		/// <value>Content-Disposition Name parameter.</value>
		string RESOURCE_NAME { get; }

		Task<RequestResponse<T>> Post(T resource);


		/// <summary>
		/// Post the specified resource with additional parts.
		/// Request is sent as multipart form-data.
		/// 
		/// Note that the keys used in the parts dictionary are the
		/// same names the spring.data.rest controller will expect.
		/// </summary>
		/// <param name="resource">Resource.</param>
		/// <param name="parts">Dictionary of k/v Parts (part name/value)</param>
		Task<RequestResponse<T>> Post(T resource, Dictionary<string, object> parts);

		Task<RequestResponse<T>> Get();

		Task<RequestResponse<T>> Update();

		Task<RequestResponse<T>> Delete();
	}
}

