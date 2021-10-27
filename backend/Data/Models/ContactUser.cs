namespace Data.Models
{
    public class ContactUser
    {
        public string ContactId { get; set; }
        public Contact Contact { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
