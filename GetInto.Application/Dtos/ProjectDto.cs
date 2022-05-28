using System.ComponentModel.DataAnnotations;

namespace GetInto.Application.Dtos
{
    public class ProjectDto
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Allowable range from 3 to 50 characters.")]
        public string Title { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$",
            ErrorMessage = "This is not a valid image. (gif, jpg, jpeg, bmp ou png)")]
        public string ImageURL { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Phone(ErrorMessage = "The field {0} has an invalid number.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [Display(Name = "email address")]
        [EmailAddress(ErrorMessage = "A valid {0} is required.")]
        public string Email { get; set; }
        public IEnumerable<JobDto> Jobs { get; set; }
        public IEnumerable<HumanDto> Humans { get; set; }
    }
}
