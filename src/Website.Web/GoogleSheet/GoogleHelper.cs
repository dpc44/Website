using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using Volo.Abp.Application.Dtos;


namespace Website.Web.GoogleSheet
{

    public class GoogleHelper
    {
        public SheetsService Service { get; set; }
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

        public GoogleHelper()
        {
            GoogleCredential credential;

            using (var stream = new FileStream("client_secret_4.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            Service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "googleSheetPrivateKey",
            });
        }

        public List<List<string>> ReadSheet(string spreadsheetId, string sheetName)
        {
            string range = $"{sheetName}!A1:Z"; 
            SpreadsheetsResource.ValuesResource.GetRequest request = Service.Spreadsheets.Values.Get(spreadsheetId, range);
            ValueRange response = request.Execute();

            IList<IList<Object>> values = response.Values;
            List<List<string>> lstResult = new();
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {

                    List<string> lst = new List<string>();

                    // Iterate through the elements in the row and add them to lst
                    foreach (var cellValue in row)
                    {
                        lst.Add(cellValue.ToString());
                    }
                    lstResult.Add(lst);
                    
                }
            }

            return lstResult;
        }
    }
}
