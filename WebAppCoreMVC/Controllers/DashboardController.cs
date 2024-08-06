using Microsoft.AspNetCore.Mvc;

namespace WebAppCoreMVC.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
