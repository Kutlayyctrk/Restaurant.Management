using Microsoft.AspNetCore.Mvc;

namespace Project.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class HrController : Controller
    {
        public IActionResult DashBoard()
        {
            return View();
        }
    }
}
