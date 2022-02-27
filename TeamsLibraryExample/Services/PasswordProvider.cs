using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;
using TeamsLibraryExample.Configuration;

namespace TeamsLibraryExample.Services {
	public class PasswordProvider : IPasswordProvider {

		private readonly IMemoryCache _memoryCache;
		private readonly RuntimeSettings _runtimeSettings;
		private readonly IDictionary<string, string> dict = new Dictionary<string,string>();

		public PasswordProvider(IMemoryCache cache, RuntimeSettings settings) {

			_memoryCache = cache;
			_runtimeSettings = settings;
			dict.Add(settings.GraphApi.AppId, "sdfddddddddddddddddddd");
			dict.Add(settings.Teams.AppId, "sdfddddddddddddddddddd");
		}

		public async Task<string> ForceRefeshAsync(string appid) {

			_memoryCache.Remove(appid);

			dict[appid] = _runtimeSettings.GraphApi.AppId.Equals(appid)
				? _runtimeSettings.GraphApi.Secret
				: _runtimeSettings.Teams.Secret;

			return await GetPasswordAsync(appid);
		}

		public async Task<string> GetPasswordAsync(string appid) {

			return _memoryCache.GetOrCreate<string>(appid, (entity) =>
			{
				entity.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);

				return dict[appid];
			});
		}
	}
}
