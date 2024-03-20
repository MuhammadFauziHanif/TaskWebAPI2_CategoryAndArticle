using AutoMapper;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;
using MyRESTServices.Domain.Models;
using MyRESTServices.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyRESTServices.BLL
{
    public class ArticleBLL : IArticleBLL
    {
        private readonly IArticleData _articleData;
        private readonly IMapper _mapper;

        public ArticleBLL(IArticleData articleData, IMapper mapper)
        {
            _articleData = articleData;
            _mapper = mapper;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return await _articleData.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error deleting article", ex);
            }
        }

        public async Task<IEnumerable<ArticleDTO>> GetAll()
        {
            try
            {
                var articles = await _articleData.GetAll();
                return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error getting all articles", ex);
            }
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticleByCategory(int categoryId)
        {
            try
            {
                var articles = await _articleData.GetArticleByCategory(categoryId);
                return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error getting articles by category", ex);
            }
        }

        public async Task<ArticleDTO> GetArticleById(int id)
        {
            try
            {
                var article = await _articleData.GetById(id);
                return _mapper.Map<ArticleDTO>(article);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error getting article by ID", ex);
            }
        }

        public async Task<IEnumerable<ArticleDTO>> GetArticleWithCategory()
        {
            try
            {
                var articles = await _articleData.GetArticleWithCategory();
                return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error getting articles with category", ex);
            }
        }

        public async Task<int> GetCountArticles()
        {
            try
            {
                return await _articleData.GetCountArticles();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error getting count of articles", ex);
            }
        }

        public async Task<IEnumerable<ArticleDTO>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            try
            {
                var articles = await _articleData.GetWithPaging(categoryId, pageNumber, pageSize);
                return _mapper.Map<IEnumerable<ArticleDTO>>(articles);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error getting articles with paging", ex);
            }
        }

        public async Task<ArticleDTO> Insert(ArticleCreateDTO article)
        {
            try
            {
                var articleEntity = _mapper.Map<Article>(article);
                var insertedArticle = await _articleData.Insert(articleEntity);
                return _mapper.Map<ArticleDTO>(insertedArticle);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error inserting article", ex);
            }
        }

        public async Task<int> InsertWithIdentity(ArticleCreateDTO article)
        {
            try
            {
                var articleEntity = _mapper.Map<Article>(article);
                return await _articleData.InsertWithIdentity(articleEntity);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error inserting article with identity", ex);
            }
        }

        public async Task<ArticleDTO> Update(int id, ArticleUpdateDTO article)
        {
            try
            {
                var articleEntity = _mapper.Map<Article>(article);
                var updatedArticle = await _articleData.Update(id, articleEntity);
                return _mapper.Map<ArticleDTO>(updatedArticle);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error updating article", ex);
            }
        }
    }
}
