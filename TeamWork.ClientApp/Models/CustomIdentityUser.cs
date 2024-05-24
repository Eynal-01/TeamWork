using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System;

namespace TeamWork.ClientApp.Models
{
    public class CustomIdentityUser : IdentityUser<string>
    {
        public DateTime BirthDate { get; set; }
        public string? City { get; set; }
        public int SeriaNo { get; set; }



        public CustomIdentityUser()
        {
            //Chats = new List<Chat>();
        }
    }
}
