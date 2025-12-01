using MedicalPracticeSuite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalPracticeSuite.Services
{
    public class PatientService
    {
        private readonly MedicalContext context;

        public PatientService()
        {
            context = new MedicalContext();
        }

        public List<Patient> GetAll()
        {
            return context.Patients.ToList();
        }

        public List<Data.Patient> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAll();
            }

            List<Data.Patient> allPatients = GetAll();

            string lowerTerm = searchTerm.Trim().ToLower();

            var results = allPatients.Where(p =>
                p.Name != null && p.Name.ToLower().Contains(lowerTerm) ||
                p.Id.ToString().Contains(lowerTerm)
            ).ToList();

            return results;
        }

        public void Add(Patient patient)
        {
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        public void Remove(int id)
        {
            var patient = context.Patients.FirstOrDefault(p => p.Id == id);
            if (patient != null)
            {
                context.Patients.Remove(patient);
                context.SaveChanges();
            }
        }
    }
}
