using System;
namespace DotNetRedirect.Models
{
	public class UpdateUserResponse
	{	
		public string user_id { get; set; }
		public string email { get; set; }
		public AppMetadata app_metadata { get; set; }
	}
}

