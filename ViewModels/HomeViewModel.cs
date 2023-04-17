using System;
namespace DotNetRedirect.ViewModels
{
	public class HomeViewModel
	{
		public HomeViewModel(bool isSuccess)
		{
            IsSuccess = isSuccess;
            HasState = false;

        }
		public bool IsSuccess { get; private set; }
        public bool HasState{ get; set; }
    }
}

