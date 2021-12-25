namespace dotnet_api_client.Common {
	public static class RequestMethodExtensions {
		public static HttpMethod ToHttpMethod(this RequestMethod requestMethod) {
			return requestMethod switch {
				RequestMethod.Get => HttpMethod.Get,

				RequestMethod.Put => HttpMethod.Put,

				RequestMethod.Post => HttpMethod.Post,

				RequestMethod.Delete => HttpMethod.Delete,

				RequestMethod.Options => HttpMethod.Options,

				RequestMethod.Patch => HttpMethod.Patch,

				_ => throw new ArgumentOutOfRangeException($"Given {nameof(RequestMethod)} does not have an designated {nameof(HttpMethod)} equivalent")
			};
		}
	}
}
