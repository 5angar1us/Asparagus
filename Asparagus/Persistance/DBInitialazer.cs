using Asparagus.Domain.Models;
using Bogus;
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

        public static void CreateDefault(ApplicationDBcontext DBcontext)
        {
            var testOrders = new Faker<Message>()
                .RuleFor(o => o.Email, f => f.Person.Email)
                .RuleFor(o => o.Name, f => f.Person.FirstName)
                .RuleFor(o => o.DateTime, f => f.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now));
            
            var messages = testOrders.Generate(10);
            
            DBcontext.Messages.AddRange(messages);
            DBcontext.Messages.AddRange(GenerateData(messages[1], count: 2, dayOffset: 4));
            DBcontext.Messages.AddRange(GenerateData(messages[2], count: 2, dayOffset: 8));
            DBcontext.Messages.AddRange(GenerateData(messages[3], count: 5, dayOffset: 3));
            DBcontext.Messages.AddRange(GenerateData(messages[4], count: 10, dayOffset: 2));
            DBcontext.Messages.AddRange(GenerateData(messages[5], count: 6, dayOffset: 1));
            DBcontext.Messages.AddRange(GenerateData(messages[6], count: 15, dayOffset: 5));
            DBcontext.Messages.AddRange(GenerateData(messages[7], count: 6, dayOffset: 4));
            
            DBcontext.SaveChanges();
        }

        private static IReadOnlyList<Message> GenerateData(Message message, int count, int dayOffset)
        {
            var users = new List<Message>();
            for (int i = 0; i < count; i++)
            {
                users.Add(
                    new Message(
                        message.Name,
                        message.Email,
                        DateTime.UtcNow.AddDays(i * dayOffset)
                        )
                    );
            }
            return users;
        }
    }
}
