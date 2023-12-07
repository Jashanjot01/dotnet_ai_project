using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_ai_project.Models;
using Microsoft.AspNetCore.Authorization;
using dotnet_ai_project.Data;

namespace dotnet_ai_project.Controllers;

public class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;
    private readonly ApplicationDbContext _context;

    public ChatController(ILogger<ChatController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Authorize]
    public IActionResult Index()
    {
        // fetchng all profiles except users own profile
        var email = User?.Identity?.Name;
        var profiles = _context.Profiles.Where(p => p.Email != email).ToList();
        var me = _context.Profiles.Where(p => p.Email == email).FirstOrDefault();
        ViewBag.profiles = profiles;
        ViewBag.images = new List<string>();
        ViewBag.me = me;

        // iterate through these user profiles and convert their profile pic to base64
        foreach (var profile in profiles)
        {
            ViewBag.images.Add("data:image/jpeg;base64," + Convert.ToBase64String(profile.ProfilePic));
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SaveMessage(string sender, string message, string receiver)
    {
        // save message to database
        var chat = new ChatModel
        {
            Sender = sender,
            Message = message,
            Receiver = receiver,
            Time = DateTime.Now
        };
        _context.Chats.Add(chat);
        await _context.SaveChangesAsync();
        return Ok();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
