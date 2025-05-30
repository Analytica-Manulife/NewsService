using FinanceNewsService.Models;
using FinanceNewsService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinanceNewsService.Controllers
{
    [ApiController]
    [Route("news/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService.INewsService _newsService;
        private readonly ILogger<NewsController> _logger;

        public NewsController(INewsService.INewsService newsService, ILogger<NewsController> logger)
        {
            _newsService = newsService;
            _logger = logger;
        }
        
        [HttpGet("status")]
        public IActionResult Get() => Ok("Finance News Service is running");
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsArticle>>> GetAll()
        {
            _logger.LogInformation("Fetching all news articles.");
            var articles = await _newsService.GetAllAsync();
            _logger.LogInformation("Retrieved {Count} articles.", articles.Count());
            return Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsArticle>> GetById(int id)
        {
            _logger.LogInformation("Fetching article with ID {Id}.", id);
            var article = await _newsService.GetByIdAsync(id);
            if (article == null)
            {
                _logger.LogWarning("Article with ID {Id} not found.", id);
                return NotFound();
            }

            return Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] NewsArticle article)
        {
            _logger.LogInformation("Creating a new article with title: {Title}.", article.Headline);
            await _newsService.AddAsync(article);
            _logger.LogInformation("Article created with ID: {Id}.", article.NewsId);
            return CreatedAtAction(nameof(GetById), new { id = article.NewsId }, article);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] NewsArticle updatedArticle)
        {
            _logger.LogInformation("Updating article with ID: {Id}.", id);

            if (id != updatedArticle.NewsId)
            {
                _logger.LogWarning("ID mismatch: URL ID = {UrlId}, Body ID = {BodyId}.", id, updatedArticle.NewsId);
                return BadRequest("ID mismatch");
            }

            var existing = await _newsService.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Article with ID {Id} not found for update.", id);
                return NotFound();
            }

            await _newsService.UpdateAsync(updatedArticle);
            _logger.LogInformation("Article with ID {Id} successfully updated.", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Attempting to delete article with ID {Id}.", id);
            var existing = await _newsService.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Article with ID {Id} not found for deletion.", id);
                return NotFound();
            }

            await _newsService.DeleteAsync(id);
            _logger.LogInformation("Article with ID {Id} successfully deleted.", id);
            return NoContent();
        }

        [HttpGet("search/{searchTerm}")]
        public async Task<ActionResult<IEnumerable<NewsArticle>>> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                _logger.LogWarning("Search term is empty or invalid.");
                return BadRequest("Search term cannot be empty.");
            }

            var articles = await _newsService.SearchAsync(searchTerm);

            if (articles == null || !articles.Any())
            {
                _logger.LogInformation("No articles found matching the search term: {SearchTerm}.", searchTerm);
                return NotFound("No articles found matching the given search term.");
            }

            return Ok(articles);
        }

    }
}
