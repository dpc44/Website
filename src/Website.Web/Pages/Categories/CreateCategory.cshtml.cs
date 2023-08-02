using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Website.Articles;
using Website.Categories;
using Website.Web.Pages.Articles;

namespace Website.Web.Pages.Categories
{
    public class CreateCategoryModel : WebsitePageModel
    {
        private readonly ICategoryAppService _categoryAppService;
        [BindProperty]
        public CreateUpdateCategoryDto Categories { get; set; }
        public  CreateCategoryModel(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            await _categoryAppService.CreateAsync(Categories);
            return await Task.FromResult(Redirect("~/Articles"));
        }
    }
}
