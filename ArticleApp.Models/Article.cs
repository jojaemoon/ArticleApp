using System;
using System.Collections.Generic;
using System.Text;

namespace ArticleApp.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
