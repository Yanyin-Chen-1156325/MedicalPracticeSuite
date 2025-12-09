using DevExpress.XtraGrid;
using MedicalPracticeSuite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalPracticeSuite.Services
{
    public class DoctorService
    {
        private readonly MedicalContext context;

        public DoctorService()
        {
            context = new MedicalContext();
        }

        public List<Doctor> GetAll()
        {
            using (var context = new MedicalContext())
            {
                return context.Doctors.AsNoTracking().ToList();
            }
        }

        public List<Data.Doctor> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAll();
            }

            List<Data.Doctor> allDoctors = GetAll();

            string lowerTerm = searchTerm.Trim().ToLower();

            var results = allDoctors.Where(p =>
                p.Name != null && p.Name.ToLower().Contains(lowerTerm) ||
                p.Id.ToString().Contains(lowerTerm)
            ).ToList();

            return results;
        }

        public void Add(Doctor doctor)
        {
            context.Doctors.Add(doctor);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            var doctor = context.Doctors.FirstOrDefault(p => p.Id == id);
            if (doctor != null)
            {
                context.Doctors.Remove(doctor);
                context.SaveChanges();
            }
        }

        public void Update(Doctor doctor)
        {
            var existingDoctor = context.Doctors.FirstOrDefault(p => p.Id == doctor.Id);
            if (existingDoctor != null)
            {
                existingDoctor.Name = doctor.Name;
                existingDoctor.Specialty = doctor.Specialty;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Doctor not found.");
            }
        }
    }
}
