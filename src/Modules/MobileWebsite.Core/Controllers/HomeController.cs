using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OrchardCore.DisplayManagement.Notify;

namespace MobileWebsite.Core.Controllers;

public class HomeController : Controller
{
    private readonly IStringLocalizer T;
    private readonly INotifier _notifier;
    private readonly IHtmlLocalizer H;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IStringLocalizer<HomeController> localizer, INotifier notifier, IHtmlLocalizer<HomeController> htmlLocalizer, ILogger<HomeController> logger)
    {
        _notifier = notifier;
        T = localizer;
        H = htmlLocalizer;
        _logger = logger;
    }

    public ActionResult Index()
    {
        ViewData["Message"] = T["Hello world!"];

        _notifier.SuccessAsync(H["Hello World!"]);

        _logger.LogError("Hello log!");

        return View();
    }
}
