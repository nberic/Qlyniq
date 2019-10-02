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

        public uint Id { get; set; }
        public uint OfficeId { get; set; }
        [Required]
        [Column(TypeName = "varchar(13)")]
        public string SocialSecurityNumber { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string LastName { get; set; }
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [Required]
        [Column(TypeName = "enum('Male','Female')")]
        public string Gender { get; set; }
        [Column(TypeName = "tinyint(1)")]
        public sbyte? IsMedicalWorker { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string MedicalTitle { get; set; }

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
