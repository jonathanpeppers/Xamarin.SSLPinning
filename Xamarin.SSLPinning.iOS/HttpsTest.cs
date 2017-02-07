using System;
using System.Net.Http;
using NUnit.Framework;

namespace Xamarin.SSLPinning.iOS
{
    [TestFixture]
    public class HttpsTest
    {
        [Test]
        public void GoodCert()
        {
            var httpClient = new HttpClient(new NSUrlSessionHandler());
            var response = httpClient.GetAsync("https://httpbin.org/get").Result;
            response.EnsureSuccessStatusCode();
        }

        [Test, ExpectedException(typeof(AggregateException))]
        public void BadCert()
        {
            var httpClient = new HttpClient(new NSUrlSessionHandler());
            var response = httpClient.GetAsync("https://www.google.com").Result;
            response.EnsureSuccessStatusCode();
        }
    }
}
