using System.Text.Json;

namespace dotnet_api_client.Common {
	public static class ContentTypeExtensions {
		public const string JsonContentType = "application/json";
		public const string XmlContentType = "application/xml";

		public static string ToHttpContent(this ContentType contentType, object body) {
			return contentType switch {
				ContentType.Json => JsonSerializer.Serialize(body),
				ContentType.Xml => string.Empty,
				_ => throw new ArgumentOutOfRangeException($"Given {nameof(ContentType)} was not found"),
			};
		}

		public static string ToMediaTypeString(this ContentType contentType) {
			return contentType switch {
				ContentType.Json => JsonContentType,
				ContentType.Xml => XmlContentType,
				_ => throw new ArgumentOutOfRangeException($"Given {nameof(ContentType)} was not found"),
			};
		}
	}
}
