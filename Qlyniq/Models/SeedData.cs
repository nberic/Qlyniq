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
                            SocialSecurityNumber = "0507995100085",
                            FirstName = "Nemanja",
                            LastName = "Beric",
                            BirthDate = new DateTime(1995, 07, 05).Date,
                            Gender = "Male",
                            HealthCareProvider = "Comtrade d.o.o."
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
                            Id = 1,
                            SocialSecurityNumber = "0310979100034",
                            OfficeId = 1,
                            FirstName = "Nebojsa",
                            LastName = "Putnik",
                            BirthDate = new DateTime(1979, 01, 11).Date,
                            Gender = "Male",
                            IsMedicalWorker = true,
                            MedicalTitle = "Prim. Dr. of Family Medicine", 
                            IsDean = true,
                            DeanOfficeId = 1
                        }
                    );
                    context.SaveChanges();
                }
                #endregion

            }
        }
    }
}