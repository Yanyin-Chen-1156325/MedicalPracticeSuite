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
    public partial class PatientForm : DevExpress.XtraEditors.XtraForm
    {
        private string _mode;
        private readonly PatientService _patientService = new PatientService();
        private Patient _patient;
        public PatientForm(string mode = "add", Patient patient = null)
        {
            InitializeComponent();
            // Apply dark theme
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
            _mode = mode;
            _patient = patient;

            if (_mode == "add")
            {
                this.Text = "Add Patient";
                lblPatientID.Visible = false;
            }
            else if (_mode == "edit")
            {
                this.Text = "Edit Patient";
                lblPatientID.Visible = true;
                getEditPatientData(_patient);
            }   
        }

        private void PatientForm_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// PatientForm Cancel Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// PatientForm Save Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientName.Text))
            {
                MessageBox.Show("Please enter the patient's name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (_mode == "edit")
                {
                    getEditPatientData(_patient);
                }
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPatientPhone.Text))
            {
                MessageBox.Show("Please enter the patient's phone.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (_mode == "edit")
                {
                    getEditPatientData(_patient);
                }
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPatientEmail.Text))
            {
                MessageBox.Show("Please enter the patient's Email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (_mode == "edit")
                {
                    getEditPatientData(_patient);
                }
                return;
            }
            if (txtPatientEmail.Text.IndexOf('@') == -1 || txtPatientEmail.Text.IndexOf('.') == -1)
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (_mode == "edit")
                {
                    getEditPatientData(_patient);
                }
                return;
            }

            // Create new patient object
            var patient = new Patient
            {
                Name = txtPatientName.Text,
                Phone = txtPatientPhone.Text,
                Email = txtPatientEmail.Text,
                DateOfBirth = dtPickerPatient.Value
            };

            if (_mode == "add")
            {
                _patientService.Add(patient);
            }
            else if (_mode == "edit")
            {
                patient.Id = _patient.Id;
                _patientService.Update(patient);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void getEditPatientData(Patient patient)
        {
            txtPatientID.Text = patient.Id.ToString();
            txtPatientName.Text = patient.Name;
            txtPatientPhone.Text = patient.Phone;
            txtPatientEmail.Text = patient.Email;
            dtPickerPatient.Value = patient.DateOfBirth;
        }
    };
}
