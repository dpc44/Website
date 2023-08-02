using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Website.Articles;
using Website.Categories;
using Website.Files;
using Website.Users;

namespace Website.Data
{
    internal class WebsiteDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IRepository<Article, Guid> _articleRepository;
        private readonly IRepository<User, Guid> _userRpository;
        private readonly IRepository<CSV, Guid> _fileRepository;


        public WebsiteDataSeedContributor(IRepository<Category, Guid> categoryRepository,
            IRepository<Article, Guid> articleRepository, IRepository<User, Guid> userRpository, IRepository<CSV, Guid> fileRepository)
        {
            _categoryRepository = categoryRepository;
            _articleRepository = articleRepository;
            _userRpository = userRpository;
            _fileRepository = fileRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _categoryRepository.CountAsync() > 0)
            {
                return;
            }

            var Comedy = new Category { Name = "Comedy" };
            var Horror = new Category { Name = "Horror" };


            await _categoryRepository.InsertManyAsync(new[] { Comedy, Horror });
            var User1 = new User
            {
                Name = "Nguyen Van A",
                Email = "Caophat1@gmail.com",
                Password = "9040255a",
                DateOfBirth = new DateTime(1996, 08, 20),
                ImageUser = "Image User 1",
                role = "User"
            };
            var User2 = new User
            {
                Name = "Nguyen Van B",
                Email = "Caophat2@gmail.com",
                Password = "9040255b",
                DateOfBirth = new DateTime(1996, 08, 20),
                ImageUser = "Image User 2",
                role = "User"
            };
            await _userRpository.InsertManyAsync(new[] { User1, User2 });


            var Article1 = new Article
            {
                Category = Comedy,
                /*User = User1,*/
                Status = ArticleStatus.Accepted,
                DatePublishment = DateTime.Now,
                DateCreation = DateTime.Now.AddDays(1),
                Name = "ContentTitle 1",
                ContentBody = "ContentBody 1",
                Teaser = "Teaser 1",
                Image = "Image 1"
            };

            var Article2 = new Article
            {
                Category = Horror,
                /*User = User2,*/
                Status = ArticleStatus.Accepted,
                DatePublishment = DateTime.Now,
                DateCreation = DateTime.Now.AddDays(3),
                Name = "ContentTitle 2",
                ContentBody = "ContentBody 2",
                Teaser = "Teaser 2",
                Image = "Image 2"
            };

            await _articleRepository.InsertManyAsync(new[] { Article1, Article2 });


            var file1 = new CSV
            {
                Name = "File Name 1",
                TypeFile = "CSV",
                Content = "Content Test 1",
                Header ="Header 1"
            };

            var file2 = new CSV
            {
                Name = "File Name 2",
                TypeFile = "CSV",
                Content = "Content Test 2",
                Header = "Header 2"
            };
            await _fileRepository.InsertManyAsync(new[] { file1, file2 });
        }
        
    }
}
