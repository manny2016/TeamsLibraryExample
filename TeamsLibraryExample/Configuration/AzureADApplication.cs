using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsLibraryExample.Configuration {
	public class AzureADApplication {
		public string AppId { get; set; }
		public string Secret { get; set; }
		public string TenantId { get; set; }
	}
}
