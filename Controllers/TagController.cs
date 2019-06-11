using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogoCatalog_API.Models;
using LogoCatalog_API.Models.QueryParams;
using LogoCatalog_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogoCatalog_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly TagRepository _repo;
        public TagController(TagRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetTags()
        {
            return Ok(await _repo.GetAllTags());
        }
        [HttpGet]
        public async Task<ActionResult<Tag>> GetTag([FromBody] Guid id)
        {
            var tag = await _repo.GetTagById(id);
            return Ok(tag);
        }
        [HttpPut]
        public async Task<IActionResult> PutTag(Tag tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _repo.EditTag(tag);
            return Ok(tag);
        }
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var dbTag = await _repo.AddTag(tag);
            return CreatedAtAction("GetTag", new { id = dbTag.Id }, dbTag);
        }
        [HttpDelete]
        public async Task<ActionResult<Tag>> DeleteTag([FromBody] Guid id)
        {
            var deletedTag = await _repo.DeleteTag(id);
            return Ok(deletedTag);
        }

    }
}