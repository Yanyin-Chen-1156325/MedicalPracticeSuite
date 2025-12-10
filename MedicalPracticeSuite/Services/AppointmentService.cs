using MedicalPracticeSuite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalPracticeSuite.Services
{
    public class AppointmentService
    {
        private readonly MedicalContext context;

        public AppointmentService()
        {
            context = new MedicalContext();
        }

        public List<Appointment> GetAll()
        {
            using (var context = new MedicalContext())
            {
                return context.Appointments
                              .Include("Patient")
                              .Include("Doctor")
                              .AsNoTracking()
                              .ToList();
            }
        }

        public void Add(Appointment appointment)
        {
            context.Appointments.Add(appointment);
            context.SaveChanges();
        }
    }
}
