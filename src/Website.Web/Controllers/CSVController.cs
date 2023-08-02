using CsvHelper;
using CsvHelper.Configuration;
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
using System.Threading.Tasks;

using Website.Files;

namespace Website.Web.Controllers
{
    public class CSVController : Controller
    {
        private readonly ICSVAppService _CSVAppService;
        public CSVController (ICSVAppService csvAppService)
        {
            _CSVAppService = csvAppService;
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
                using (var reader = new StreamReader(csvFile.OpenReadStream()))
                {
                    // Read the first line to get the column headers
                    var columnHeaders = reader.ReadLine();
                    if (columnHeaders != null)
                    {
                        var content = reader.ReadToEnd();

                        var csv = new CSVDto
                        {
                            Name = csvFile.FileName,
                            TypeFile = csvFile.ContentType,
                            Content = content,
                            Header = columnHeaders
                        };

                       await _CSVAppService.addCSVData(csv);
                    }
                        
                }
            }

            return RedirectToAction("Index");
        }









        public async Task<IActionResult> View(Guid id, int page =1)
        {
            const int ItemsPerPage = 20;

            var csv = await _CSVAppService.GetCSVById(id);
            var columnHeaders = csv.Header.Split(';',',');

            using (var reader = new StringReader(csv.Content))
            {
                var records = new List<Dictionary<string, string>>();
                var errRecords = new List<Dictionary<string, string>>();
                string line;
                
                while ((line = reader.ReadLine()) != null && line != "")
                {
                    var values = line.Split(';', ',');
                    var record = new Dictionary<string, string>();
                    var errRecord = new Dictionary<string, string>();
                    if(values.Length == columnHeaders.Length)
                    {
                        for (var i = 0; i < columnHeaders.Length; i++)
                        {
                            var columnName = columnHeaders[i];
                            var value = values[i];
                            record[columnName] = value;
                        }

                        records.Add(record);
                    }
                    else
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

                //pagination for Records
                int totalPages = (int)Math.Ceiling((double)records.Count / ItemsPerPage);
                var pagedRecords = records.Skip((page - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
                ViewData["ContentList"] = pagedRecords;
                ViewData["TotalPages"] = totalPages;
                ViewData["CurrentPage"] = page;


                ViewData["ErrorContentList"] = errRecords;
                TempData["ErrorContentList2"] = "ABC";
            }




            


            return View(csv);
        }
      
        public async Task<IActionResult> SecondView(Guid id, int page = 1)
        {
            const int ItemsPerPage = 20;
            


            var csv = await _CSVAppService.GetCSVById(id);
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

                //pagination for Records


                int totalPages = (int)Math.Ceiling((double)errRecords.Count / ItemsPerPage);
                var pagedRecords = errRecords.Skip((page - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
                ViewData["ErrorContentList"] = pagedRecords;
                ViewData["TotalPages"] = totalPages;
                ViewData["CurrentPage"] = page;



            }


            return View(csv);

        }



        [HttpPost]
        public IActionResult Open(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Process the file data as needed
                TempData["FileName"] = file.FileName;
                TempData["CSVFile"] = file;
                return Json(new { success = true, fileName = file.FileName });
            }

            // Handle the case when no file is uploaded or the file is empty.
            return Json(new { success = false });
        }

        public IActionResult Open(string fileId)
        {
            if (!string.IsNullOrEmpty(fileId))
            {
                // Retrieve the file data using the fileId from TempData or other storage options
                string fileName = TempData["FileName"] as string;
                var file = TempData["CSVFile"] as IFormFile;
                // Pass the file name to the view
                ViewData["FileName"] = fileName;

                return View();
            }

            return NoContent();
        }






    }




}
