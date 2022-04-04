using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsMVC.Models
{
    public class Fellowship
    {
        public int Id { get; set; }

        public int FriendId { get; set; }

        public Friends Friends { get; set; }

        public int FellowId { get; set; }

        public Fellow Fellow { get; set; }


    }
}
