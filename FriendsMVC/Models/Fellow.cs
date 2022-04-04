using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsMVC.Models
{
    public class Fellow
    {
        public int FellowId { get; set; }

        public string Name { get; set; }

        public List<Fellowship> Fellowship { get; set; }
    }
}
