using FinanceNewsService.Data;
using FinanceNewsService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceNewsService.Services
{
    public class NewsService : INewsService.INewsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<NewsService> _logger;

        public NewsService(AppDbContext context, ILogger<NewsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all news articles from the database.");
            var articles = await _context.NewsArticles.ToListAsync();
            _logger.LogInformation("Retrieved {Count} articles.", articles.Count);
            return articles;
        }

        public async Task<NewsArticle?> GetByIdAsync(int id)
        {
            _logger.LogInformation("Fetching article with ID: {Id}.", id);
            var article = await _context.NewsArticles.FindAsync(id);
            if (article == null)
            {
                _logger.LogWarning("Article with ID {Id} not found.", id);
            }
            return article;
        }

        public async Task AddAsync(NewsArticle newsArticle)
        {
            _logger.LogInformation("Adding new article with title: {Title}.", newsArticle.Headline);
            _context.NewsArticles.Add(newsArticle);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Article added successfully with ID: {Id}.", newsArticle.NewsId);
        }

        public async Task UpdateAsync(NewsArticle newsArticle)
        {
            _logger.LogInformation("Updating article with ID: {Id}.", newsArticle.NewsId);
            var existing = await _context.NewsArticles.FindAsync(newsArticle.NewsId);
            if (existing == null)
            {
                _logger.LogWarning("Cannot update. Article with ID {Id} not found.", newsArticle.NewsId);
                return;
            }

            existing.Headline = newsArticle.Headline;
            existing.Content = newsArticle.Content;
            existing.ArticleLink = newsArticle.ArticleLink;
            existing.ImageLink = newsArticle.ImageLink;
            existing.Company = newsArticle.Company;

            _context.NewsArticles.Update(existing);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Article with ID {Id} updated successfully.", newsArticle.NewsId);
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting article with ID: {Id}.", id);
            var article = await _context.NewsArticles.FindAsync(id);
            if (article == null)
            {
                _logger.LogWarning("Cannot delete. Article with ID {Id} not found.", id);
                return;
            }

            _context.NewsArticles.Remove(article);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Article with ID {Id} deleted successfully.", id);
        }
        public async Task<IEnumerable<NewsArticle>> SearchAsync(string searchTerm)
        {
            _logger.LogInformation("Searching for articles matching: {SearchTerm}.", searchTerm);
            var lowerCaseSearchTerm = searchTerm.ToLower();
            var articles = await _context.NewsArticles
                .Where(a => a.Headline.ToLower().Contains(searchTerm) 
                            || a.Content.ToLower().Contains(searchTerm) 
                            || a.Company.ToLower().Contains(searchTerm))
                .ToListAsync();
    
            _logger.LogInformation("Found {Count} articles matching the search term.", articles.Count);
            return articles;
        }

    }
}
