using Newtonsoft.Json;
using System.Net;
using System.Xml.Serialization;

namespace dotnet_api_client.Common {
	public class ApiResponse {
		public static async Task<ApiResponse> GetApiResponse(HttpResponseMessage httpResponseMessage, CookieCollection cookieCollection) {
			return new ApiResponse {
				StatusCode = httpResponseMessage.StatusCode,
				Headers = httpResponseMessage.Headers.ToDictionary(k => k.Key, v => v.Value),
				Cookies = cookieCollection,
				Body = await httpResponseMessage.Content.ReadAsStringAsync()
			};
		}

		public HttpStatusCode StatusCode { get; private set; }

		public IDictionary<string, IEnumerable<string>> Headers { get; private set; }

		public IEnumerable<Cookie> Cookies { get; private set; }

		public string Body { get; private set; }

		public T ToObject<T>() {
			var contentType = Headers.SingleOrDefault(h => h.Key.ToLower() == "content-type");
			if (contentType.Value.FirstOrDefault() == ContentTypeExtensions.XmlContentType) {
				var serialiser = new XmlSerializer(typeof(T));
				using var reader = new StringReader(Body);

				return (T)serialiser.Deserialize(reader);
			}

			return JsonConvert.DeserializeObject<T>(Body);
		}

		public bool IsSuccessCode() {
			var statusCode = (int)StatusCode;
			return statusCode >= 200 && statusCode <= 299;
		}

		public bool IsClientErrorCode() {
			var statusCode = (int)StatusCode;
			return statusCode >= 400 && statusCode <= 499;
		}

		public bool IsServerErrorCode() {
			var statusCode = (int)StatusCode;
			return statusCode >= 500 && statusCode <= 599;
		}
	}
}
