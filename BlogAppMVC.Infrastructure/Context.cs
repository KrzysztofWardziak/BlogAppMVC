using System;
using System.Collections.Generic;
using System.Text;
using BlogAppMVC.Domain.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogAppMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<BlogDetail> BlogDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
            
        }

    }
}
