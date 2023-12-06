using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_ai_project.Models;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_ai_project.Controllers;

public class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;

    public ChatController(ILogger<ChatController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
