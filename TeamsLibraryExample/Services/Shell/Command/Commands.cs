using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibraryExample.Services {

	
	[Verb("GetAzureADApplication", HelpText = "Use GraphAPI to retrive Azure AD Application.")]
	public class GetAzureADApplication { }

	[Verb("SendPersonalMessage", HelpText = "Send Teams peer to peer message.")]
	public class SendPersonalMessage { }

	[Verb("SendChannelMessage", HelpText = "Send Teams channel message.")]
	public class SendChannelMessage { }

	[Verb("ListChannelMessages", HelpText = "List channel message.")]
	public class ListChannelMessages { }

	[Verb("GetChatMessagesInaChannelDelta", HelpText = "Get chat messages in a channel delta.")]
	public class GetChatMessagesInaChannelDelta { }

	[Verb("GetChannelMessage", HelpText = "Get an channel message.")]
	public class GetChannelMessage { }


	[Verb("GetaReplyToAmessage", HelpText = "Get a reply to a message.")]
	public class GetAReplyToAmessage { }

	[Verb("ListChats", HelpText = "List chats.")]
	public class ListChats { }

	[Verb("ListMessageInaChat", HelpText = "List message in a chat.")]
	public class ListMessageInaChat { }

	[Verb("GetMessageInChat", HelpText = "Get message in chat.")]
	public class GetMessageInChat { }

	[Verb("ListAllHostedContentInChannel", HelpText = "List all hosted content in channel.")]
	public class ListAllHostedContentInChannel { }

	[Verb("ListAllHostedContentInChat", HelpText = "List all hosted content in chat.")]
	public class ListAllHostedContentInChat { }

	[Verb("ListHostedContent", HelpText = "Get hosted content.")]
	public class GetHostedContent { }	
}
