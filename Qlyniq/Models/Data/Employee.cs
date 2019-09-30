using System;
using System.ComponentModel.DataAnnotations;

using Qlyniq.Models.Resources;

namespace Qlyniq.Models.Data
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a positive integer.")]
        public int OfficeId { get; set; }

        [Display(Name = "Social Security Number")]
        [StringLength(13, MinimumLength = 13)]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Empty")]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "Please input a sequence of 13 digits.")]
        [Required]
        public string SocialSecurityNumber { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        public Gender? Gender { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Is Medical Worker")]
        public bool IsMedicalWorker { get; set; } = false;

        [Display(Name = "Medical Title")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "No Medical Title")]
        public string MedicalTitle { get; set; }

    }
}