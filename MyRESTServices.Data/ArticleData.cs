using Microsoft.EntityFrameworkCore;
using MyRESTServices.Data.Interfaces;
using MyRESTServices.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRESTServices.Data
{
    public class ArticleData : IArticleData
    {
        private readonly AppDbContext _context;
        public ArticleData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var article = await _context.Articles.FindAsync(id);
                if (article == null)
                    return false;

                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {

                throw new Exception("Error deleting article", ex);
            }
        }

        public async Task<IEnumerable<Article>> GetAll()
        {
            try
            {
                var articles = await _context.Articles.OrderBy(c => c.Title).ToListAsync();
                return articles;
            }
            catch (Exception ex)
            {

                throw new Exception("Error getting all articles", ex);
            }
        }

        public Task<IEnumerable<Article>> GetArticleByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Article>> GetArticleWithCategory()
        {
            throw new NotImplementedException();
        }

        public async Task<Article> GetById(int id)
        {
            try
            {
                return await _context.Articles.FirstOrDefaultAsync(c => c.ArticleId == id);
            }
            catch (Exception ex)
            {

                throw new Exception("Error getting article by ID", ex);
            }
        }

        public async Task<int> GetCountArticles(string name)
        {
            try
            {
                return await _context.Articles.CountAsync(c => c.Title.Contains(name));
            }
            catch (Exception ex)
            {

                throw new Exception("Error getting count of articles", ex);
            }
        }

        public Task<int> GetCountArticles()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Article>> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            try
            {
                var articles = await _context.Articles
                    .Where(c => c.Title.Contains(name))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                return articles;
            }
            catch (Exception ex)
            {

                throw new Exception("Error getting articles with paging", ex);
            }
        }

        public Task<IEnumerable<Article>> GetWithPaging(int categoryId, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Article> Insert(Article entity)
        {
            try
            {
                _context.Articles.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                throw new Exception("Error inserting article", ex);
            }
        }

        public Task<Task> InsertArticleWithCategory(Article article)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertWithIdentity(Article article)
        {
            try
            {
                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
                return article.ArticleId;
            }
            catch (Exception ex)
            {

                throw new Exception("Error inserting article with identity", ex);
            }
        }

        public async Task<Article> Update(int id, Article entity)
        {
            try
            {
                var existingArticle = await _context.Articles.FindAsync(id);
                if (existingArticle == null)
                    return null;

                existingArticle.Title = entity.Title;
                existingArticle.Pic = entity.Pic;
                existingArticle.Details = entity.Details;
                existingArticle.CategoryId = entity.CategoryId;
                existingArticle.IsApproved = entity.IsApproved;

                await _context.SaveChangesAsync();
                return existingArticle;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating article", ex);
            }
        }
    }
}



