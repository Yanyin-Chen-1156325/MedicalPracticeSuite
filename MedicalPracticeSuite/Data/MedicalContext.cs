using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalPracticeSuite.Data
{
    public class MedicalContext : DbContext
    {
        // Constructor with connection string name from App.config
        public MedicalContext() : base("name=MedicalDB")
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}
