using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using DotNetRedirect.Models;
using DotNetRedirect.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using DotNetRedirect.ViewModels;
using System.Net;
using System;

namespace SampleMvcApp.Controllers
{
    public class HomeController : Controller
    {        

        Auth0Config _configuration;
        IUserManagementService _userManagementService;

        public HomeController(IOptions<Auth0Config> options, IUserManagementService userManagementService)
        {
            _configuration = options.Value;
            _userManagementService = userManagementService;
        }
       
        public IActionResult Index()
        {
            var isSuccess = false;
            var vm = new HomeViewModel(isSuccess);            
           return View(vm);            
        }

        [HttpGet("emailprompt")]        
        public void EmailPrompt(string state)
        {
            if (state == null)
            {
                throw new Exception("Invalid state");
            }

            var redirectUri = "/account/login";

           Response.Cookies.Append("state", state);
           Response.Redirect(redirectUri);           
        }
        
        public void Continue()
        {
            var state = Request.Cookies["state"];         

            if (state == null)
            {
                throw new Exception("Missing state");
            }

            Response.Cookies.Delete("state");          
            Response.Redirect($"https://{_configuration.Domain}/continue?state={state}");
        }       



        [HttpPost]
        public async Task<IActionResult> UpdateEmail(string email)
        {            
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var isSuccess = await _userManagementService.UpdateEmail(userId, email);
            var vm = new HomeViewModel(isSuccess);

            if (Request.Cookies["state"] != null)
            {
                vm.HasState = true;
            }
            
            return View("index", vm);
        }

        public IActionResult Error()
        {
            return View();
        }


       
    }
}
