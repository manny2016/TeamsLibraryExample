using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibraryExample.Configuration {
	public class RuntimeSettings {
		public AzureADApplication GraphApi { get; set; }
		public AzureADApplication Teams { get; set; }

		public string[] Scope { get;set;}=  new string[] { "https://graph.microsoft.com/.default" };
	}

}
