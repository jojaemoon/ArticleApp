using ArticleApp.Models;
using Dul.Domain.Common;
using DulPager;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleApp.Pages.Articles
{
    public partial class Manage
    {
        [Inject]
        public IArticleRepository ArticleRepository { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private void btnCreate_Click()
        {
            editFormTitle = "글쓰기";
            _article = new Article();   // 모델을 클리어
        }

        private string editFormTitle = "";

        /// <summary>
        /// 저장 또는 수정 후 데이터 다시 읽어오기
        /// </summary>
        private async void SaveOrUpdated()
        {
            // 페이징
            PagingResult<Article> pagingData = await ArticleRepository.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = pagingData.TotalRecords;        // 총 레코드 수
            articles = pagingData.Records.ToList();             // 페이징 처리된 레코드

            StateHasChanged();      // 현재 콤포넌트 재로드  리플레쉬
        }

        /// <summary>
        /// 삭제 -> 모달 폼 닫기 -> 선택했던 데이터 초기화 -> 전체 데이터 다시 읽어오기 -> Refresh
        /// </summary>
        private async void btnDelete_Click()
        {
            await ArticleRepository.DeleteArticleAsync(_article.Id);   //삭제
            await JSRuntime.InvokeAsync<object>("closeModal", "articleDeleteDialog");  // _Host.cshtml
            _article = new Article();   // 선택 항목 초기화

            // 페이징
            PagingResult<Article> pagingData = await ArticleRepository.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = pagingData.TotalRecords;        // 총 레코드 수
            articles = pagingData.Records.ToList();             // 페이징 처리된 레코드

            StateHasChanged();      // 현재 콤포넌트 재로드  리플레쉬
        }

        private void EditBy(Article article)
        {
            editFormTitle = "수정하기";
            _article = article;
        }

        private void DeleteBy(Article article)
        {
            _article = article;
        }

        /*  공지 사항 모달 */
        private Article _article = new Article();       // 선택한 항목 관리

        private bool isShow = false;   //  notice Modal

        private void PinnedBy(Article article)
        {
            _article = article;
            isShow = true;
        }

        private void btnClose_Click()
        {
            isShow = false;  // 창 닫기
            _article = new Article();
        }

        private async void btnTogglePinned_Click()
        {
            _article.IsPinned = !_article.IsPinned;  // Toggle
            await ArticleRepository.EditArticleAsync(_article);

            PagingResult<Article> pagingData = await ArticleRepository.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = pagingData.TotalRecords;        // 총 레코드 수
            articles = pagingData.Records.ToList();             // 페이징 처리된 레코드

            isShow = false; // Modal Close

            StateHasChanged();  // Refresh
        }
        /*  공지 사항 모달 */

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
            pager.RecordCount = pagingData.TotalRecords;        // 총 레코드 수
            articles = pagingData.Records.ToList();             // 페이징 처리된 레코드
        }

        // Pager 버튼 클릭 콜백 메서드
        private async void PageIndexChanged(int pageIndex)
        {
            pager.PageIndex = pageIndex;
            pager.PageNumber = pageIndex + 1;

            // 페이징
            var pagingData = await ArticleRepository.GetAllAsync(pager.PageIndex, pager.PageSize);
            pager.RecordCount = pagingData.TotalRecords;
            articles = pagingData.Records.ToList();

            StateHasChanged();      // 현재 콤포넌트 재로드  리플레쉬
        }
    }
}
