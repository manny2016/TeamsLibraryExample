using Microsoft.Graph;
using System.Threading.Tasks;

namespace TeamsLibraryExample.Services {
	public class GraphApiClient : IGraphApiClient {

		private IAuthenticationProvider authenticationProvider;
		private IGraphHttpClient graphHttpClient;

		private const string GroupId = "8edc79a5-21a5-4bcd-a760-aa89a4f734be";
		private const string ChannelId = "19:g8-sjQm8lCIjuoQZ6iWL24V5OClvowRBD014ErpP5G41@thread.tacv2";
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

		public async Task<IChannelMessagesCollectionPage> ListChannelMessages() {

			return await (await CreateAsync()).Teams[GroupId]
				.Channels[ChannelId].Messages.Request().GetAsync();
		}

		public async Task<IChatMessageDeltaCollectionPage> GetChatMessagesInaChannelDelta() {

			return await (await CreateAsync())
				.Teams[GroupId]
				.Channels[ChannelId]
				.Messages.Delta().Request()
				.Filter("").GetAsync();

		}

		public async Task<ChatMessage> GetChannelMessage(string messageId) {

			return await (await CreateAsync()).Teams[GroupId]
				.Channels[ChannelId].Messages[messageId]
				.Request().GetAsync();
		}

		public async Task<IChatMessageRepliesCollectionPage> ListReplies(string messageId) {

			return await (await CreateAsync())
				.Teams[GroupId].Channels[ChannelId]
				.Messages[messageId]
				.Replies.Request()
				.GetAsync();
		}

		public async Task<ChatMessage> GetReplytoMessage(string messageid, string replyid) {
			return await (await CreateAsync())
				.Teams[GroupId]
				.Channels[ChannelId]
				.Messages[messageid]
				.Replies[replyid]
				.Request()
				.GetAsync();
		}

		public async Task<IUserChatsCollectionPage> ListChats(string userAadId) {
			return await (await CreateAsync())
				.Users[userAadId]
				.Chats
				.Request()
				.GetAsync();
		}

		public async Task<IChatMessagesCollectionPage> ListMessagesInaChat(string chatId) {

			return await (await CreateAsync()).Chats[chatId]
				.Messages
				.Request()
				.GetAsync();
		}

		public async Task<IChatMessageHostedContentsCollectionPage> ListHostedContentsInChannel(string messageId) {
			return await (await CreateAsync()).Teams[GroupId]
				.Channels[ChannelId]
				.Messages[messageId]
				.HostedContents
				.Request()
				.GetAsync();

		}

		public async Task<IChatMessageHostedContentsCollectionPage> ListHostedContentsInChat(string chatid, string messageId) {

			return await (await CreateAsync())
				.Chats[chatid]
				.Messages[messageId]
				.HostedContents
				.Request()
				.GetAsync();
		}

		public async Task<ChatMessageHostedContent> GetHostedContentInChanel(string messageId, string hostedContentId) {

			return await (await CreateAsync()).Teams[GroupId]
				   .Channels[ChannelId]
				   .Messages[messageId]
				   .HostedContents[hostedContentId]
				   .Request()
				   .GetAsync();
		}

		public async Task<ChatMessageHostedContent> GetHostedContentInChat(string chatId, string messageId, string hostedContentId) {

			return await(await CreateAsync()).Chats[chatId]				   
				   .Messages[messageId]
				   .HostedContents[hostedContentId]
				   .Request()
				   .GetAsync();
		}
	}
}
