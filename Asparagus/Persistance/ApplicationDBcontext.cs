using Asparagus.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Asparagus.Persistance
{
    public class ApplicationDBcontext : DbContext, IApplicationDBcontext
    {
        public DbSet<Message> Users { get; set; }

        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }





    }

}
