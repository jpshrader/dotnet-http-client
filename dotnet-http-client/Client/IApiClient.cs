using dotnet_api_client.Common;

namespace dotnet_http_client.Client {
	public interface IApiClient {
		Task<ApiResponse> MakeApiRequest(
			string requestUrl,
			RequestMethod requestMethod = RequestMethod.Get,
			ContentType contentType = ContentType.Json,
			object? body = null,
			IEnumerable<(string key, string value)>? queryParams = null,
			IEnumerable<(string key, string value)>? headers = null);
	}
}
