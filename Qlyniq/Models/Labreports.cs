using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qlyniq.Models
{
    [Table("labreports")]
    public partial class Labreports
    {
        public Labreports()
        {
            Examinations = new HashSet<Examinations>();
        }

        public uint Id { get; set; }
        public uint RecipientId { get; set; }
        public uint PatientId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime AcceptedTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SampledTime { get; set; }
        public float Glucose { get; set; }
        public float Urea { get; set; }
        public float Creatine { get; set; }
        public float Cholesterol { get; set; }
        [Column(TypeName = "tinyint(1)")]
        public sbyte Helicobacter { get; set; }

        [ForeignKey("PatientId")]
        [InverseProperty("Labreports")]
        public virtual Patients Patient { get; set; }
        [ForeignKey("RecipientId")]
        [InverseProperty("Labreports")]
        public virtual Employees Recipient { get; set; }
        [InverseProperty("LabReport")]
        public virtual ICollection<Examinations> Examinations { get; set; }
    }
}
