namespace Asparagus.Domain.Models
{
    public class Message
    {
        public Message(string name, string email, DateTime dateTime)
        {
            Name = name;
            Email = email;
            DateTime = dateTime;
        }

        public Message()
        {

        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime DateTime { get; set; }
    }
}
