using Microsoft.Graph;
using TeamsLibraryExample;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using NLog;
using System.Web;

namespace PollyTest.Handlers {
	public class UnauthorizedHttpMessageHandler : DelegatingHandler {

		private readonly IPasswordProvider _passwordProvider;
		private readonly string _handlerName;
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		public UnauthorizedHttpMessageHandler(string handlerName, IPasswordProvider passwordProvider) {

			_handlerName = handlerName;
			_passwordProvider = passwordProvider;

		}
		bool modifyHttpContentRequired = false;

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

			ModifyHttpContentIfUnauthorizedHandled(request);
			var response = await base.SendAsync(request, cancellationToken);
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				modifyHttpContentRequired = true;
				Logger.Info($"Handled unauthorized exception. HandleName={_handlerName}");
			}
			return response;
		}

		private void ModifyHttpContentIfUnauthorizedHandled(HttpRequestMessage httpRequest) {

			try
			{
				if (!modifyHttpContentRequired) return;

				if (httpRequest.Content == null) return;

				if (httpRequest.Content.Headers.ContentType.MediaType == "application/x-www-form-urlencoded")
				{

					var content = httpRequest.Content.ReadAsStringAsync().Result;
					var parameters = content.Split('&').Select(x => x.Split('='))
							.ToDictionary(x => x[0], x => HttpUtility.UrlDecode(x[1]));

					parameters["client_secret"] = _passwordProvider.ForceRefeshAsync(parameters["client_id"]).Result;
					httpRequest.Content = new FormUrlEncodedContent(parameters);
					Logger.Info($"Modify http content because of unauthorized exception AppId={parameters["client_id"]};Hint={parameters["client_secret"].Substring(0, 3)}");
				}
			}
			catch (IndexOutOfRangeException ex)
			{
				Logger.Error(ex);
			}
			finally
			{
				modifyHttpContentRequired = false;
			}
		}
	}
}
