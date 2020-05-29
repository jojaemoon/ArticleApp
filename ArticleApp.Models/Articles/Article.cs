using Dul.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace ArticleApp.Models
{
    public class Article : AuditableBase
    {
        public int Id { get; set; }

        //[Required]
        [Required(ErrorMessage ="제목을 입력하세요.")]
        public string Title { get; set; }

    }
}
