using Microsoft.EntityFrameworkCore;
using ProjectAssignment.Models;
using System;

namespace ProjectAssignment.Data
{
    public class EcommerceDbContext: DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

        public DbSet<Categories> Category { get; set; }
        public DbSet<Products> Product { get; set; }
    }
}
