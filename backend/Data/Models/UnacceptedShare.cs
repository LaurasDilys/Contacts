namespace Data.Models
{
    public class UnacceptedShare
    {
        public string ContactId { get; set; }
        public Contact Contact { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
