using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogoCatalog_API.Models
{
    public class Brand
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public byte[] Logo { get; set; }
  
    }
}
