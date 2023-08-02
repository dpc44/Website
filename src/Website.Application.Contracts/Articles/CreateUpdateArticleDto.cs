using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Website.Articles
{
    //: IValidatableObject for custom validation (the last method)
    public class CreateUpdateArticleDto :IValidatableObject
    {
        [Required]
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Guid IdentityUserId { get; set; }
        public ArticleStatus Status { get; set; }
        public DateTime DatePublishment { get; set; }

        public DateTime DateCreation { get; set; }
        public string ContentBody { get; set; }

        public string Teaser { get; set; }
        //[Url]
        public string? Image { get; set; }


        //Custom validation - 207/184 page
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == ContentBody)
            {
                yield return new ValidationResult(
                   "Name and Description can not be the same!",
                   new[] { "Name", "Description" });
            }
        }
    }
}
