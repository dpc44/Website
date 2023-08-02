using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Website.Articles;
using System.Linq.Dynamic.Core;
namespace Website.Categories
{
    public class CategoryAppService : WebsiteAppService,ICategoryAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        public CategoryAppService (IRepository<Category, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        //Show List
        public async Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _categoryRepository.WithDetailsAsync();
            queryable = queryable
                        .Skip(input.SkipCount)
                        .Take(input.MaxResultCount).OrderBy(input.Sorting ?? nameof(Category.Name));

            var products = await AsyncExecuter.ToListAsync(queryable);
            var count = await _categoryRepository.GetCountAsync();
            return new PagedResultDto<CategoryDto>(count, ObjectMapper.Map<List<Category>, List<CategoryDto>>(products));
        }

        //Create
        public async Task CreateAsync(CreateUpdateCategoryDto input)
        {
            await _categoryRepository.InsertAsync(ObjectMapper.Map<CreateUpdateCategoryDto, Category>(input));
        }

        //Delete
        public async Task DeleteAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
        }
        //Update
        public async Task<CategoryDto> GetAsync(Guid id)
        {

            return ObjectMapper.Map<Category, CategoryDto>(await _categoryRepository.GetAsync(id)); ;
        }

        public async Task UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            var product = await _categoryRepository.GetAsync(id);
            ObjectMapper.Map(input, product);
        }
    }
}
