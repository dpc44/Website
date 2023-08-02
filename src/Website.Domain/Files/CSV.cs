using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Website.Files
{
    public class CSV : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string TypeFile { get; set; }
        public string Content { get; set; }
        public string Header { get; set; }
    }
}
