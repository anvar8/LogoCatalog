using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogoCatalog_API.Models.DTO
{
    public class BrandToCreateDTO

    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
    }
}
