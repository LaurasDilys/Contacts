using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class UnacceptedShare
    {
        [Key]
        public string Id { get; set; }

        public string ContactId { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
