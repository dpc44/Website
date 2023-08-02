using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Website.Files
{
    public interface ICSVAppService : IApplicationService
    {
        Task<List<CSVDto>> GetListAsync();
        Task addCSVData (CSVDto csvData);

        Task<CSVDto> GetCSVById(Guid id);
    }
}
