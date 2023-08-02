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
    }
}
