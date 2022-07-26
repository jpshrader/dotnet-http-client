using dotnet_http_client.Client;
using System.Threading.Tasks;
using Xunit;

namespace dotnet_http_client.examples {
	public class RequestExample {
		private readonly ApiClient subject;

		public RequestExample() {
			subject = new ApiClient();
		}

		[Fact]
		public async Task HitEndpoint() {
			var result = await subject.MakeApiRequest("https://www.google.com");

			Assert.NotNull(result);
			Assert.True(result.IsSuccessCode());
		}
	}
}
