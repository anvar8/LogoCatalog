using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LogoCatalog_API.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
