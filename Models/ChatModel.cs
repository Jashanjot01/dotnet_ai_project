using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace dotnet_ai_project.Models
{
    public class ChatModel
    {
        [Key]
        public int ID { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }
}