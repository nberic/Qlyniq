using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("employees")]
    public partial class Employees
    {
        public Employees()
        {
            Appointments = new HashSet<Appointments>();
            Deans = new HashSet<Deans>();
            Examinations = new HashSet<Examinations>();
            Files = new HashSet<Files>();
            Labreports = new HashSet<Labreports>();
        }

        [Key]
        public uint Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive integer.")]
        public uint OfficeId { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 13)]
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

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [RegularExpression(@"^Male$|^Female$", ErrorMessage = "Only valid values for the Gender field are 'Male' and 'Female'")]
        [Column(TypeName = "enum('Male','Female')")]
        public string Gender { get; set; }


        [Display(Name = "Is Medical Worker")]
        [Column(TypeName = "tinyint(1)")]
        public bool? IsMedicalWorker { get; set; } = false;

        [Display(Name = "Medical Title")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "No Medical Title")]
        [Column(TypeName = "varchar(50)")]
        public string MedicalTitle { get; set; } = null;

        [ForeignKey("OfficeId")]
        [InverseProperty("Employees")]
        public virtual Offices Office { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<Appointments> Appointments { get; set; }
        [InverseProperty("Employee")]
        public virtual ICollection<Deans> Deans { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<Examinations> Examinations { get; set; }
        [InverseProperty("Creator")]
        public virtual ICollection<Files> Files { get; set; }
        [InverseProperty("Recipient")]
        public virtual ICollection<Labreports> Labreports { get; set; }
    }
}
