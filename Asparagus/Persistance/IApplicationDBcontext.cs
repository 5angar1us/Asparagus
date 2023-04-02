using Asparagus.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Asparagus.Persistance
{
    public interface IApplicationDBcontext
    {
        DbSet<Message> Messages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        int SaveChanges();
    }
}