using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2022_02_11.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {     
        [StringLength(25, ErrorMessage ="FirstName max length is 100")]
        public string? FirstName { get; set; }
        
        [StringLength(25, ErrorMessage = "LastName max length is 100")]
        public string? LastName { get; set; }

        //public ApplicationUser()
        //{
        //    CreatedPlaylists = new List<Playlist>();
        //    ModifiedPlaylists = new List<Playlist>();
        //}

        //[Required]
        //[StringLength(25)]
        //public string FirstName { get; set; }

        //[Required]
        //[StringLength(25)]
        //public string LastName { get; set; }

        //// Relationships 
        //public virtual List<Playlist> CreatedPlaylists { get; set; }
    }
}
