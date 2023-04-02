using Asparagus.Domain.Models;
using Asparagus.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asparagus.Tests.Common
{
    public class ApplicationContexFactory
    {
        public static ApplicationDBcontext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBcontext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDBcontext(options);
            context.Database.EnsureCreated();


            context.SaveChanges();
            return context;
        }

        public static void Destroy(ApplicationDBcontext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
