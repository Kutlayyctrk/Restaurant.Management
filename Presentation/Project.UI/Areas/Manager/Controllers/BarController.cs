using Microsoft.AspNetCore.Mvc;

[Area("Manager")]
public class BarController : Controller
{
  

    public IActionResult DashBoard()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> RecipeList()
    {
     
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ActiveOrders()
    {
      
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> MenuProducts()
    {
     
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Reports()
    {
      
        return View();
    }
}