﻿using Microsoft.AspNetCore.Identity;

namespace myMusicApi.model
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
        
        
    }
   
}
