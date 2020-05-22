using Microsoft.EntityFrameworkCore;

namespace ArticleApp.Models
{
    public class ArticleAppDbContext : DbContext
    {

        // Install-Package Microsoft.EntityFrameworkCore.SqlServer
        // Install-Package Microsoft.EntityFrameworkCore.InMemory
        // Install-Package System.Configuration.ConfigurationManager
        public ArticleAppDbContext()
        {

        }

        public ArticleAppDbContext(DbContextOptions<ArticleAppDbContext> options) 
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// Articles 테이블의 Created 열은 자동으로 GetDate() 제약 조건을 부여하기
            modelBuilder.Entity<Article>().Property(m => m.Created).HasDefaultValueSql("GetDate()");
        }

        public DbSet<Article> Articles { get; set; }
    }
}
