using Microsoft.Graph;
using System.Threading.Tasks;

namespace TeamsLibraryExample.Services {
	public class GraphApiClient : IGraphApiClient {

		private IAuthenticationProvider authenticationProvider;
		private IGraphHttpClient graphHttpClient;
		public GraphApiClient(IAuthenticationProvider authentication, IGraphHttpClient httpClient) {

			authenticationProvider = authentication;
			graphHttpClient = httpClient;
		}

		private async Task<GraphServiceClient> CreateAsync() {

			var httpProvider = new SimpleHttpProvider(graphHttpClient.HttpClient);
			return new GraphServiceClient(authenticationProvider, httpProvider);
		}

		public async Task<IGraphServiceApplicationsCollectionPage> GetApplicationsAsync() {

			var client = await CreateAsync();			
			return await client.Applications.Request().GetAsync();
		}
		
	}
}
