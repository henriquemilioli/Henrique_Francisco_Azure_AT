using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsMVC.Models
{
    public class Friends
    {
        [Key]
        public int FriendId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public DateTime Birth { get; set; }
        public string FriendsPhoto { get; set; }
        public string Email { get; set; }
        

        public List<Friends> FriendsList { get; set; }
    }
}
