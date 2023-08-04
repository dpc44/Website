using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Website.Files
{
    public class TableListDtoMap : ClassMap<TableListDto>
    {
        public TableListDtoMap()
        {
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
