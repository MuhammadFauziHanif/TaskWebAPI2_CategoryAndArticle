using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRESTServices.BLL
{
    public class CategoryBLL : ICategoryBLL
    {
        private readonly ICategoryData _categoryData;
        private readonly IMapper _mapper;

        public CategoryBLL(ICategoryData categoryData, IMapper mapper)
        {
            _categoryData = categoryData;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _categoryData.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error occurred while deleting category.", ex);
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            try
            {
                var categories = await _categoryData.GetAll();
                var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                return categoriesDto;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error occurred while getting all categories.", ex);
            }
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            try
            {
                var category = await _categoryData.GetById(id);
                var categoryDto = _mapper.Map<CategoryDTO>(category);
                return categoryDto;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error occurred while getting category with ID: {id}.", ex);
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetByName(string name)
        {
            try
            {
                var categories = await _categoryData.GetByName(name);
                var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                return categoriesDto;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error occurred while getting categories by name: {name}.", ex);
            }
        }

        public async Task<int> GetCountCategories(string name)
        {
            try
            {
                return await _categoryData.GetCountCategories(name);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error occurred while getting count of categories with name: {name}.", ex);
            }
        }

        public async Task<IEnumerable<CategoryDTO>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            try
            {
                var categories = await _categoryData.GetWithPaging(pageNumber, pageSize, name);
                var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
                return categoriesDto;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error occurred while getting categories with paging. Page number: {pageNumber}, Page size: {pageSize}, Name: {name}.", ex);
            }
        }

        public async Task<CategoryDTO> Insert(CategoryCreateDTO entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                var insertedCategory = await _categoryData.Insert(category);
                return _mapper.Map<CategoryDTO>(insertedCategory);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error occurred while inserting category.", ex);
            }
        }

        public async Task<CategoryDTO> Update(int id, CategoryUpdateDTO entity)
        {
            try
            {
                var category = _mapper.Map<Category>(entity);
                var updatedCategory = await _categoryData.Update(id, category);
                return _mapper.Map<CategoryDTO>(updatedCategory);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Error occurred while updating category with ID: {id}.", ex);
            }
        }
    }
}
