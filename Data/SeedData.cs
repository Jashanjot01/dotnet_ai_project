using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnet_ai_project.Models;

namespace SQLiteApp.Data;

public static class SeedData
{
    // this is an extension method to the ModelBuilder class
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfileModel>().HasData(
            GetProfiles()
        );
        
    }

    public static List<ProfileModel> GetProfiles()
    {
        List<ProfileModel> contacts = new List<ProfileModel>()
            {
                new ProfileModel()
                {
                    Username = "JohnDoe",
                    Email = "johndoe@gmail.com",
                    Joined = DateTime.Now,
                    UserBio = "I am a developer",
                    ProfilePic = new byte[0]
                },
                
            };
        return contacts;
    }
}