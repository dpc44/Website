using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

using Website.Articles;

namespace Website.Web.Pages.Articles
{
    public class ViewArticleModel : WebsitePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditArticleViewModel Article { get; set; }

        private readonly IArticleAppService _articleAppService;

        public string NameAuthor {get; set;}

        public ViewArticleModel(IArticleAppService articleAppService)
        {
            _articleAppService = articleAppService;
            
        }

        public async Task OnGetAsync()
        {
            var ArticleDto = await _articleAppService.GetAsync(Id);
            Article = ObjectMapper.Map<ArticleDto, CreateEditArticleViewModel>(ArticleDto);

            NameAuthor = await _articleAppService.getAuthor(Article.IdentityUserId);


        }
    }
}
