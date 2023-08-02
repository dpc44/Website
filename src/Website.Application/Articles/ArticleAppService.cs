    using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;

//orderby
using System.Linq.Dynamic.Core;
using Website.Categories;
using Website.Users;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;
using Volo.Abp.BlobStoring;
using Website.Documents;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Website.Articles
{
    
    public class ArticleAppService : WebsiteAppService, IArticleAppService
    {
        
        private readonly IRepository<Article, Guid> _articleRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IRepository<IdentityUser, Guid> _UserIndentity;
       
        public ArticleAppService(
            
            IRepository<Article, Guid> articleRepository, 
            IRepository<Category, Guid> categoryRepository,
            IRepository<User, Guid> userRepository,
            IAuthorizationService authorizationService,
            IRepository<IdentityUser, Guid> userIndentity
            
            )
        {
            _articleRepository = articleRepository;

            _categoryRepository = categoryRepository;

            _userRepository = userRepository;
            _authorizationService = authorizationService;
            _UserIndentity = userIndentity;
            
        }


        [Authorize("Website.ArticleList")]
        public async Task<PagedResultDto<ArticleDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            
                var queryable = await _articleRepository.WithDetailsAsync(x => x.Category, y => y.IdentityUser);









                //dsfadfdas
                queryable = queryable
                        .Skip(input.SkipCount)
                        .Take(input.MaxResultCount).OrderBy(input.Sorting ?? nameof(Article.Name));



                var products = await AsyncExecuter.ToListAsync(queryable);
                var count = await _articleRepository.GetCountAsync();
                return new PagedResultDto<ArticleDto>(count, ObjectMapper.Map<List<Article>, List<ArticleDto>>(products));
            
            
            

        }

        /*public async Task<PagedResultDto<ArticleDto>> GetListAsync(MySearchFilterDto input)
        {
            var queryable = await _articleRepository.WithDetailsAsync(x => x.Category, y => y.IdentityUser);
            var query = queryable.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), book => book.Name.ToLower()
                .Contains(input.Filter.ToLower()))
                .OrderBy(input.Sorting ?? nameof(Article.Name))
                .PageBy(input);


            var products = await AsyncExecuter.ToListAsync(queryable);
            var count = await _articleRepository.GetCountAsync();

            return new PagedResultDto<ArticleDto>(count, ObjectMapper.Map<List<Article>, List<ArticleDto>>(products));
        }*/





        //Create
        //[Authorize("Website.ArticleCreate")]
        public async Task CreateAsync(CreateUpdateArticleDto input)
        {
            //check authorize
            //CheckAsync : ABP tu handle the exception error (user khong co quyen su dung tinh nang)
            //IsGrantedAsync : neu muon tu handle the exception error

            if (await _authorizationService.IsGrantedAsync("Website.ArticleCreate"))
            {
                var queryable2 = await _articleRepository.GetQueryableAsync();

                var query2 = from Category in queryable2 select Category.Name;

                var nameQuery = query2.ToList();
                if (nameQuery.Contains(input.Name))
                {
                    throw new UserFriendlyException("Name have existed");
                }
                else
                {
                    await _articleRepository.InsertAsync(ObjectMapper.Map<CreateUpdateArticleDto, Article>(input));
                }
                
            }
            else
            {
                throw new UserFriendlyException("You cannot use this method");
            }
            
        }

        //For Category
        public async Task<ListResultDto<CategoryLookUpDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetListAsync();
            return new ListResultDto<CategoryLookUpDto>(ObjectMapper.Map<List<Category>, List<CategoryLookUpDto>>(categories));

        }

        //For User
        public async Task<ListResultDto<UserLookUpDto>> GetUsersAsync()
        {
            var identityUser = await _UserIndentity.GetListAsync();
            var query = from User in identityUser where User.Name != "admin" select User;
            var TableIdentityUser = query.ToList();
            return new ListResultDto<UserLookUpDto>(ObjectMapper.Map<List<IdentityUser>, List<UserLookUpDto>>(TableIdentityUser));

            /*var users = await _userRepository.GetListAsync();
            var query = from User in users select (i => new { i.Id, i.Name });
            var nameQuery = query.ToList();
            return new ListResultDto<UserLookUpDto>(ObjectMapper.Map<List<User>, List<UserLookUpDto>>(users));*/
        }

        //Update
       public async Task<ArticleDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Article, ArticleDto>(await _articleRepository.GetAsync(id));
        }
        public async Task UpdateAsync(Guid id, CreateUpdateArticleDto input)
        {
            var product = await _articleRepository.GetAsync(id);
            ObjectMapper.Map(input, product);
        }

        //Delete
        //[Authorize("Website.ArticleDelete")]
        public async Task DeleteAsync(Guid id)
        {
            if (await _authorizationService.IsGrantedAsync("Website.ArticleDelete"))
            {
                await _articleRepository.DeleteAsync(id);
            }
            else
            {
                throw new UserFriendlyException("You cannot use this method");
            }
                
        }

        public async Task<PagedResultDto<ArticleDto>> GetListSearchAsync(string input)
        {
            var queryable2 = await _articleRepository.WithDetailsAsync(x => x.Category, y => y.IdentityUser);
            var query2 = from Category in queryable2 where Category.Name.Contains(input) select Category;
            var nameQuery = await AsyncExecuter.ToListAsync(query2);
            var count = query2.Count();
            return new PagedResultDto<ArticleDto>(count, ObjectMapper.Map<List<Article>, List<ArticleDto>>(nameQuery));
        }

        /*public async Task SaveProfilePictureAsync(IFormFile file)*/
        /*

         public async Task<byte[]> GetProfilePictureAsync()
         {
             var blobName = "TestImageCover";
             return await _blobContainer.GetAllBytesOrNullAsync(blobName);
         }*/


        public async Task<string> getAuthor (Guid Id)
        {
            var queryable = await _UserIndentity.GetListAsync();
            var query = from User in queryable where User.Id == Id select User.Name;
            var stringquery = "";
            foreach(var item in query)
            {
               stringquery = item.ToString();
            }
            return stringquery;
        }
    }
}
