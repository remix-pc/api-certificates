using CertificatesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CertificatesAPI.Data
{
    public class AppDbContext : DbContext
    {


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Certificate> Certificates { get; set; }


    }
}
