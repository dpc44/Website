using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Website.Files
{
    public class CSVDto : AuditedEntityDto<Guid>
    {

        public string Name { get; set; }
        public string TypeFile { get; set; }
        public string Content { get; set; }
        public string Header { get; set; }
    }
}
