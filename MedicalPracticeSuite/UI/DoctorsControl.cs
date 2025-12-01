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
    public partial class DoctorsControl : UserControl
    {
        private DoctorService doctorService;
        public DoctorsControl()
        {
            InitializeComponent();
        }

        private void DoctorsControl_Load(object sender, EventArgs e)
        {
            doctorService = new DoctorService();

            List<MedicalPracticeSuite.Data.Doctor> doctorList;

            doctorList = doctorService.GetAll();

            if (doctorList != null && doctorList.Count > 0)
            {
                gridControl1.DataSource = doctorList;
            }
            else
            {
                gridControl1.DataSource = null;
            }
        }
    }
}
