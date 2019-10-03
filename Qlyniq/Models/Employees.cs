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
            Examinations = new HashSet<Examinations>();
            Files = new HashSet<Files>();
            Labreports = new HashSet<Labreports>();
        }

        [Key]
        public uint Id { get; set; }

        [NotMapped]
        public string Name => $"{ FirstName } { LastName } { SocialSecurityNumber }";

        [Display(Name = "Office")]
        public uint OfficeId { get; set; }

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

        [Required]
        [Display(Name = "Is Medical Worker")]
        [Column(TypeName = "tinyint(1)")]
        public bool? IsMedicalWorker { get; set; } = true;

        [Display(Name = "Medical Title")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "No Medical Title")]
        [Column(TypeName = "varchar(50)")]
        public string MedicalTitle { get; set; } = null;

        [Required]
        [Display(Name = "Is a Dean")]
        [Column(TypeName = "tinyint(1)")]
        public bool? IsDean { get; set; } = false;

        [Display(Name = "Dean to Office")]
        public uint? DeanOfficeId { get; set; }


        [ForeignKey("DeanOfficeId")]
        [InverseProperty("EmployeesDeanOffice")]
        public virtual Offices DeanOffice { get; set; }
        [ForeignKey("OfficeId")]
        [InverseProperty("EmployeesOffice")]
        public virtual Offices Office { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<Appointments> Appointments { get; set; }
        [InverseProperty("Doctor")]
        public virtual ICollection<Examinations> Examinations { get; set; }
        [InverseProperty("Creator")]
        public virtual ICollection<Files> Files { get; set; }
        [InverseProperty("Recipient")]
        public virtual ICollection<Labreports> Labreports { get; set; }
    }
}
