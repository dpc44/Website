using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Website.Categories;
using Website.Users;

namespace Website.Articles
{
    public class Article : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public Guid CategoryId { get; set; }

        public Guid IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        
        
        public ArticleStatus Status { get; set; }

        public DateTime DatePublishment { get; set; }

        public DateTime DateCreation { get; set; }

        
        public string ContentBody { get; set; }

        public string Teaser { get; set; }

        public string Image { get; set; }
        
    }
}
