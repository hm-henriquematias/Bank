using Microsoft.AspNetCore.Mvc;

namespace BankWeb.Controllers
{
    public class HomeController : ControllerBase
    {
        public string Index()
        {
            return "index";
        }
    }
}
