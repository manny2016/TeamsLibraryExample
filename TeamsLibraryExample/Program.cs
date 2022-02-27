using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using PollyTest.Services;
using TeamsLibraryExample;
using TeamsLibraryExample.Common.Extensions;
using TeamsLibraryExample.Services;
using TeamsLibraryExample.Services.Auth;
using Microsoft.Extensions.Logging;
using NLog;
using TeamsLibraryExample.Configuration;

namespace TeamsLibraryExample {
	public class Program {
		static void Main(string[] args) {

			CreateHostBuilder(args).Build().RunAsync();
		}
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
			.ConfigureAppConfiguration((option) =>
			{

			})
			.ConfigureLogging((context, logging) =>
			{
				logging.AddFilter("System", Microsoft.Extensions.Logging.LogLevel.Warning);
				logging.AddFilter("Microsoft", Microsoft.Extensions.Logging.LogLevel.Warning);
			})
			.ConfigureServices((services) =>
			{
				services.AddHostedService<Startup>();
				services.AddMemoryCache();
				services.AddSingleton<RuntimeSettings>((provider) =>
				{
					return new RuntimeSettings()
					{
						GraphApi = new AzureADApplication()
						{
							 AppId = "39e7feaf-9304-4a5f-9e6f-25c8d15c252d",
							 Secret = "JYy7Q~iEiKEsVSTAOT~sOf3BwNIECFTP-n66N",
							 TenantId = "55b668c0-4163-4edf-a76c-1fb284cbd0a6"
						},
						Teams = new AzureADApplication()
						{
							AppId = "ac6a32f5-753d-4b61-99a7-666c65f7b39d",
							Secret = "s237Q~kYLgp0h.IiyNLTfNtCQvSr_ncDtz2K6",
							TenantId = "55b668c0-4163-4edf-a76c-1fb284cbd0a6"
						}
					};
				});
				services.AddTransient<IGraphApiClient, GraphApiClient>();
				services.AddTransient<IPasswordProvider, PasswordProvider>();
				services.AddTransient<IAuthenticationProvider, GraphAPIAuthenticationProvider>();
				services.AddTransient<ITeamsSdkClient, TeamsSdkClient>();

				services.AddHttpClient<IMsalHttpClientFactory, GraphAuthenticationHttpClientFactory>()
				.AddUnauthorizedHttpMessageHandler("GraphApi.Auth");

				services.AddHttpClient<IGraphHttpClient, GraphHttpClient>()
				.AddUnauthorizedHttpMessageHandler("GraphApi");

				services.AddHttpClient<ITeamsSdkHttpClient, TeamsSdkHttpClient>()
				.AddUnauthorizedHttpMessageHandler("Teams");

			});
	}
}
