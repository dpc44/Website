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
        Task addCSVData2 (TableListDto csvData);
        Task<CSVDto> GetCSVById(Guid id);
        List<Dictionary<string, string>> CVSList(CSVDto csv);
        List<Dictionary<string, string>> ErrorCVSList(CSVDto csv);
    }
}
