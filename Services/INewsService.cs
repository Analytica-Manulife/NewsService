using FinanceNewsService.Models;

namespace FinanceNewsService.INewsService;

public interface INewsService
{
    Task<IEnumerable<NewsArticle>> GetAllAsync();
    Task<NewsArticle?> GetByIdAsync(int id);
    Task AddAsync(NewsArticle newsArticle);
    Task UpdateAsync(NewsArticle newsArticle);
    Task DeleteAsync(int id);
    Task<IEnumerable<NewsArticle>> SearchAsync(string searchTerm);
}