using Microsoft.EntityFrameworkCore;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRESTServices.Data
{
    public class CategoryData : ICategoryData
    {
        private readonly AppDbContext _context;
        public CategoryData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                    return false;

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
      
                throw new Exception("Error deleting category", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            try
            {
                var categories = await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync();
                return categories;
            }
            catch (Exception ex)
            {
      
                throw new Exception("Error getting all categories", ex);
            }
        }

        public async Task<Category> GetById(int id)
        {
            try
            {
                return await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            }
            catch (Exception ex)
            {
      
                throw new Exception("Error getting category by ID", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetByName(string name)
        {
            try
            {
                return await _context.Categories.Where(c => c.CategoryName.Contains(name)).ToListAsync();
            }
            catch (Exception ex)
            {
      
                throw new Exception("Error getting categories by name", ex);
            }
        }

        public async Task<int> GetCountCategories(string name)
        {
            try
            {
                return await _context.Categories.CountAsync(c => c.CategoryName.Contains(name));
            }
            catch (Exception ex)
            {
      
                throw new Exception("Error getting count of categories", ex);
            }
        }

        public async Task<IEnumerable<Category>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            try
            {
                var categories = await _context.Categories
                    .Where(c => c.CategoryName.Contains(name))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return categories;
            }
            catch (Exception ex)
            {
      
                throw new Exception("Error getting categories with paging", ex);
            }
        }

        public async Task<Category> Insert(Category entity)
        {
            try
            {
                _context.Categories.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
      
                throw new Exception("Error inserting category", ex);
            }
        }

        public async Task<int> InsertWithIdentity(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return category.CategoryId;
            }
            catch (Exception ex)
            {
      
                throw new Exception("Error inserting category with identity", ex);
            }
        }

        public async Task<Category> Update(int id, Category entity)
        {
            try
            {
                var existingCategory = await _context.Categories.FindAsync(id);
                if (existingCategory == null)
                    return null;

                existingCategory.CategoryName = entity.CategoryName; 
                await _context.SaveChangesAsync();
                return existingCategory;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating category", ex);
            }
        }
    }
}
