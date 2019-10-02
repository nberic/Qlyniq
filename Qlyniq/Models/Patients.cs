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

        public uint Id { get; set; }
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
        public DateTime? BirthDate { get; set; }
        [Required]
        [Column(TypeName = "enum('Male','Female')")]
        public string Gender { get; set; }
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
