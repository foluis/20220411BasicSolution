using System;
using System.Collections.Generic;

namespace _2022_02_11.Entities.DTOs
{
    public partial class TblUserProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
