using Dul.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace ArticleApp.Models
{
    public class Article : AuditableBase
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

    }
}
