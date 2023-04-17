using System;
namespace DotNetRedirect.Models
{
	public class ClientCredentialsRequest
	{
		public ClientCredentialsRequest()
		{
			grant_type = "client_credentials";

        }

		public string grant_type { get; private set; }
		public string client_id { get; set; }
        public string client_secret { get; set; }
		public string audience { get; set; }
        public string scope { get; set; }
    }
}

