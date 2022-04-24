using System.ComponentModel.DataAnnotations;

namespace _2022_02_11.Entities.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        [StringLength(25, ErrorMessage = "FirstName max length is 25")]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(25, ErrorMessage = "FirstName max length is 25")]
        public string LastName { get; set; } = string.Empty;
    }
}