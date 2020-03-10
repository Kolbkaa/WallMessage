using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace WallMessage.Models
{
    public class User:IdentityUser<int>
    {
        public ICollection<Message> Messages { get; set; }
    }
}
