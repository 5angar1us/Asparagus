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
            DBInitialazer.CreateDefault(DBcontext);

            DBcontext.SaveChanges();
        }
    }
}