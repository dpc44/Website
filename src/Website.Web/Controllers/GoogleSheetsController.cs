using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Website.Files;
using Website.Web.GoogleSheet;

namespace Website.Web.Controllers
{
    public class GoogleSheetsController : Controller
    {
        private readonly GoogleHelper _googleHelper;
        private readonly IRepository<TableList, Guid> _tableList;
        public GoogleSheetsController(GoogleHelper googleHelper, IRepository<TableList, Guid> tableList)
        {
            _googleHelper = googleHelper;
            _tableList = tableList;
        }
        [HttpPost]
        public async Task<IActionResult> SaveGoogleSheet()
        {
            string spreadsheetId = "1eOk2eKYGYsVyyBfSmhZOgZ7MRPdyYnnMgD2OLWymblk";
            string sheetName = "100";

            var sheetData = _googleHelper.ReadSheet(spreadsheetId, sheetName);
            
            foreach(var row in sheetData)
            {
                TableList tableList = new TableList
                {
                    Variable = row[0],
                    Breakdown = row[1],
                    BreakdownCategory = row[2],
                    Year = ConvertToInt(row[3]),
                    RdValue = row[4],
                    Status = row[5],
                    Unit = row[6],
                    Footnotes = row[7],
                    RelativeSamplingError = row[8]
                };
                await _tableList.InsertAsync(tableList);

            }

            return RedirectToAction("Index", "CSV");
        }

        private int ConvertToInt(string input)
        {
            int result;
            if (int.TryParse(input, out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
    }
}
