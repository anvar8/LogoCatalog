using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogoCatalog_API.Models.QueryParams
{
    public class BrandQuery
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public IFormFile Logo { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
