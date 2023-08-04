
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using CsvHelper;
using CsvHelper.Configuration;
using Website.Files;
using AutoMapper.Internal.Mappers;

namespace Website.Web.Controllers
{
    public class CSVController : Controller
    {
        private readonly ICSVAppService _CSVAppService;
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<TableList, Guid> _tableList;
        public CSVController (ICSVAppService csvAppService, IWebHostEnvironment env, IRepository<TableList, Guid> tableList)
        {
            _CSVAppService = csvAppService;
            _env = env;
            _tableList = tableList;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CSVDto> csvData = await _CSVAppService.GetListAsync();
            return View(csvData);
        }
        [HttpPost]
        public async Task<IActionResult> UploadCSVFile(IFormFile csvFile)
        {
            if (csvFile != null && csvFile.Length > 0)
            {
                //save the file to folder
                var rootPath = _env.WebRootPath;
                var csvFolderPath = Path.Combine(rootPath, "CSVfiles");
                Directory.CreateDirectory(csvFolderPath);
                var uniqueFileName = $"{Guid.NewGuid()}_{csvFile.FileName}";
                var filePath = Path.Combine(csvFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await csvFile.CopyToAsync(stream);
                }


                using (var streamReader = new StreamReader(filePath))
                {
                    using(var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                        csvReader.Context.RegisterClassMap<TableListMap>();
                        var records = csvReader.GetRecords<TableList>();
                        foreach (var record in records)
                        {
                            await _tableList.InsertAsync(record);
                        }

                    }
                }


                return Json(new { success = true, name = csvFile.FileName });
            }

            return Json(new { success = false });
        }




        public async Task<IActionResult> ViewTwo(int page = 1, int pageSize = 30)
        {
            var result = await _CSVAppService.GetItemPerPage(pageSize, page);
            ViewBag.TotalPages = result.Item2;
            ViewBag.CurrentPage = page;
            return View(result.Item1);
        }



        [HttpGet("CSV/ViewById/{id}")]
        public async Task<IActionResult> View(Guid id, int page =1)
        {
            const int ItemsPerPage = 20;

            var csv = await _CSVAppService.GetCSVById(id);
            

            var records = _CSVAppService.CVSList(csv);
            var errRecords = _CSVAppService.ErrorCVSList(csv);
            //pagination for Records
            int totalPages = (int)Math.Ceiling((double)records.Count / ItemsPerPage);
            var pagedRecords = records.Skip((page - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
            ViewData["ContentList"] = pagedRecords;
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;


            ViewData["ErrorContentList"] = errRecords;
            TempData["ErrorContentList2"] = "ABC";



            return View(csv);
        }
      
        public async Task<IActionResult> SecondView(Guid id, int page = 1)
        {
            const int ItemsPerPage = 20;
            


            var csv = await _CSVAppService.GetCSVById(id);

            var errRecords = _CSVAppService.ErrorCVSList(csv);
            int totalPages = (int)Math.Ceiling((double)errRecords.Count / ItemsPerPage);
            var pagedRecords = errRecords.Skip((page - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
            ViewData["ErrorContentList"] = pagedRecords;
            ViewData["TotalPages"] = totalPages;
            ViewData["CurrentPage"] = page;
            return View(csv);

        }



        [HttpPost]
        public IActionResult Open(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Process the file data as needed
                var csvDto = new CSVDto
                {
                    Name = file.FileName,
                    TypeFile = file.ContentType,
                    Content = "Content Content",
                    Header = "Header Header"
                };

                var data = ProcessFileData(file);
                TempData["fileCSV"] = JsonSerializer.Serialize(data);
                return Json(new { success = true, fileName = file.FileName });
            }

            // Handle the case when no file is uploaded or the file is empty.
            return Json(new { success = false });
        }
        [HttpGet("CSV/ViewByFileId/{fileId}")]
        public IActionResult View(string fileId , int page = 1)
        {
            if (!string.IsNullOrEmpty(fileId))
            {
                // Retrieve the file data using the fileId from TempData or other storage options

                var data = TempData["fileCSV"].ToString();
                //Variables
                CSVDto csv = JsonSerializer.Deserialize<CSVDto>(data);
                var records = _CSVAppService.CVSList(csv);
                var errRecords = _CSVAppService.ErrorCVSList(csv);
                const int ItemsPerPage = 20;
                //pagination for Records
                int totalPages = (int)Math.Ceiling((double)records.Count / ItemsPerPage);
                var pagedRecords = records.Skip((page - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
                ViewData["ContentList"] = pagedRecords;
                ViewData["TotalPages"] = totalPages;
                ViewData["CurrentPage"] = page;


                ViewData["ErrorContentList"] = errRecords;
                return View(csv);
            }

            return NoContent();
        }



        private CSVDto ProcessFileData(IFormFile file)
        {
            // Read the file content and convert it into a CSVDto
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var columnHeaders = reader.ReadLine();
                var content = reader.ReadToEnd();

                return new CSVDto
                {
                    Name = file.FileName,
                    TypeFile = file.ContentType,
                    Content = content,
                    Header = columnHeaders
                };
            }
        }



    }




}
