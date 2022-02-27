using System.Threading.Tasks;

namespace TeamsLibraryExample {
	public interface IPasswordProvider {
		Task<string> GetPasswordAsync(string appid);
		Task<string> ForceRefeshAsync(string appid);
	}
}
