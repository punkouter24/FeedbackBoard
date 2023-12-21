namespace FeedbackBoard
{
    public class Message
    {
        public string Id { get; set; } // Unique identifier for the message
        public string Content { get; set; } // The content of the message
        public DateTime DateCreated { get; set; } // The date when the message was created
    }
}
