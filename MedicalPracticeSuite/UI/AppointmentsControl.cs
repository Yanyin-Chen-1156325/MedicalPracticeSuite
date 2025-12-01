using MedicalPracticeSuite.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalPracticeSuite
{
    public partial class AppointmentsControl : UserControl
    {
        private AppointmentService appointmentService;
        public AppointmentsControl()
        {
            InitializeComponent();
        }

        private void AppointmentsControl_Load(object sender, EventArgs e)
        {
            appointmentService = new AppointmentService();

            List<MedicalPracticeSuite.Data.Appointment> appointmentList;

            appointmentList = appointmentService.GetAll();

            if (appointmentList != null && appointmentList.Count > 0)
            {
                gridControl1.DataSource = appointmentList.Select(a => new
                {
                    a.Id,
                    PatientName = a.Patient.Name,
                    DoctorName = a.Doctor.Name,
                    a.StartTime,
                    a.EndTime,
                    a.Notes
                }).ToList();
            }
            else
            {
                gridControl1.DataSource = null;
            }

        }
    }
}
