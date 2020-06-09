using ArticleApp.Models;
using Dul.Domain.Common;
using DulPager;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleApp.Pages.Articles
{
    public partial class Index
    {
        [Inject]
        public IArticleRepository ArticleRepository { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        // 페이저 기본값 설정
        private DulPagerBase pager = new DulPagerBase()
        {
            PageNumber = 1,
            PageIndex = 0,
            PageSize = 5,
            PagerButtonCount = 10
        };

        private List<Article> articles;

        protected override async Task OnInitializedAsync()
        {
            // articles = await ArticleRepository.GetArticlesAsync();
            PagingResult<Article> pagingData = await ArticleRepository.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = pagingData.TotalRecords;
            articles = pagingData.Records.ToList();
        }

        // 제목 td 클릭시 상세보기페이지로 이동
        private void btnTitle_Click(int id)
        {
            NavigationManager.NavigateTo($"/Articles/Details/{id}");
        }

        // Pager 버튼 클릭 콜백 메서드
        private async void PageIndexChanged(int pageIndex)
        {
            pager.PageIndex = pageIndex;
            pager.PageNumber = pageIndex + 1;

            var pagingData = await ArticleRepository.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = pagingData.TotalRecords;
            articles = pagingData.Records.ToList();

            StateHasChanged();      // 현재 콤포넌트 재로드  리플레쉬
        }
    }
}
