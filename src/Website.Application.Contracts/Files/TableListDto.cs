using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Website.Files
{
    public class TableListDto : AuditedEntityDto<Guid>
    {
        public string Variable { get; set; }
        public string Breakdown { get; set; }
        public string BreakdownCategory { get; set; }
        public string Year { get; set; }
        public string RdValue { get; set; }
        public string Status { get; set; }
        public string Unit { get; set; }
        public string Footnotes { get; set; }
        public string RelativeSamplingError { get; set; }
        /* public string Username { get; set; }
         public string Identifier { get; set; }
         public string FirstName { get; set; }
         public string LastName { get; set; }*/
    }
}
