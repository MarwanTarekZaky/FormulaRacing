using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Formula.Models;
using Formula.Models.SharedResources;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using IoC.Resources;
using IoC.Services.Interface;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Formula.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;   
    private readonly IStringLocalizer<SharedResource> _stringLocalizer;
    private readonly IHtmlLocalizer<SharedResource> _htmlLocalizer;
    private readonly IRaceService _raceService;
    private readonly IEventService _eventService;
    public HomeController(ILogger<HomeController> logger, 
        IStringLocalizer<SharedResource> stringLocalizer,
        IHtmlLocalizer<SharedResource> htmlLocalizer, IRaceService raceService, ICarService carService, IEventService eventService)
    {
        _logger = logger;
        _stringLocalizer = stringLocalizer;
        _htmlLocalizer = htmlLocalizer;
        _raceService = raceService;
        _eventService = eventService;
    }
    public async Task<IActionResult> Index()
    {
        ViewBag.races  = (await _raceService.GetAllAsync(u => u.ShowOnHomepage == true)).OrderBy(u => u.Date).Take(3);
        
        ViewBag.events = (await _eventService.GetAllAsync(u => u.ShowOnHomepage == true)).OrderBy(u => u.Date).Take(3);
        return View();
    }

    public IActionResult ChangeLanguage(ChangeLanguageViewModel model)
    {
        if (model.IsSubmit)
        {
            HttpContext myContext = this.HttpContext;
            ChangeLanguage_SetCookie(myContext, model.SelectedLanguage);
            //doing funny redirect to get new Request Cookie
            //for presentation
            return LocalRedirect("/Home/ChangeLanguage");
        }

        //prepare presentation
        ChangeLanguage_PreparePresentation(model);
        return View(model);
    }
    private void ChangeLanguage_PreparePresentation(ChangeLanguageViewModel model)
    {
        model.ListOfLanguages = new List<SelectListItem>
        {
            new SelectListItem
            {
                Text = "English",
                Value = "en"
            },

            new SelectListItem
            {
                Text = "Arabic",
                Value = "ar",
            }
        };
    }
    
    private void ChangeLanguage_SetCookie(HttpContext myContext, string? culture)
    {
        if(culture == null) { throw new Exception("culture == null"); };

        //this code sets .AspNetCore.Culture cookie
        myContext.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) }
        );
    }

    public IActionResult LocalizationExample(LocalizationExampleViewModel model)
    {
        //so, here we use IStringLocalizer
        model.IStringLocalizerInController = _stringLocalizer["Hola"];
        //so, here we use IHtmlLocalizer
        model.IHtmlLocalizerInController = _htmlLocalizer["Hola"];
        return View(model);
    }

    
   
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}