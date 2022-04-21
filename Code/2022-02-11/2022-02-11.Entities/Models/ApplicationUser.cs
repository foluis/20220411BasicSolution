using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _2022_02_11.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(25, ErrorMessage = "FirstName max length is 25")]
        public string? FirstName { get; set; } = string.Empty;

        [StringLength(25, ErrorMessage = "LastName max length is 25")]
        public string? LastName { get; set; } = string.Empty;
    }
}