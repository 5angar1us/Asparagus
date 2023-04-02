using Asparagus.Domain.Models;
using Asparagus.Persistance;
using Asparagus.Services;
using Asparagus.Tests.Common;
using System.Xml.Linq;

namespace Asparagus.Tests
{
    public class Tests
    {
        private ApplicationDBcontext Context;

        [SetUp]
        public void Setup()
        {
            Context = ApplicationContexFactory.Create();
        }

        [TearDown]
        public void Cleanup()
        {
            ApplicationContexFactory.Destroy(Context);
        }

        [Test]
        public void CreateMessage_Success()
        {
            //
            var service = new FeedService(Context);
            var name = "Test1";
            var email = "Test1@gmail.com";

            //
            service.Add(new MessageDto(name, email));

            //
            Assert.That(Context.Messages.SingleOrDefault(m => m.Name == name && m.Email == email), Is.Not.Null);

        }

        [Test]
        public void GetAll_CorrectOrder()
        {
            //
            var service = new FeedService(Context);


            //
            CreateMessages(Context);

            //

            var messages = service.GetFeed();
            var isOrderCorrect = DescendingElementOrderByDateIsCorrect(messages);

            Assert.IsTrue(isOrderCorrect);
        }

        private bool DescendingElementOrderByDateIsCorrect(IReadOnlyList<FeedItem> messages)
        {
            return messages
                // create tuples
                .Zip(messages.Skip(1), Tuple.Create)
                // check if all are true
                .All(p => p.Item1.DateTime > p.Item2.DateTime);
        }

        private static void CreateMessages(ApplicationDBcontext DBcontext)
        {
            DBcontext.Messages.Add(new Message("puma", "puma@yandex.ru", DateTime.Now));
            DBcontext.Messages.Add(new Message("Barsik", "barsik@yandex.ru", DateTime.Now));
            DBcontext.Messages.Add(new Message("Barsik2", "barsik@yandex.ru", DateTime.Now.AddDays(1)));

            DBcontext.Messages.Add(new Message("suslik1", "suslik1@yandex.ru", DateTime.Now));
            DBcontext.Messages.Add(new Message("suslik2", "suslik1@yandex.ru", DateTime.Now.AddDays(2)));
            DBcontext.Messages.Add(new Message("suslik3", "suslik1@yandex.ru", DateTime.Now.AddDays(4)));

            DBcontext.Messages.AddRange(GenerateData("turtle", count: 5, dayOffset: 3));
            DBcontext.Messages.AddRange(GenerateData("lion", count: 10, dayOffset: 2));
            DBcontext.Messages.AddRange(GenerateData("mouse", count: 6, dayOffset: 1));
            DBcontext.Messages.AddRange(GenerateData("giraffe", count: 15, dayOffset: 5));
            DBcontext.Messages.AddRange(GenerateData("lama", count: 6, dayOffset: 4));

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