using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace dotnet_ai_project.Models
{
    public class ProfileModel
    {
        [Key]
        public string? Username { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? UserBio { get; set; }

        public DateTime Joined { get; set; }

        [Column(TypeName = "image")]
        public byte[] ProfilePic { get; set; }
    }
}