using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Xamarin.SSLPinning.iOS
{
    [TestFixture]
    public class HttpsTest
    {
        private const int Timeout = 3000;

        [Test, Timeout(Timeout)]
        public async Task Get()
        {
            var httpClient = new HttpClient(new NSUrlSessionHandler());
            var response = await httpClient.GetAsync("https://httpbin.org/get");
            response.EnsureSuccessStatusCode();
        }
    }
}
