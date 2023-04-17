using System;
namespace DotNetRedirect.Models
{
	public class UpdateUserRequest
    {
        public UpdateUserRequest(string email) {
            app_metadata = new AppMetadata
            {
                altEmail = email
            };
        }

		public AppMetadata app_metadata { get; set; }
	}

    public class AppMetadata
    {
        public string altEmail { get; set; }
    }
}

