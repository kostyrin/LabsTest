using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NewLabsTest.Domain.Models;

namespace NewLabsTest.Domain
{
    public class NewLabsTestContext : DbContext
    {
        public NewLabsTestContext(DbContextOptions<NewLabsTestContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
        }
    }
}
