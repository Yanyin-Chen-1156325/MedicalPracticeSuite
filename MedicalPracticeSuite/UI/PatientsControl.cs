using DevExpress.XtraGrid;
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
    public partial class PatientsControl : UserControl
    {
        private PatientService patientService;

        public PatientsControl()
        {
            InitializeComponent();
            patientService = new PatientService();
        }

        private void PatientsControl_Load(object sender, EventArgs e)
        {
            LoadPatientData();
        }

        public void LoadPatientData(string searchTerm = null)
        {
            List<MedicalPracticeSuite.Data.Patient> patientList;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                patientList = patientService.GetAll();
            }
            else
            {
                patientList = patientService.Search(searchTerm);
            }

            if (patientList != null)
            {
                gridControl1.DataSource = patientList;
            }
            else
            {
                gridControl1.DataSource = null;
            }

            gridControl1.RefreshDataSource();
        }
    }
}
