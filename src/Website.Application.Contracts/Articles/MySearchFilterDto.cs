using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Website.Articles
{
    public class MySearchFilterDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
