using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using RichardSzalay.MockHttp;
using Patros.WhispirApi;

namespace Patros.WhispirApi.HttpClient.Tests
{
    public class WhispirApiHttpClientTests
    {
        [Fact]
        public async Task TestRequestAddsAuthentication()
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp
                .Expect("http://www.example.com?apikey=test-api-key")
                .WithHeaders("Authorization", "Basic dGVzdC11c2VybmFtZTp0ZXN0LXBhc3N3b3Jk")
                .WithHeaders("x-api-key", "test-api-key")
                .Respond(HttpStatusCode.OK);
            var client = WhispirApiHttpClient.GetClient(new WhispirApiHttpClientOptions
            {
                Username = "test-username",
                Password = "test-password",
                ApiKey = "test-api-key"
            }, mockHttp);

            var responseContent = await client.GetStringAsync("http://www.example.com");

            mockHttp.VerifyNoOutstandingExpectation();
        }
    }
}
