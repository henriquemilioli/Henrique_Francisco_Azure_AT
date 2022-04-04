using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsMVC.Models
{
    public class Countryes
    {
        [Key]
        public int IdCountry { get; set; }
        public string CountryName { get; set; }
        public string CountryPhoto { get; set; }
        [NotMapped]
        public IFormFile Picture { get; set; }
    }
}
