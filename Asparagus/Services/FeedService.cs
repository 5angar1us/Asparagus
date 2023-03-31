using Asparagus.Domain.Models;
using Asparagus.Persistance;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Asparagus.Services
{
    public class FeedService
    {
        public ApplicationDBcontext _dbContext;

        public FeedService(ApplicationDBcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(MessageDto message)
        {
            var user = new Message(message.Name, message.Email, DateTime.Now);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public IReadOnlyList<FeedItem> GetAll()
        {
            var feed = _dbContext.Users
                .ToLookup(message => message.Email)
                .Select(messageGroup => NumberMessages(messageGroup))
                .SelectMany(x => x)
                .OrderBy(message => message.DateTime)
                .Reverse()
                .ToList();

            return feed;
        }
        private IEnumerable<FeedItem> NumberMessages(IGrouping<string, Message> messageGroup)
        {
            return messageGroup.OrderBy(message => message.DateTime)
                 .Select((message, index) =>
                 {
                     return new FeedItem(message.Name, message.Email, message.DateTime, index + 1);
                 });
        }
    }
}
