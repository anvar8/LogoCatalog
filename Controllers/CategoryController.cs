using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogoCatalog_API.Models;
using LogoCatalog_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogoCatalog_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _repo;
        public CategoryController(CategoryRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            return Ok(await _repo.GetAllCategories());
        }
        [HttpGet]
        public async Task<ActionResult<Category>> GetCategory([FromBody] Guid id)
        {
            var cat = await _repo.GetCategoryById(id);
            return Ok(cat);
        }
        [HttpPut]
        public async Task<IActionResult> PutCategory(Category cat)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _repo.EditCategory(cat);
            return Ok(cat);
        }
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category cat)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var dbCat = await _repo.AddCategory(cat);
            return CreatedAtAction("GetCategory", new { id = dbCat.Id }, dbCat);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(Guid id)
        {
            var deletedCat = await _repo.DeleteCategory(id);
            return Ok(deletedCat);
        }
    }
}