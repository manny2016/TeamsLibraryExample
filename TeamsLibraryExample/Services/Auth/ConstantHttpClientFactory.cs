using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibraryExample.Services.Auth {
    /// <summary>
    /// HttpClientFactory that always returns the same HttpClient instance for ADAL AcquireTokenAsync calls.
    /// </summary>
    internal class ConstantHttpClientFactory : Microsoft.IdentityModel.Clients.ActiveDirectory.IHttpClientFactory {
        private readonly HttpClient httpClient;

        public ConstantHttpClientFactory(HttpClient client) {
            httpClient = client ?? throw new ArgumentNullException(nameof(client));
        }

        public HttpClient GetHttpClient() {
            return httpClient;
        }
    }
}
