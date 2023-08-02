using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Website.Articles;
using System.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;
using System.Web.Mvc;
using Microsoft.Extensions.Logging;

namespace Website.UploadFile
{
    public class UploadImage : WebsiteAppService, IUploadImage
    {
        private readonly IHostingEnvironment environment;
        public UploadImage(IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        public async Task<string> SaveProfilePictureAsync(IFormFile file, string fileName)
        {
            
            var filepath = Path.Combine(environment.ContentRootPath,@"wwwroot\Uploads", fileName);
            using var stream = new FileStream(filepath, FileMode.Create);
            await file.CopyToAsync(stream);
            return filepath;
        }

        public string getImgURl(string fileName)
        {
            var filepath2 = Path.Combine(@".\\Uploads", fileName);
            return filepath2;
        }

        
        public async Task<string> CkeditorUploadFileAsync(IFormFile file)
        {

            
            //De luu vao folder
           string fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
           var filepath = Path.Combine(environment.ContentRootPath, @"wwwroot\UploadsCkedited", fileName);
           using var stream = new FileStream(filepath, FileMode.Create);
           await file.CopyToAsync(stream);

            //de return URL
            var filepathSave = Path.Combine(@"\UploadsCkedited", fileName);
            filepathSave = filepathSave.Replace("\\", "/");



            var value = new { path = filepathSave };
            var json = JsonSerializer.Serialize(new { path = filepathSave });
            return json;
        }

        public string returnString(string input)
        {
            string result = "this is the returnstring function with input: " + input + "\n";
            return result;
        }

       
    }
}
