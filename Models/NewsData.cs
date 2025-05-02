using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceNewsService.Models
{
    [Table("NewsArticles")]
    public class NewsArticle
    {
        [Key]
        [Column("news_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsId { get; set; } 
        
        [Column("headline")]
        [Required]
        [MaxLength(255)]
        public string Headline { get; set; }

        [Column("content")]
        [Required]
        public string Content { get; set; }

        [Column("article_link")]
        [Required]
        [MaxLength(500)]
        public string ArticleLink { get; set; }

        [Column("image_link")]
        [MaxLength(500)]
        public string? ImageLink { get; set; }
        
        [Column("company")]
        [MaxLength(500)]
        public string? Company { get; set; }
    }
}