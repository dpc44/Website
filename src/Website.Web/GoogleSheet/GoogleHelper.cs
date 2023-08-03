using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using Volo.Abp.Application.Dtos;

namespace Website.Web.GoogleSheet
{
    public class GoogleHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SheetsService Service { get; set; }
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

        public GoogleHelper(IWebHostEnvironment webHostEnvironment) {
            _webHostEnvironment = webHostEnvironment;
            string JsonPath = Path.Combine(_webHostEnvironment.WebRootPath, "client_secret.json");
            GoogleCredential credential;
            using(var stream = new FileStream(JsonPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "TestCSVGoogleSheet",
            });

            
        }
        public List<List<string>> ReadSheet(string spreadsheetId, string sheetName)
        {
            string range = $"{sheetName}!A1:Z";
            var request = Service.Spreadsheets.Values.Get(spreadsheetId, range);
            var response = request.Execute();
            var values = response.Values;
            List<List<string>> lstResult = new();
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    List<string> lst = new() { (string)row[0], (string)row[1], (string)row[2], (string)row[3] };
                    lstResult.Add(lst);
                }
            }
            return lstResult;
        }
    }
}
