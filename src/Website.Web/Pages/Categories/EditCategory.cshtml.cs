using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Website.Articles;
using Website.Categories;
using Website.Web.Pages.Articles;

namespace Website.Web.Pages.Categories
{
    public class EditCategoryModel : WebsitePageModel
    {
        private readonly ICategoryAppService _categoryAppService;

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }


        [BindProperty]
        public CreateUpdateCategoryDto Categories { get; set; }
        public EditCategoryModel(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public async Task OnGetAsync()
        {

            var ArticleDto = await _categoryAppService.GetAsync(Id);
            Categories = ObjectMapper.Map<CategoryDto, CreateUpdateCategoryDto>(ArticleDto);

            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _categoryAppService.UpdateAsync(Id, Categories);
            return await Task.FromResult(Redirect("~/Articles"));
        }
    }
}
