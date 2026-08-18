using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required(ErrorMessage = "Feature image is required.")]
        public IFormFile FeatureImage { get; set; }
    }
}
