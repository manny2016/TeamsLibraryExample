using System.Net.Http;

namespace TeamsLibraryExample {
	public interface IGraphHttpClient : IProxyHttpClient {

	}
	public interface ITeamsSdkHttpClient : IProxyHttpClient {

	}
	public interface IProxyHttpClient {
		HttpClient HttpClient { get; }
	}
}
