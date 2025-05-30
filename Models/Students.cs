using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Faculity_System.Models
{
    public class Students
    {
        public int Id { get; set; }

        [DisplayName("FullName")]
        [Required(ErrorMessage = "Please, Enter a Valid Name")]
        [MinLength(6, ErrorMessage = "name of Department mustn't be less than 6 characters")]
        [MaxLength(50, ErrorMessage = "Name mustn't be more than 50 characters")]
        public string FullName { get; set; }

        [Range(0,4,ErrorMessage ="Invalid GPA")]
        [DisplayName("GPA")]
        public float GPA { get; set; }

        [Range(1,4,ErrorMessage ="Invalid Level")]
        [DisplayName("Level")]
        public sbyte Level { get; set; }

        [ValidateNever]
        [DisplayName("Date Of Birth")]
        public DateTime BirthDate { get; set; }

        [ValidateNever]
        [DisplayName("Date Of Join")]
        public DateTime JoinDate { get; set; }

        [ValidateNever]
        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        [DisplayName("Department")]
        [Range(1,int.MaxValue , ErrorMessage ="Choose a Valid Department")]
        public int DepartmentsId { get; set; }
        [ValidateNever]
        public Departments Departments { get; set; }


    }
}
