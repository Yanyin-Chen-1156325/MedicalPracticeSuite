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
            return context.Doctors.ToList();
        }
    }
}
