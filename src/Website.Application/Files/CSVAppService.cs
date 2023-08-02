using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Website.Categories;


namespace Website.Files
{
    public class CSVAppService : WebsiteAppService , ICSVAppService
    {
        private readonly IRepository<CSV, Guid> _fileAppService;
        public CSVAppService (IRepository<CSV, Guid> fileAppService)
        {
            _fileAppService = fileAppService;
        }
        public async Task<List<CSVDto>> GetListAsync()
        {
            var files = await _fileAppService.WithDetailsAsync();
            var products = await  AsyncExecuter.ToListAsync(files);
            return ObjectMapper.Map<List<CSV>, List<CSVDto>>(products);
        }

        public async Task addCSVData(CSVDto csvData)
        {
            _fileAppService.InsertAsync(ObjectMapper.Map<CSVDto, CSV>(csvData));
            
        }


        public async Task<CSVDto> GetCSVById(Guid id)
        {
           
                var csv = await _fileAppService.GetAsync(id);
                return ObjectMapper.Map<CSV, CSVDto>(csv);
            
        }
    }
}
