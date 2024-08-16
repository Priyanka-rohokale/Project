using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManegment.Models;

namespace TaskManegment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Attachments> Attachments { get; set; }

       
    }
}
