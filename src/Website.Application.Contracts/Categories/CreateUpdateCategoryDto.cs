using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Website.Categories
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
