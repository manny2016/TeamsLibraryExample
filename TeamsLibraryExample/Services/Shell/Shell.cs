using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace TeamsLibraryExample.Services {
	public static class Shell {

		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		public static async void GetAzureADApplicationAsync(IServiceProvider provider) {
			Logger.Info($"Execution={nameof(GetAzureADApplicationAsync)}");
			var client = provider.GetService<IGraphApiClient>();
			foreach (var app in await client.GetApplicationsAsync())
			{
				Logger.Info($"AppId={app.AppId};DisplayName={app.DisplayName}");
			}
			Logger.Info($"Completed.");
		}

		public static void SendPersonalMessage(IServiceProvider provider) {

		}
		public static async void SendChannelMessage(IServiceProvider provider) {

			Logger.Info($"Execution={nameof(SendChannelMessage)}");

			var client = provider.GetService<ITeamsSdkClient>();
			var id = await client.SendChannelMessageAsync();

			Logger.Info($"ConversationId={id}");
			Logger.Info($"Completed.");
		}
	}
}
