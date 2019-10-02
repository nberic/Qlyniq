using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("patients")]
    public partial class Patients
    {
        public Patients()
        {
            Appointments = new HashSet<Appointments>();
            Examinations = new HashSet<Examinations>();
            Files = new HashSet<Files>();
            Labreports = new HashSet<Labreports>();
        }

        [Key]
        public uint Id { get; set; }

        [Required]
        [Display(Name = "Social Security Number")]
        [StringLength(13, MinimumLength = 13)]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Empty")]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "Please input a sequence of 13 digits.")]
        [Column(TypeName = "varchar(13)")]
        public string SocialSecurityNumber { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [Column(TypeName = "varchar(255)")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [Column(TypeName = "varchar(255)")]
        public string LastName { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required]
        [RegularExpression(@"^Male$|^Female$", ErrorMessage = "Only valid values for the Gender field are 'Male' and 'Female'")]
        [Column(TypeName = "enum('Male','Female')")]
        public string Gender { get; set; }

        [Display(Name = "Healthcare Provider")]
        [Column(TypeName = "varchar(100)")]
        public string HealthCareProvider { get; set; }

        [InverseProperty("Patient")]
        public virtual ICollection<Appointments> Appointments { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<Examinations> Examinations { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<Files> Files { get; set; }
        [InverseProperty("Patient")]
        public virtual ICollection<Labreports> Labreports { get; set; }
    }
}
