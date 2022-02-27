using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Polly;
using Polly.Extensions.Http;

using PollyTest.Handlers;
using System;
using System.Net.Http;

namespace TeamsLibraryExample.Common.Extensions {
	public static class HttpBuilderExtensions {
		public static IHttpClientBuilder AddUnauthorizedHttpMessageHandler(this IHttpClientBuilder builder, string handlerName) {
			return builder.AddHttpMessageHandler((services) =>
			{
				var passwordProvider = services.GetService<IPasswordProvider>();

				return new UnauthorizedHttpMessageHandler(handlerName, passwordProvider);
			});
		}
	}
}
