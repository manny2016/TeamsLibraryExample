using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamsLibraryExample.Configuration;
using TeamsLibraryExample.Services.Auth;

namespace TeamsLibraryExample.Services {
	public class TeamsSdkClient : ITeamsSdkClient {

		private readonly ITeamsSdkHttpClient teamsSdkHttpClient;
		private readonly IPasswordProvider passwordProvider;
		private readonly RuntimeSettings runtimeSettings;
		private readonly string SERVICEURL = "https://smba.trafficmanager.net/amer/";
		public TeamsSdkClient(ITeamsSdkHttpClient httpClient, IPasswordProvider provider, RuntimeSettings settings) {

			teamsSdkHttpClient = httpClient;
			passwordProvider = provider;
			runtimeSettings = settings;

		}
		private async Task<ConnectorClient> CreateClientAsync() {

			var secret = await passwordProvider.GetPasswordAsync(runtimeSettings.Teams.AppId);
			//var credential = new TeamsAppCredentials(runtimeSettings.Teams.AppId, secret, teamsSdkHttpClient.HttpClient,passwordProvider);
			var credential = new MicrosoftAppCredentials(runtimeSettings.Teams.AppId, secret, teamsSdkHttpClient.HttpClient);
			return new ConnectorClient(credential, teamsSdkHttpClient.HttpClient, false)
			{
				BaseUri = new Uri(SERVICEURL)
			};
		}
		public async Task<string> SendChannelMessageAsync() {

			using (var client = await CreateClientAsync())
			{
				var activity = Activity.CreateMessageActivity();
				activity.From = new ChannelAccount($"28:{runtimeSettings.Teams.AppId}");
				activity.ServiceUrl = SERVICEURL;
				activity.Text = $"Helo World! {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";

				var conversation = await client.Conversations.CreateConversationAsync(new Microsoft.Bot.Schema.ConversationParameters()
				{
					IsGroup = true,
					Activity = (Activity)activity,
					Bot = new ChannelAccount($"28:{runtimeSettings.Teams.AppId}"),
					TenantId = runtimeSettings.Teams.TenantId,
					ChannelData = new TeamsChannelData()
					{
						Tenant = new TenantInfo(runtimeSettings.Teams.TenantId),
						Team = new TeamInfo() { Id = "8edc79a5-21a5-4bcd-a760-aa89a4f734be" },
						Channel = new ChannelInfo() { Id = "19:g8-sjQm8lCIjuoQZ6iWL24V5OClvowRBD014ErpP5G41@thread.tacv2" }
					}
				});
				return conversation.Id;
			}
		}

		public async Task<string> SendPersonalMessageAsync() {
			throw new NotImplementedException();
		}
	}
}
