using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Website.Web.Pages.Categories
{
    public class CreateUpdateCatergoryModel
    {
        [Required]
        public string Name { get; set; }
    }
}
