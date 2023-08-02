using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Website.Articles;

namespace Website.Web.Pages.Articles
{
    public class EditArticleModalModel : WebsitePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditArticleViewModel Article { get; set; }
        public SelectListItem[] Categories { get; set; }
        public SelectListItem[] Users { get; set; }
        public IFormFile? imageFile { get; set; }

        private readonly IArticleAppService _articleAppService;
        private IHostingEnvironment Environment;
        public EditArticleModalModel (IArticleAppService articleAppService, IHostingEnvironment _environment)
        {
            _articleAppService = articleAppService;
            Environment = _environment;
        }

        public async Task OnGetAsync()
        {
            var ArticleDto = await _articleAppService.GetAsync(Id);
            Article = ObjectMapper.Map < ArticleDto, CreateEditArticleViewModel>(ArticleDto);

            var CategoryLookUp = await _articleAppService.GetCategoriesAsync();
            Categories = CategoryLookUp.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToArray();

            var UserLookUp = await _articleAppService.GetUsersAsync();
            Users = UserLookUp.Items.Select(x => new SelectListItem(x.UserName, x.Id.ToString())).ToArray();

           
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (imageFile == null)
            {
                var ArticleDto = await _articleAppService.GetAsync(Id);
                Article.Image = ArticleDto.Image;
            }
            else
            {
                //for saving folder Uploads
                string uploadPath = Environment.WebRootPath + "\\Uploads";
                string fileName = Path.GetRandomFileName() + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                //for saving on database (for reading)
                string uploadPath2 = ".\\Uploads";

                string filePath2 = Path.Combine(uploadPath2, fileName);

                Article.Image = filePath2;
                Article.Image = Article.Image.Replace("\\", "/");
            }
            
            await _articleAppService.UpdateAsync(Id, ObjectMapper.Map<CreateEditArticleViewModel, CreateUpdateArticleDto>(Article));
            return NoContent();
        }
    
    }
}
