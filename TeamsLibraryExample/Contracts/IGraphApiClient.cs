using Microsoft.Graph;
using System.Threading.Tasks;

namespace TeamsLibraryExample {
	public interface IGraphApiClient {
		
		Task<IGraphServiceApplicationsCollectionPage> GetApplicationsAsync();

	}
}
