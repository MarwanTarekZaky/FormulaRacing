using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Authorization;


namespace Adminstration.Controllers;
[Authorize]

public class HomeController : Controller
{
    

    public HomeController()
    {
       
        
    }

    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Documentation()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}