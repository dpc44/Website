using CsvHelper.Configuration;
using CsvHelper;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Volo.Abp.Application.Dtos;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

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
            string range = $"{sheetName}!A2:Z"; 
            SpreadsheetsResource.ValuesResource.GetRequest request = Service.Spreadsheets.Values.Get(spreadsheetId, range);
            ValueRange response = request.Execute();

            IList<IList<Object>> values = response.Values;
            List<List<string>> lstResult = new();
            int IndexLine = 2;
            if (values != null && values.Count > 0)
            {
                var headerRow = values[0];
                
                foreach (var row in values)
                {
                    List<string> lst = new List<string>();
                    //If la header thi 
                    if (IndexLine == 2)
                    {
                        foreach (var cellValue in row)
                        {
                            lst.Add(cellValue.ToString());
                        }
                        IndexLine++;
                        
                    }
                    else
                    {
                        //if not the header we will check if length of row == headerrowcount because of checking status read
                        if(headerRow.Count != row.Count)
                        {
                            foreach (var cellValue in row)
                            {
                                lst.Add(cellValue.ToString());
                            }
                            lst.Add("Read");
                            lstResult.Add(lst);

                        }
                        else
                        {
                            IndexLine++;
                        }
                    }
                    

                    
                        
                        
                    
                    
                    
                    
                }
            }
            
            UpdateGoogleSheet(spreadsheetId, sheetName, lstResult, IndexLine);
            return lstResult;
        }

        public void UpdateGoogleSheet(string spreadsheetId, string sheetName, List<List<string>> data, int IndexLine)
        {
            var range = $"{sheetName}!A{IndexLine}:Z";


            var convertedData = data.Select(row => row.Select(cell => (object)cell).ToList()).ToList();



            var valueRange = new ValueRange()
            {
                Values = new List<IList<object>>() // Initialize the Values property
            }; 
            foreach (var row in convertedData)
            {
                valueRange.Values.Add(row);
            }
            SpreadsheetsResource.ValuesResource.UpdateRequest request = Service.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
            request.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

            var response = request.Execute();
        }
    }
}
