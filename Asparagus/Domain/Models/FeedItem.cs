namespace Asparagus.Domain.Models
{
    public class FeedItem
    {
        public FeedItem(string name, string email, DateTime dateTime, int count)
        {
            Name = name;
            Email = email;
            DateTime = dateTime;
            Count = count;
        }

        public string Name { get; init; }

        public string Email { get; init; }

        public DateTime DateTime { get; init; }

        public int Count { get; init; }
    }
}
