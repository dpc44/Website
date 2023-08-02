using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Website.Categories;
using Website.Users;

namespace Website.Articles
{
    
    public interface IArticleAppService : IApplicationService
    {
        
        Task<PagedResultDto<ArticleDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        /*Task<PagedResultDto<ArticleDto>> GetListAsync(MySearchFilterDto input);*/
        Task CreateAsync(CreateUpdateArticleDto input);
        //For Category

        Task<ListResultDto<CategoryLookUpDto>> GetCategoriesAsync();

        //For User
        Task<ListResultDto<UserLookUpDto>> GetUsersAsync();

        //Update
        Task<ArticleDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, CreateUpdateArticleDto input);
        //Delete
        Task DeleteAsync(Guid id);

        //Search
        Task<PagedResultDto<ArticleDto>> GetListSearchAsync(string input);
        /*Task SaveProfilePictureAsync(byte[] bytes);
        Task<byte[]> GetProfilePictureAsync();*/

        Task<string> getAuthor(Guid Id);


    }
}
