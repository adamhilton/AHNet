using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AHNet.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace AHNet.Web.Tests.Data
{
    public class MockAHNetDbContext : IdentityDbContext<User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase();
        }
    }
}
