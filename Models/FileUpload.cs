using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace dotnet_ai_project.Models
{
    public class FileUpload
    {
        public IFormFile ProfilePic { get; set; }

        public ProfileModel Profile { get; set; }
    }
}