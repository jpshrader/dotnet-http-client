using dotnet_api_client.Common;
using System.Net;
using System.Text;

namespace dotnet_http_client.Client {
	public class ApiClient : IApiClient {
		private static readonly IEnumerable<(string key, string value)> emptyParams = Enumerable.Empty<(string key, string value)>();

		public Task<ApiResponse> Get(
			string requestUrl, 
			ContentType contentType = ContentType.Json, 
			IEnumerable<(string key, string value)>? queryParams = null, 
			IEnumerable<(string key, string value)>? headers = null) {
			return MakeApiRequest(requestUrl, RequestMethod.Get, contentType, null, queryParams, headers);
		}

		public Task<ApiResponse> Put(
			string requestUrl, 
			ContentType contentType = ContentType.Json, 
			object? body = null, 
			IEnumerable<(string key, string value)>? queryParams = null, 
			IEnumerable<(string key, string value)>?  headers = null) {
			return MakeApiRequest(requestUrl, RequestMethod.Put, contentType, body, queryParams, headers);
		}

		public Task<ApiResponse> Post(
			string requestUrl, 
			ContentType contentType = ContentType.Json, 
			object? body = null, 
			IEnumerable<(string key, string value)>? queryParams = null, 
			IEnumerable<(string key, string value)>? headers = null) {
			return MakeApiRequest(requestUrl, RequestMethod.Post, contentType, body, queryParams, headers);
		}

		public Task<ApiResponse> Delete(
			string requestUrl, 
			ContentType contentType = ContentType.Json, 
			IEnumerable<(string key, string value)>? queryParams = null, 
			IEnumerable<(string key, string value)>? headers = null) {
			return MakeApiRequest(requestUrl, RequestMethod.Delete, contentType, null, queryParams, headers);
		}

		public async Task<ApiResponse> MakeApiRequest(
			string requestUrl,
			RequestMethod requestMethod = RequestMethod.Get,
			ContentType contentType = ContentType.Json,
			object? body = null,
			IEnumerable<(string key, string value)>? queryParams = null,
			IEnumerable<(string key, string value)>? headers = null) {
			var cookieContainer = new CookieContainer();
			using var clientHandler = new HttpClientHandler {
				PreAuthenticate = true,
				CookieContainer = cookieContainer
			};
			using var httpClient = new HttpClient(clientHandler);

			foreach (var (key, value) in queryParams ?? emptyParams) {
				requestUrl = AddUrlSegment(requestUrl, key, value);
			}

			var uri = new Uri(requestUrl);
			var httpRequestMessage = GetHttpRequestMessage(uri, requestMethod, contentType, headers, body);

			return await ApiResponse.GetApiResponse(await httpClient.SendAsync(httpRequestMessage), cookieContainer.GetCookies(uri));
		}

		private static HttpRequestMessage GetHttpRequestMessage(Uri requestUri, RequestMethod requestMethod, ContentType contentType, IEnumerable<(string key, string value)>? headers, object? body) {
			var httpRequestMessage = new HttpRequestMessage {
				RequestUri = requestUri,
				Method = requestMethod.ToHttpMethod(),
				Content = GetHttpContent(contentType, body)
			};

			foreach (var (key, value) in headers ?? emptyParams)
				httpRequestMessage.Headers.Add(key, value);

			return httpRequestMessage;
		}

		private static StringContent? GetHttpContent(ContentType contentType, object? body) {
			if (body == null)
				return null;

			return new StringContent(contentType.ToHttpContent(body), Encoding.Default, contentType.ToMediaTypeString());
		}

		private static string AddUrlSegment(string requestUrl, string key, string value) {
			if (requestUrl.Contains('?'))
				return $"{requestUrl}&{key}={value}";

			return $"{requestUrl}?{key}={value}";
		}
	}
}