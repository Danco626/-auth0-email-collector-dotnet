using System;
namespace DotNetRedirect.Models;

public class Auth0Config
{
	public string Domain { get; set; }
    public string ClientId{ get; set; }
    public string ClientSecret { get; set; }    
    public string MgmtClientId { get; set; }
    public string MgmtClientSecret { get; set; }
    public string Auth0Domain { get; set; }
}

