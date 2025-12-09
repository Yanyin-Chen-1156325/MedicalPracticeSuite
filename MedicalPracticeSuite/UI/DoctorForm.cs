using DevExpress.LookAndFeel;
using MedicalPracticeSuite.Data;
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

namespace MedicalPracticeSuite.UI
{
    public partial class DoctorForm : DevExpress.XtraEditors.XtraForm
    {
        private string _mode;
        private readonly DoctorService _doctorService = new DoctorService();
        private Doctor _doctor;
        public DoctorForm(string mode = "add", Doctor doctor = null)
        {
            InitializeComponent();
            // Apply dark theme
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
            _mode = mode;
            _doctor = doctor;

            if (_mode == "add")
            {
                this.Text = "Add Doctor";
                lblDoctorID.Visible = false;
            }
            else if (_mode == "edit")
            {
                this.Text = "Edit Doctor";
                lblDoctorID.Visible = true;
                getEditDoctorData(_doctor);
            }   
        }

        private void DoctorForm_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// DoctorForm Cancel Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// DoctorForm Save Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDoctorName.Text))
            {
                MessageBox.Show("Please enter the doctor's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (_mode == "edit")
                {
                    getEditDoctorData(_doctor);
                }
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDoctorSpecialty.Text))
            {
                MessageBox.Show("Please enter the doctor's specialty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (_mode == "edit")
                {
                    getEditDoctorData(_doctor);
                }
                return;
            }

            // Create new doctor object
            var doctor = new Doctor
            {
                Name = txtDoctorName.Text,
                Specialty = txtDoctorSpecialty.Text,
            };

            if (_mode == "add")
            {
                _doctorService.Add(doctor);
            }
            else if (_mode == "edit")
            {
                doctor.Id = _doctor.Id;
                _doctorService.Update(doctor);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void getEditDoctorData(Doctor doctor)
        {
            txtDoctorID.Text = doctor.Id.ToString();
            txtDoctorName.Text = doctor.Name;
            txtDoctorSpecialty.Text = doctor.Specialty;
        }
    };
}
