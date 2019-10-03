using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Qlyniq.Models
{
    public partial class QlyniqContext : DbContext
    {
        public QlyniqContext()
        {
        }

        public QlyniqContext(DbContextOptions<QlyniqContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<Diagnosis> Diagnosis { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Examinations> Examinations { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Labreports> Labreports { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<Patients> Patients { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseMySql("Server=127.0.0.1; Port=3306; Database=Qlyniq; Uid=nberic; Pwd=nbericpass");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.HasIndex(e => e.DoctorId)
                    .HasName("FK_Appointments_DoctorId");

                entity.HasIndex(e => e.PatientId)
                    .HasName("FK_Appointments_PatientId");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointments_DoctorId");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Appointments_PatientId");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasIndex(e => e.DeanOfficeId)
                    .HasName("FK_Employees_DeanOfficeId");

                entity.HasIndex(e => e.OfficeId)
                    .HasName("FK_Employees_OfficeId");

                entity.HasIndex(e => e.SocialSecurityNumber)
                    .HasName("SocialSecurityNumber")
                    .IsUnique();

                entity.Property(e => e.IsDean).HasDefaultValueSql("'0'");

                entity.Property(e => e.IsMedicalWorker).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.DeanOffice)
                    .WithMany(p => p.EmployeesDeanOffice)
                    .HasForeignKey(d => d.DeanOfficeId)
                    .HasConstraintName("FK_Employees_DeanOfficeId");

                entity.HasOne(d => d.Office)
                    .WithMany(p => p.EmployeesOffice)
                    .HasForeignKey(d => d.OfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_OfficeId");
            });

            modelBuilder.Entity<Examinations>(entity =>
            {
                entity.HasIndex(e => e.DiagnosisId)
                    .HasName("FK_Examinations_DiagnosisId");

                entity.HasIndex(e => e.DoctorId)
                    .HasName("FK_Examinations_DoctorId");

                entity.HasIndex(e => e.FileId)
                    .HasName("FK_Examinations_FileId");

                entity.HasIndex(e => e.LabReportId)
                    .HasName("FK_Examinations_LabReportId");

                entity.HasIndex(e => e.PatientId)
                    .HasName("FK_Examinations_PatientId");

                entity.Property(e => e.IsEmergency).HasDefaultValueSql("'0'");

                entity.Property(e => e.Therapy).HasDefaultValueSql("'Nihil'");

                entity.HasOne(d => d.Diagnosis)
                    .WithMany(p => p.Examinations)
                    .HasForeignKey(d => d.DiagnosisId)
                    .HasConstraintName("FK_Examinations_DiagnosisId");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Examinations)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Examinations_DoctorId");

                entity.HasOne(d => d.File)
                    .WithMany(p => p.Examinations)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Examinations_FileId");

                entity.HasOne(d => d.LabReport)
                    .WithMany(p => p.Examinations)
                    .HasForeignKey(d => d.LabReportId)
                    .HasConstraintName("FK_Examinations_LabReportId");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Examinations)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Examinations_PatientId");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.HasIndex(e => e.CreatorId)
                    .HasName("FK_Files_CreatorId");

                entity.HasIndex(e => e.PatientId)
                    .HasName("FK_Files_PatientId");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Files_CreatorId");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Files)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Files_PatientId");
            });

            modelBuilder.Entity<Labreports>(entity =>
            {
                entity.HasIndex(e => e.PatientId)
                    .HasName("FK_LabResults_PatientId");

                entity.HasIndex(e => e.RecipientId)
                    .HasName("FK_LabResults_RecipientId");

                entity.Property(e => e.Helicobacter).HasDefaultValueSql("'0'");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Labreports)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LabResults_PatientId");

                entity.HasOne(d => d.Recipient)
                    .WithMany(p => p.Labreports)
                    .HasForeignKey(d => d.RecipientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LabResults_RecipientId");
            });

            modelBuilder.Entity<Offices>(entity =>
            {
                entity.Property(e => e.Budget).HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Patients>(entity =>
            {
                entity.HasIndex(e => e.SocialSecurityNumber)
                    .HasName("SocialSecurityNumber")
                    .IsUnique();
            });
        }
    }
}
