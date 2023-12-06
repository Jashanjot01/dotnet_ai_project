using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dotnet_ai_project.Models;
using dotnet_ai_project.Data;
using Microsoft.AspNetCore.Identity;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
namespace dotnet_ai_project.Controllers;

public class ProfileController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public ProfileController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var email = User?.Identity?.Name;
        var user = await _context.Profiles
            .FirstOrDefaultAsync(m => m.Email == email);
        if (user == null)
        {
            return RedirectToAction("Create");
        }
        var viewModel = new ProfileModel { Email = email, UserBio = user.UserBio, Username = user.Username, ProfilePic = user.ProfilePic, Joined = user.Joined };
        ViewBag.image = "data:image/jpeg;base64," + Convert.ToBase64String(viewModel.ProfilePic);
        ViewBag.profile = viewModel;
        return View();
    }

    public IActionResult Create()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(FileUpload fileObj)
    {
        ProfileModel profile = fileObj.Profile;

        if (fileObj.ProfilePic.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                var email = User?.Identity?.Name;
                fileObj.ProfilePic.CopyTo(ms);
                var fileBytes = ms.ToArray();
                profile.ProfilePic = fileBytes;
                profile.Joined = DateTime.Now;
                profile.Email = email;

                _context.Profiles.Add(profile);
                await _context.SaveChangesAsync();
                ViewBag.profile = profile;
                return RedirectToAction("Index");
            }
        }

        return View("Create");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private byte[] GetImage(string ss64)
    {
        byte[] imageBytes = Convert.FromBase64String(ss64);
        return imageBytes;
    }


    [HttpPost]
    public async Task<IActionResult> EditAsync(FileUpload fileObj)
    {
        var email = User?.Identity?.Name;

        // Retrieve the profile with the matching email
        var existingProfile = await _context.Profiles.FirstOrDefaultAsync(p => p.Email == email);

        if (existingProfile != null)
        {
            // Update the specific fields
            existingProfile.UserBio = fileObj.Profile.UserBio;

            if (fileObj.ProfilePic != null && fileObj.ProfilePic.Length > 0)
            {
                // Update the ProfilePic only if a new file is provided
                using (var ms = new MemoryStream())
                {
                    await fileObj.ProfilePic.CopyToAsync(ms);
                    existingProfile.ProfilePic = ms.ToArray();
                }
            }

            // Update the rest of the profile properties if needed
            _context.Profiles.Update(existingProfile);
            await _context.SaveChangesAsync();

            ViewBag.profile = existingProfile;
            return RedirectToAction("Index");
        }

        return View("Edit");
    }

    public async Task<IActionResult> Edit()
    {
        var email = User?.Identity?.Name;
        var user = await _context.Profiles
            .FirstOrDefaultAsync(m => m.Email == email);
        ViewBag.image = "data:image/jpeg;base64," + Convert.ToBase64String(user.ProfilePic);
        

        return View("Edit");
    }
}
