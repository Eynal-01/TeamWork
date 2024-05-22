using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;

namespace TeamWork.ClientApp.Models
{
    public class CustomIdentityDbContext : IdentityDbContext<CustomIdentityUser, CustomIdentityRole, string>
    {
        public CustomIdentityDbContext()
        {
        }

        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options)
           : base(options)
        {

        }


    }
}
