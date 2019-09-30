using System;
using System.ComponentModel.DataAnnotations;

using Qlyniq.Models.Resources;

namespace Qlyniq.Models.Data
{
    public class Patient
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Social Security Number")]
        [StringLength(13, MinimumLength = 13)]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Empty")]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "Please input a sequence of 13 digits.")]
        public string SocialSecurityNumber { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        // [RegularExpression(@"Male|Female", ErrorMessage = "Only valid values for Gender field are 'Male' and 'Female'")]
        public Gender? Gender { get; set; }
    }
}