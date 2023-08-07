using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Website.Files
{
    public class TableList : FullAuditedAggregateRoot<Guid>
    {
        public string Variable { get; set; }
        public string Breakdown { get; set; }
        public string BreakdownCategory { get; set; }
        public int Year { get; set; }
        public string RdValue { get; set; }
        public string Status { get; set; }
        public string Unit { get; set; }
        public string Footnotes { get; set; }
        public string RelativeSamplingError { get; set; }
        /*public string Username { get; set; }
        public string Identifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }*/
    }
}
