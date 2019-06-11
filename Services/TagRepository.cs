using LogoCatalog_API.Data;
using LogoCatalog_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogoCatalog_API.Services
{
    public class TagRepository
    {
        private readonly AppDBContext _context;
        public TagRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _context.Tags.ToArrayAsync();
        }
        public async Task<Tag> GetTagById(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
                throw new Exception("not found");
            return tag;
        }
        public async Task<Tag> AddTag(Tag tag)
        {

            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task EditTag(Tag tag)
        {
            var dbTag = await GetTagById(tag.Id);
            dbTag.Description = tag.Description;
            await _context.SaveChangesAsync();
        }

        public async Task<Tag> DeleteTag(Guid id)
        {
            var tag = await GetTagById(id);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return tag;
        }
    }
}
