using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Website.Categories
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task CreateAsync(CreateUpdateCategoryDto input);
        Task DeleteAsync(Guid id);

        Task<CategoryDto> GetAsync(Guid id);

        Task UpdateAsync(Guid id, CreateUpdateCategoryDto input);
    }
}
