using Microsoft.Graph;
using System.Threading.Tasks;

namespace TeamsLibraryExample {
	public interface IGraphApiClient {

		Task<IGraphServiceApplicationsCollectionPage> GetApplicationsAsync();

		Task<IChannelMessagesCollectionPage> ListChannelMessages();

		Task<IChatMessageDeltaCollectionPage> GetChatMessagesInaChannelDelta();

		Task<ChatMessage> GetChannelMessage(string messageId);

		Task<IChatMessageRepliesCollectionPage> ListReplies(string messageId);

		Task<ChatMessage> GetReplytoMessage(string messageid, string reply);

		Task<IUserChatsCollectionPage> ListChats(string userAadId);

		Task<IChatMessagesCollectionPage> ListMessagesInaChat(string chatId);

		Task<IChatMessageHostedContentsCollectionPage> ListHostedContentsInChannel(string messageId);

		Task<IChatMessageHostedContentsCollectionPage> ListHostedContentsInChat(string chatid, string messageId);

		Task<ChatMessageHostedContent> GetHostedContentInChanel(string messageId, string hostedContentId);
		Task<ChatMessageHostedContent> GetHostedContentInChat(string chatId, string messageId, string hostedContentId);
	}
}
