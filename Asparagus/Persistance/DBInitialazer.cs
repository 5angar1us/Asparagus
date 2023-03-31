using Asparagus.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Asparagus.Persistance
{
    public class DBInitialazer
    {
        public static void Initialize(ApplicationDBcontext DBcontext)
        {
            if (DBcontext.Database.EnsureCreated())
            {
                CreateDefault(DBcontext);
            }

        }

        private static void CreateDefault(ApplicationDBcontext DBcontext)
        {
            DBcontext.Users.Add(new Message("puma", "puma@yandex.ru", DateTime.Now));
            DBcontext.Users.Add(new Message("Barsik", "barsik@yandex.ru", DateTime.Now));
            DBcontext.Users.Add(new Message("Barsik2", "barsik@yandex.ru", DateTime.Now.AddDays(1)));

            DBcontext.Users.Add(new Message("suslik1", "suslik1@yandex.ru", DateTime.Now));
            DBcontext.Users.Add(new Message("suslik2", "suslik1@yandex.ru", DateTime.Now.AddDays(2)));
            DBcontext.Users.Add(new Message("suslik3", "suslik1@yandex.ru", DateTime.Now.AddDays(4)));

            DBcontext.Users.AddRange(GenerateData("turtle", count: 5, dayOffset: 3));
            DBcontext.Users.AddRange(GenerateData("lion", count: 10, dayOffset: 2));
            DBcontext.Users.AddRange(GenerateData("mouse", count: 6, dayOffset: 1));
            DBcontext.Users.AddRange(GenerateData("giraffe", count: 15, dayOffset: 5));
            DBcontext.Users.AddRange(GenerateData("lama", count: 6, dayOffset: 4));
            
            DBcontext.SaveChanges();
        }
        private static IReadOnlyList<Message> GenerateData(string name, int count, int dayOffset)
        {
            var users = new List<Message>();
            for (int i = 0; i < count; i++)
            {
                users.Add(
                    new Message(
                        name,
                        name + "@yandex.ru",
                        DateTime.UtcNow.AddDays(i * dayOffset)
                        )
                    );
            }
            return users;
        }
    }
}
