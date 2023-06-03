using dotnet_api_client.Common;

namespace dotnet_http_client.Client {
	public interface IApiClient {
		Task<ApiResponse> Get(
			string requestUrl,
			ContentType contentType = ContentType.Json,
			IEnumerable<(string key, string value)>? queryParams = null,
			IEnumerable<(string key, string value)>? headers = null);

		Task<ApiResponse> Put(
			string requestUrl,
			ContentType contentType = ContentType.Json,
			object? body = null,
			IEnumerable<(string key, string value)>? queryParams = null,
			IEnumerable<(string key, string value)>? headers = null);

		Task<ApiResponse> Post(
			string requestUrl,
			ContentType contentType = ContentType.Json,
			object? body = null,
			IEnumerable<(string key, string value)>? queryParams = null,
			IEnumerable<(string key, string value)>? headers = null);

		Task<ApiResponse> Delete(
			string requestUrl,
			ContentType contentType = ContentType.Json,
			IEnumerable<(string key, string value)>? queryParams = null,
			IEnumerable<(string key, string value)>? headers = null);

		Task<ApiResponse> MakeApiRequest(
			string requestUrl,
			RequestMethod requestMethod = RequestMethod.Get,
			ContentType contentType = ContentType.Json,
			object? body = null,
			IEnumerable<(string key, string value)>? queryParams = null,
			IEnumerable<(string key, string value)>? headers = null);
	}
}