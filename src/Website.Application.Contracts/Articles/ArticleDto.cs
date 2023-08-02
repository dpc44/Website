using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Website.Articles
{
    public class ArticleDto: AuditedEntityDto<Guid>
    {

        public string Name { get; set; }
        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }

        public string IdentityUserUsername { get; set; }
        public Guid IdentityUserId { get; set; }

        public ArticleStatus Status { get; set; }

        public DateTime DatePublishment { get; set; }

        public DateTime DateCreation { get; set; }



        public string ContentBody { get; set; }

        public string Teaser { get; set; }

        public string Image { get; set; }
    }
}
