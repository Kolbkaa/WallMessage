using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WallMessage.Models
{
    public class Message
    {

        public Message()
        {
            CreateDate = DateTime.Now;
        }
  
        public int Id { get; set; }
        [Required]
        [Display(Name = "Wiadomość")]
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }
   
        public int UserId { get; set; }
     
        public User User { get; set; }
    }
}
