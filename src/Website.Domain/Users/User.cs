using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.TenantManagement;

namespace Website.Users
{
    public class User : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string ImageUser { get; set; }

        public string role { get; set; }
    }

    
}
