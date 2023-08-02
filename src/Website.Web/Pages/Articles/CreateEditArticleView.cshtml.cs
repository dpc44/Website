using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.ComponentModel.DataAnnotations;
using Website.Articles;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Website.Web.Pages.Articles
{
    public class CreateEditArticleViewModel
    {
        [SelectItems("Categories")]
        [DisplayName("Category")]
        public Guid CategoryId { get; set; }

        [SelectItems("Users")]
        [DisplayName("User")]
        public Guid IdentityUserId { get; set; }

        [Required]
        public string Name { get; set; }


        public ArticleStatus Status { get; set; }
        public DateTime DatePublishment { get; set; }

        public DateTime DateCreation { get; set; }
        public string ContentBody { get; set; }

        public string Teaser { get; set; }

        public string? Image { get; set; }

        /*[NotMapped]
        [Required(ErrorMessage = "Please choose Cover Image")]
        [Display(Name = "Cover Image")]
        public IFormFile imageFile { get; set; }*/

    }
}
