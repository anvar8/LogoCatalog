using LogoCatalog_API.Data;
using LogoCatalog_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using LogoCatalog_API.Utils;
using System.Threading.Tasks;
using LogoCatalog_API.Models.DTO;
using LogoCatalog_API.Models.Enums;
using LogoCatalog_API.Models.QueryParams;

namespace LogoCatalog_API.Services
{
    public class BrandRepository
    {
        private readonly AppDBContext _context;
        public BrandRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Brand>> GetAllBrands()
        {
            return await _context.Brands.ToArrayAsync();
        }
        public async Task<Brand> GetBrandById(Guid id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
                throw new Exception("brand not found");
            return brand;
        }
        public async Task AssignLogo(IFormFile logo, Guid brandId)
        {
            var dbBrand = await GetBrandById(brandId);
            if (logo != null)
            {
                if (logo.Length > 0)
                {
                    dbBrand.Logo = FileHelper.BytesFromFormFile(logo);
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task AssignTags(ICollection<Tag> tags, Guid brandId)
        {
            //var dbBrand = await GetBrandById(brandId);
            //var logoToTags = new List<BrandToTag>();
            //foreach (var t in tags)
            //{
            //    logoToTags.Add(new BrandToTag
            //    {
            //        Brand = dbBrand,
            //        BrandId = dbBrand.Id,
            //        Tag = t,
            //        TagId = t.Id
            //    });
            //}

          
            //dbBrand.LogoToTags = logoToTags;
            await _context.SaveChangesAsync();
        }
        public async Task<Brand> AddBrand(BrandQuery bToCreate)
        {
            Brand brand = new Brand();
            if (bToCreate.CategoryId == null)
            {
                throw new Exception("category required");
            }
            Category cat = await _context.Categories.FindAsync(bToCreate.CategoryId);
            brand.Category = cat;
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }
        public async Task<Brand> EditBrand(BrandQuery brand)
        {
            //TODO: see if extension is not supported
            //
            var dbBrand = await GetBrandById(brand.Id);
            dbBrand.Name = brand.Name;
            await _context.SaveChangesAsync();
            return dbBrand;
        }

        public async Task DeleteBrand(Guid id)
        {
            var brand = await GetBrandById(id);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }
        public async Task<dynamic> DownloadLogo (Guid id, ImageExt extension)
        {
            //todo download logo with proper extension
            return "logo";

        }
    }
}
