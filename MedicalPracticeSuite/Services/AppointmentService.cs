using MedicalPracticeSuite.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public List<Data.Appointment> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAll();
            }

            List<Data.Appointment> allAppointments = GetAll();

            string lowerTerm = searchTerm.Trim().ToLower();

            var results = allAppointments.Where(p =>
                p.Patient != null && p.Patient.Name.ToLower().Contains(lowerTerm) ||
                p.Doctor != null && p.Doctor.Name.ToLower().Contains(lowerTerm)
            ).ToList();

            return results;
        }
        public void Add(Appointment appointment)
        {
            context.Appointments.Add(appointment);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            var appointment = context.Appointments.FirstOrDefault(p => p.Id == id);
            if (appointment != null)
            {
                context.Appointments.Remove(appointment);
                context.SaveChanges();
            }
        }

        public void Update(Appointment appointment)
        {
            var existingAppointment = context.Appointments.FirstOrDefault(p => p.Id == appointment.Id);
            if (existingAppointment != null)
            {
                existingAppointment.PatientId = appointment.PatientId;
                existingAppointment.DoctorId = appointment.DoctorId;
                existingAppointment.StartTime = appointment.StartTime;
                existingAppointment.EndTime = appointment.EndTime;
                existingAppointment.Notes = appointment.Notes;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Appointment not found.");
            }
        }

        public List<Appointment> GetByDoctorId(int doctorId)
        {
            using (var context = new MedicalContext())
            {
                return context.Appointments
                              .Where(a => a.DoctorId == doctorId)
                              .Include(a => a.Patient)
                              .Include(a => a.Doctor)
                              .AsNoTracking()
                              .ToList();
            }
        }

    }
}
