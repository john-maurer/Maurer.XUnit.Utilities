using System.Net;
using UnitTesting.Fixtures;
using UnitTesting.Harnesses;
using UnitTesting.MClient.Verbs;

namespace UnitTesting.Assertions.UsingJson
{
    public class UsingClient : ClientHarness<JsonClientFixture>
    {
        public UsingClient(JsonClientFixture fixture) : base(fixture)
        {

        }

        [Fact]
        public async Task ShouldGetWithoutException() =>
            Assert.Null(await Record.ExceptionAsync(() =>
                _fixture.OkContext!.SendRequestAsync<Get>("https://test/index.html", Payload)
            ));

        [Fact]
        public async Task ShouldGetSuccessfully()
        {
            var response = await _fixture.OkContext!.SendRequestAsync<Get>("https://test/index.html", Payload);

            Assert.True(response.IsSuccessStatusCode);
            response.StatusCode = HttpStatusCode.OK;
        }

        [Fact]
        public async Task ShouldPutWithoutException() =>
            Assert.Null(await Record.ExceptionAsync(() =>
                _fixture.OkContext!.SendRequestAsync<Put>("https://test/index.html", Payload)
            ));

        [Fact]
        public async Task ShouldPutSuccessfully()
        {
            var response = await _fixture.OkContext!.SendRequestAsync<Put>("https://test/index.html", Payload);

            Assert.True(response.IsSuccessStatusCode);
            response.StatusCode = HttpStatusCode.OK;
        }

        [Fact]
        public async Task ShouldPostWithoutException() =>
            Assert.Null(await Record.ExceptionAsync(() =>
                _fixture.OkContext!.SendRequestAsync<Post>("https://test/index.html", Payload)
            ));

        [Fact]
        public async Task ShouldPostSuccessfully()
        {
            var response = await _fixture.OkContext!.SendRequestAsync<Post>("https://test/index.html", Payload);

            Assert.True(response.IsSuccessStatusCode);
            response.StatusCode = HttpStatusCode.OK;
        }

        [Fact]
        public async Task ShouldDeleteWithoutException() =>
            Assert.Null(await Record.ExceptionAsync(() =>
                _fixture.OkContext!.SendRequestAsync<Delete>("https://test/index.html", Payload)
            ));

        [Fact]
        public async Task ShouldDeleteSuccessfully()
        {
            var response = await _fixture.OkContext!.SendRequestAsync<Delete>("https://test/index.html", Payload);

            Assert.True(response.IsSuccessStatusCode);
            response.StatusCode = HttpStatusCode.OK;
        }
    }
}
