using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adminstration.Models;
using Microsoft.AspNetCore.Authorization;

namespace Adminstration.Controllers;
[Authorize]
public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;

    public ProfileController(ILogger<ProfileController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

  
}