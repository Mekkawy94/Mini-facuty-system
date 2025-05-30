using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Faculity_System.Models
{
    public class Departments
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage ="Please, Enter a Valid Name")]
        [MinLength(2,ErrorMessage ="name of Department mustn't be less than 2 characters")]
        [MaxLength(10,ErrorMessage ="Name mustn't be more than 10 characters")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please, Enter a Valid Description")]
        [MaxLength(30, ErrorMessage = "Name mustn't be more than 30 characters")]
        public string Description { get; set; }

        [ValidateNever]
        public List<Students> Student { get; set; }

    }
}
