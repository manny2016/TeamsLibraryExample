using Microsoft.Graph;
using System.Net.Http;
using System.Threading.Tasks;

namespace TeamsLibraryExample.Services {
	public class GraphHttpClient : IGraphHttpClient {
		
		public GraphHttpClient(HttpClient client) {
			HttpClient = client;
		}
		public HttpClient HttpClient { get; private set;}
	}
}
