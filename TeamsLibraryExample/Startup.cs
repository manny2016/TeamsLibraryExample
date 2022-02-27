using CommandLine;
using Microsoft.Extensions.Hosting;
using NLog;
using System;
using System.Threading;
using System.Threading.Tasks;
using TeamsLibraryExample.Services;
using System.Linq;
namespace TeamsLibraryExample {
	public class Startup : IHostedService {

		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		private static IServiceProvider serviceProvider;
		public Startup(IServiceProvider sp) {
			serviceProvider = sp;
		}
		private Type[] types = new Type[] {

			typeof(GetAzureADApplication),typeof(SendPersonalMessage),typeof(SendChannelMessage),typeof(ListChannelMessages),
			typeof(GetChatMessagesInaChannelDelta),typeof(GetChannelMessage),typeof(GetAReplyToAmessage),typeof(ListChats),
			typeof(ListMessageInaChat),typeof(GetMessageInChat),typeof(ListAllHostedContentInChannel),typeof(GetHostedContent)		
		};
		public async Task StartAsync(CancellationToken cancellationToken) {
			
			while (!cancellationToken.IsCancellationRequested)
			{
				Console.Write(">");
				var args = Console.ReadLine().Split(" ".ToCharArray());
				await Parser.Default.ParseArguments(args, types)
					.WithParsed<GetAzureADApplication>((context) => { Shell.GetAzureADApplicationAsync(serviceProvider); })
					.WithParsed<SendPersonalMessage>((context) => {Shell.SendPersonalMessage(serviceProvider); })
					.WithParsed<SendChannelMessage>((context) => { Shell.SendChannelMessage(serviceProvider);})
					.WithParsed<ListChannelMessages>((content)=>{ })
					.WithParsed<GetChatMessagesInaChannelDelta>((content) => { })
					.WithParsed<GetChannelMessage>((content) => { })
					.WithParsed<GetAReplyToAmessage>((content) => { })
					.WithParsed<ListChats>((content) => { })
					.WithParsed<ListMessageInaChat>((content) => { })
					.WithParsed<GetMessageInChat>((content) => { })
					.WithParsed<ListAllHostedContentInChannel>((content) => { })
					.WithParsed<GetHostedContent>((content) => { })
					.WithNotParsedAsync(async (errors) => {
						//Logger.Error(string.Join("\r\n",errors.Select(x=>x.ToString())));
					});
				
				Thread.CurrentThread.Join(TimeSpan.FromSeconds(1));
			}
		}

		public async Task StopAsync(CancellationToken cancellationToken) {

		}
	}
}
