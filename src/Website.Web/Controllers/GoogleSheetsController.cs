using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Website.Web.GoogleSheet;

namespace Website.Web.Controllers
{
    public class GoogleSheetsController : Controller
    {
        private readonly GoogleHelper _googleHelper;
        public GoogleSheetsController(GoogleHelper googleHelper)
        {
            _googleHelper = googleHelper;
        }
        public IActionResult Index()
        {
            string spreadsheetId = "1eOk2eKYGYsVyyBfSmhZOgZ7MRPdyYnnMgD2OLWymblk";
            string sheetName = "Sample";

            List<List<string>> sheetData = _googleHelper.ReadSheet(spreadsheetId, sheetName);
            return View();
        }
    }
}
