﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AglTest.Tests
{
	public class FakeHttpMessageHandler : HttpMessageHandler
	{
		private readonly HttpResponseMessage _response;

		public FakeHttpMessageHandler(HttpResponseMessage response)
		{
			_response = response;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var responseTask = new TaskCompletionSource<HttpResponseMessage>();
			responseTask.SetResult(_response);

			return responseTask.Task;
		}
	}
}
