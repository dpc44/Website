using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Volo.Abp.Application.Services;

namespace Website.UploadFile
{
    public interface IUploadImage : IApplicationService
    {
        Task<string> SaveProfilePictureAsync(IFormFile file, string fileName);
        string getImgURl (string fileName);
        Task<string> CkeditorUploadFileAsync(IFormFile file);

        string returnString (string input);
    }
}
