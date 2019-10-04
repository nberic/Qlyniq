using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Qlyniq.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new QlyniqContext(
                serviceProvider.GetRequiredService<DbContextOptions<QlyniqContext>>()))
            {

                #region Patients table seeding
                // look for any patients
                if (context.Patients.Any())
                {
                    return; // Database has already been seeded
                }
                // generate test database data
                else
                {
                    context.Patients.AddRange(
                        new Patients
                        {
                            Id = 1,
                            SocialSecurityNumber = "2404260619297",
                            FirstName = "Geoffrey",
                            LastName = "Wheeler",
                            BirthDate = new DateTime(1980, 08, 18).Date,
                            Gender = "Male",
                            HealthCareProvider = "XkCjDfwOPO"
                        },
                        new Patients
                        {
                            Id = 2,
                            SocialSecurityNumber = "5318256502461",
                            FirstName = "Myrtie",
                            LastName = "Knaggs",
                            BirthDate = new DateTime(1982, 04, 01).Date,
                            Gender = "Female",
                            HealthCareProvider = "riITQgRdhy nhFjoVMGwh"
                        },
                        new Patients
                        {
                            Id = 3,
                            SocialSecurityNumber = "9121671123422",
                            FirstName = "Cassarah",
                            LastName = "Kingston",
                            BirthDate = new DateTime(1988, 11, 03).Date,
                            Gender = "Male",
                            HealthCareProvider = "LoANMibcGS"
                        }
                        
                    );
                    context.SaveChanges();
                }
                #endregion

                #region Offices table seeding
                // look for any offices
                if (context.Offices.Any())
                {
                    return; // Database has already been seeded
                }
                // generate test database data
                else
                {
                    context.Offices.AddRange(
                        new Offices
                        {
                            Id = 1,
                            Name = "Family Medicine",
                            OfficeNumber = 1,
                            Budget = 1_000.0M
                        }, 
                        new Offices
                        {
                            Id = 2,
                            Name = "Emergency Room",
                            OfficeNumber = 2,
                            Budget = 3_000.0M
                        },
                        new Offices
                        {
                            Id = 3,
                            Name = "Stomatology",
                            OfficeNumber = 1,
                            Budget = 1_000.0M
                        },
                        new Offices
                        {
                            Id = 4,
                            Name = "Laboratory Room",
                            OfficeNumber = 4
                        }
                    );
                    context.SaveChanges();
                }
                #endregion

                #region Employees table seeding
                // look for any employees
                if (context.Employees.Any())
                {
                    return; // Database has already been seeded
                }
                // generate test database data
                else
                {
                    context.Employees.AddRange(
                        new Employees
                        {
                            SocialSecurityNumber = "1235226108237",
                            OfficeId = 4,
                            FirstName = "Elliot",
                            LastName = "Doe",
                            BirthDate = new DateTime(1988, 11, 03).Date,
                            Gender = "Male",
                            IsMedicalWorker = true,
                            MedicalTitle = "Dr. of Laboratory Medicine", 
                            IsDean = true,
                            DeanOfficeId = 4
                        }, 
                        new Employees
                        {
                            SocialSecurityNumber = "3935234748265",
                            OfficeId = 1,
                            FirstName = "Cali",
                            LastName = "Rose",
                            BirthDate = new DateTime(1997, 01, 20).Date,
                            Gender = "Female",
                            IsMedicalWorker = true,
                            MedicalTitle = "Prim. Dr. of Family Medicine", 
                            IsDean = true,
                            DeanOfficeId = 1
                        }, 
                        new Employees
                        {
                            SocialSecurityNumber = "2024166974381",
                            OfficeId = 3,
                            FirstName = "Rochelle",
                            LastName = "Thurstan",
                            BirthDate = new DateTime(1993, 04, 28).Date,
                            Gender = "Female",
                            IsMedicalWorker = true,
                            MedicalTitle = "Dr. of Emergency Medicine", 
                            IsDean = false
                        }, 
                        new Employees
                        {
                            SocialSecurityNumber = "9811064736082",
                            OfficeId = 4,
                            FirstName = "Edwin",
                            LastName = "Danell",
                            BirthDate = new DateTime(1988, 12, 16).Date,
                            Gender = "Male",
                            IsMedicalWorker = true,
                            MedicalTitle = "Dr. of Stomatology Medicine", 
                            IsDean = false
                        }
                        
                    );
                    context.SaveChanges();
                }
                #endregion

                #region Files table seeding
                // look for any files
                if (context.Files.Any())
                {
                    return; // Database has already been seeded
                }
                // generate test database data
                else
                {
                    context.Files.AddRange(
                        new Files
                        {
                            Name = "K101",
                            PatientId = 1,
                            CreatorId = 1,
                            CreationDate = DateTime.Now, 
                            Note = "XkCjDfwOPO riITQgRdhy"

                        },
                        new Files
                        {
                            Name = "J462",
                            PatientId = 3,
                            CreatorId = 2,
                            CreationDate = DateTime.Now, 
                            Note = "SVUGeFLgbI CsmvUCgGxR"

                        },
                        new Files
                        {
                            Name = "A123",
                            PatientId = 1,
                            CreatorId = 3,
                            CreationDate = DateTime.Now, 
                            Note = "qamUyQFHys"

                        }
                        
                    );
                    context.SaveChanges();
                }

                #endregion
            
                #region Labreports table seeding
                // look for any files
                if (context.Labreports.Any())
                {
                    return; // Database has already been seeded
                }
                // generate test database data
                else
                {
                    context.Labreports.AddRange(
                        new Labreports
                        {
                            RecipientId = 1,
                            PatientId = 2,
                            AcceptedTime = new DateTime(2019, 8, 2),
                            SampledTime = DateTime.Now,
                            Glucose = 11.0f,
                            Urea = 14.7f,
                            Creatine = 22.03f,
                            Cholesterol = 8.15f,
                            Helicobacter = false
                        }, 
                        new Labreports
                        {
                            RecipientId = 3,
                            PatientId = 1,
                            AcceptedTime = new DateTime(2019, 8, 2),
                            SampledTime = DateTime.Now,
                            Glucose = 15.23f,
                            Urea = 13.71f,
                            Creatine = 18.03f,
                            Cholesterol = 7.15f,
                            Helicobacter = false
                        }
                        
                    );
                    context.SaveChanges();
                }

                #endregion

                #region Diagnosis table seeding
                // look for any diagnosis
                if (context.Diagnosis.Any())
                {
                    return; // Database has already been seeded
                }
                // generate test database data
                else
                {
                    context.Diagnosis.AddRange(
                        new Diagnosis
                        {
                            Code = "M123",
                            MedicalTerm = "Imaginitis Acuta"
                        },
                        new Diagnosis
                        {
                            Code = "M124",
                            MedicalTerm = "Imaginitis Chronica"
                        }
                        
                    );
                    context.SaveChanges();
                }
                #endregion
            
                #region Examinations table seeding
                // look for any examinations
                if (context.Examinations.Any())
                {
                    return; // Database has already been seeded
                }
                // generate test database data
                else
                {
                    context.Examinations.AddRange(
                        new Examinations
                        {
                            PatientId = 1,
                            DoctorId = 3,
                            StartingTime = new DateTime(2019, 10, 3, 11, 59, 23),
                            FileId = 3,
                            DiagnosisId = 1,
                            Therapy = "Nihil",
                            IsEmergency = false,
                            LabReportId = 2
                        }
                        
                    );
                    context.SaveChanges();
                }
                #endregion

                #region Appointments table seeding
                // look for any appointments
                if (context.Appointments.Any())
                {
                    return; // Database has already been seeded
                }
                // generate test database data
                else
                {
                    context.Appointments.AddRange(
                        new Appointments
                        {
                            PatientFirstName = "Jannet",
                            PatientLastName = "Thompson",
                            DoctorId = 3,
                            StartingTime = DateTime.Now
                        }
                        
                    );
                    context.SaveChanges();
                }
                #endregion
            }
        }
    }
}   