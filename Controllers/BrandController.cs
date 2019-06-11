using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogoCatalog_API.Models;
using LogoCatalog_API.Models.DTO;
using LogoCatalog_API.Models.Enums;
using LogoCatalog_API.Models.QueryParams;
using LogoCatalog_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace LogoCatalog_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly BrandRepository _repo;
        public BrandController(BrandRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult> GetBrands()
        {
            var res = await _repo.GetAllBrands();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {
            var brand = await _repo.GetBrandById(id);
            return Ok(brand);
        }
        [HttpPost]
        //public async Task<IActionResult> PutBrand(Guid id, BrandToUpdateDTO brand,
        //   IFormFile logo, ICollection<Tag> tags)
        public async Task<IActionResult> PutBrand(BrandQuery brand)

        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (brand.Logo != null && !Enum.GetNames(typeof(ImageExt))
               .Contains(System.IO.Path.GetExtension(brand.Logo.FileName)
               .Trim().ToUpper()))
                return BadRequest("File not supported");

            var dbBrand = await _repo.EditBrand(brand);
            if (dbBrand != null)
            {
                if (brand.Logo != null)
                {
                    await _repo.AssignLogo(brand.Logo, dbBrand.Id);
                }
                if (brand.Tags.Count > 0)
                {
                    await _repo.AssignTags(brand.Tags, dbBrand.Id);
                }
            }
            return CreatedAtAction("GetBrand", new { id = dbBrand.Id }, dbBrand);
        }
        [HttpPost]
        public async Task<ActionResult> PostBrand(BrandQuery brand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (brand.Logo != null && !Enum.GetNames(typeof(ImageExt))
                .Contains(System.IO.Path.GetExtension(brand.Logo.FileName)
                .Trim().ToUpper()))
                return BadRequest("File not supported");
            var dbBrand = await _repo.AddBrand(brand);
            if (dbBrand != null)
            {
                if (brand.Logo != null)
                {
                    await _repo.AssignLogo(brand.Logo, dbBrand.Id);
                }
                if (brand.Tags.Count > 0)
                {
                    await _repo.AssignTags(brand.Tags, dbBrand.Id);
                }
            }

            return CreatedAtAction("GetBrand", new { id = dbBrand.Id }, dbBrand);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBrand(Guid id)
        {
            await _repo.DeleteBrand(id);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> DownloadLogo(Guid brandId, ImageExt extension)
        {
            var file = await _repo.DownloadLogo(brandId, extension);
            return Ok(file);
        }
    }
}