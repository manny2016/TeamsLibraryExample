using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Polly;
using TeamsLibraryExample.Configuration;

namespace TeamsLibraryExample.Services.Auth {
	public class GraphAPIAuthenticationProvider : IAuthenticationProvider {


		private readonly string[] scopes = new string[] { "https://graph.microsoft.com/.default" };
		private readonly IPasswordProvider _passwordProvider;
		private readonly IMsalHttpClientFactory _factory;
		private readonly RuntimeSettings _settings;
		public GraphAPIAuthenticationProvider(IMsalHttpClientFactory factory, IPasswordProvider passwordProvider, RuntimeSettings settings) {
			_factory = factory;
			_passwordProvider = passwordProvider;
			_settings = settings;
		}
		static string pertoken = default(string);
		public async Task AuthenticateRequestAsync(HttpRequestMessage request) {

			Policy.Handle<AggregateException>((exception) =>
			 {
				 var ex = exception.GetBaseException() as MsalServiceException;
				 return ex?.Message?.IndexOf("Invalid client secret provided") >= 0;

			 }).Retry(1)
			 .Execute(() =>
			 {
				 var password = _passwordProvider.GetPasswordAsync(_settings.GraphApi.AppId).Result;

				 var client = ConfidentialClientApplicationBuilder.Create(_settings.GraphApi.AppId)
				 .WithClientSecret(password)
				 .WithTenantId(_settings.GraphApi.TenantId)
				 .WithHttpClientFactory(_factory)
				 .Build();

				 var authenticateResult = client.AcquireTokenForClient(scopes).ExecuteAsync().Result;
				 request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authenticateResult.AccessToken);

			  });

		}
	}

}
