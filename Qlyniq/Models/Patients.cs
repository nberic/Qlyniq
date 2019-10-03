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

        [NotMapped]
        public string Name => $"{ FirstName } { LastName } { SocialSecurityNumber }";

        [Display(Name = "Social Security Number")]
        [StringLength(13, MinimumLength = 13)]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "Please input a sequence of 13 digits.")]
        [Required]
        [Column(TypeName = "varchar(13)")]
        public string SocialSecurityNumber { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [RegularExpression(@"^Male$|^Female$", ErrorMessage = "Only valid values for the Gender field are 'Male' and 'Female'")]
        [Required]
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
