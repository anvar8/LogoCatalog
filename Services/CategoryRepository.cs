using LogoCatalog_API.Data;
using LogoCatalog_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogoCatalog_API.Services
{
    public class CategoryRepository
    {
        private readonly AppDBContext _context;
        public CategoryRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToArrayAsync();
        }
        public async Task<Category> GetCategoryById(Guid id)
        {
            var cat = await _context.Categories.FindAsync(id);
            if (cat == null)
                throw new Exception("not found");
            return cat;
        }
        public async Task<Category> AddCategory(Category cat)
        {
            _context.Categories.Add(cat);
            await _context.SaveChangesAsync();
            return cat;
        }

        public async Task EditCategory(Category cat)
        {
         
            var dbCat = await GetCategoryById(cat.Id);
            dbCat.Description = cat.Description;
            await _context.SaveChangesAsync();
        }

        public async Task<Category> DeleteCategory(Guid id)
        {
            var cat = await GetCategoryById(id);
            _context.Categories.Remove(cat);
            await _context.SaveChangesAsync();
            return cat;
        }
    }
}
