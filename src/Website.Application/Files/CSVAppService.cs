using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IRepository<TableList, Guid> _tableList;

        public CSVAppService (IRepository<CSV, Guid> fileAppService, IRepository<TableList, Guid> tableList)
        {
            _fileAppService = fileAppService;
            _tableList = tableList;
        }
        public async Task<List<CSVDto>> GetListAsync()
        {
            var files = await _fileAppService.WithDetailsAsync();
            var products = await  AsyncExecuter.ToListAsync(files);
            return ObjectMapper.Map<List<CSV>, List<CSVDto>>(products);
        }

        public async Task addCSVData(CSVDto csvData)
        {
          await  _fileAppService.InsertAsync(ObjectMapper.Map<CSVDto, CSV>(csvData));
            
        }

        public async Task addCSVData2 (TableListDto csvData)
        {
           await _tableList.InsertAsync(ObjectMapper.Map<TableListDto, TableList>(csvData));
        }
        public async Task<CSVDto> GetCSVById(Guid id)
        {
           
                var csv = await _fileAppService.GetAsync(id);
                return ObjectMapper.Map<CSV, CSVDto>(csv);
            
        }
        public List<Dictionary<string, string>> CVSList(CSVDto csv)
        {
            var columnHeaders = csv.Header.Split(';', ',');

            using (var reader = new StringReader(csv.Content))
            {
                var records = new List<Dictionary<string, string>>();
                string line;

                while ((line = reader.ReadLine()) != null && line != "")
                {
                    var values = line.Split(';', ',');
                    var record = new Dictionary<string, string>();
                    if (values.Length == columnHeaders.Length)
                    {
                        for (var i = 0; i < columnHeaders.Length; i++)
                        {
                            var columnName = columnHeaders[i];
                            var value = values[i];
                            record[columnName] = value;
                        }

                        records.Add(record);
                    }

                }
                 return records;

            }
        }

        public List<Dictionary<string, string>> ErrorCVSList(CSVDto csv)
        {
            var columnHeaders = csv.Header.Split(';', ',');

            using (var reader = new StringReader(csv.Content))
            {

                var errRecords = new List<Dictionary<string, string>>();
                string line;

                while ((line = reader.ReadLine()) != null && line != "")
                {
                    var values = line.Split(';', ',');

                    var errRecord = new Dictionary<string, string>();
                    if (values.Length != columnHeaders.Length)
                    {

                        for (var i = 0; i < columnHeaders.Length; i++)
                        {
                            var columnName = columnHeaders[i];
                            var value = values[i];
                            errRecord[columnName] = value;
                        }

                        errRecords.Add(errRecord);
                    }

                }


                return errRecords;




            }
        }

        public async Task<Tuple<List<TableListDto>, int>> GetItemPerPage(int ItemsPerPage, int CurrentPage)
        {
            var records = await _tableList.GetListAsync();
            int totalPages = (int)Math.Ceiling((double)records.Count / ItemsPerPage);
            var pagedRecords = records.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();

            return new Tuple<List<TableListDto>, int>(ObjectMapper.Map<List<TableList>, List<TableListDto>>(pagedRecords), totalPages);
        }
    }
}
