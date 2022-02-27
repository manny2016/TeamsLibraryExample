using System.Threading.Tasks;

namespace TeamsLibraryExample {
	public interface ITeamsSdkClient {
		Task<string> SendPersonalMessageAsync();
		Task<string> SendChannelMessageAsync();
	}
}
