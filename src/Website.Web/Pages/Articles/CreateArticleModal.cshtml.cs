using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;
using Website.Articles;
using Website.UploadFile;

namespace Website.Web.Pages.Articles
{
    public class CreateArticleModalModel : WebsitePageModel
    {
        [BindProperty]
        public CreateEditArticleViewModel Article {get; set;}
        
        
        [DataType(DataType.Upload)]
        [FileExtensions(ErrorMessage = "Must choose .csv file.", Extensions = "image/jpg,image/png,image/jpeg")]
        [DisplayName("Cover Image")]
        public IFormFile imageFile { get; set; }


        public SelectListItem[] Categories { get; set; }
        public SelectListItem[] Users { get; set; }

        
        
        private readonly IArticleAppService _articleAppService;
        private readonly IUploadImage _uploadImage;
        private IHostingEnvironment Environment;

        public CreateArticleModalModel(IArticleAppService articleAppService, IHostingEnvironment _environment, IUploadImage uploadImage)
        {
            _articleAppService = articleAppService;
            Environment = _environment;
            _uploadImage = uploadImage;
        }


        public async Task OnGetAsync()
        {
            Article = new CreateEditArticleViewModel()
            {
                Status = ArticleStatus.NotAvailable,
                DateCreation = Clock.Now,
                DatePublishment = Clock.Now.AddDays(7),

            };
            var CategoryLookUp = await _articleAppService.GetCategoriesAsync();
            Categories = CategoryLookUp.Items.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToArray();

            var UserLookUp =  await _articleAppService.GetUsersAsync();
            Users = UserLookUp.Items.Select(x => new SelectListItem(x.UserName, x.Id.ToString())).ToArray();

            
        }
        
        public async Task<IActionResult> OnPostAsync(IFormCollection formValues)
        {
            
            if (imageFile != null && imageFile.Length > 0)
            {
                

                //Test api
                string fileName = Path.GetRandomFileName() + Path.GetExtension(imageFile.FileName);
                //To Save the image with full URl
                _uploadImage.SaveProfilePictureAsync(imageFile, fileName);
                //Get the short URL for upload to Database
                Article.Image = _uploadImage.getImgURl(fileName);
                Article.Image = Article.Image.Replace("\\", "/");


                await _articleAppService.CreateAsync(ObjectMapper.Map<CreateEditArticleViewModel, CreateUpdateArticleDto>(Article));
                    return await Task.FromResult(Redirect("~/Articles"));
                
                
            }
            return NoContent();
               
        }

        
    }


}
