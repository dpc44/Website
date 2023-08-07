using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace Website.Files
{
   
    public class TableListMap : ClassMap<TableList>
    {
        public TableListMap() {
            Map(m => m.Variable).Name("Variable");
            Map(m => m.Breakdown).Name("Breakdown");
            Map(m => m.BreakdownCategory).Name("Breakdown_category");
            Map(m => m.Year).Name("Year");
            Map(m => m.RdValue).Name("RD_Value");
            
            Map(m => m.Status).Name("Status");
            Map(m => m.Unit).Name("Unit");
            Map(m => m.Footnotes).Name("Footnotes");
            Map(m => m.RelativeSamplingError).Name("Relative_Sampling_Error");

        }
    }
}
