using Asparagus.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Asparagus.Persistance
{
    public interface IApplicationDBcontext
    {
        DbSet<Message> Users { get; set; }
    }
}