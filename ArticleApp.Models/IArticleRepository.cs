using Dul.Domain.Common;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArticleApp.Models
{
    public interface IArticleRepository
    {
        Task<Article> AddArticleAsync(Article article);
        Task<List<Article>> GetArticlesAsync();
        Task<Article> GetArticleByIdAsync(int id);
        Task<Article> EditArticleAsync(Article article);
        Task DeleteArticleAsync(int i);

        Task<PagingResult<Article>> GetAllAsync(int pageIndex, int pageSize);
    }
}

