using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;
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
    public partial class AppointmentForm : DevExpress.XtraEditors.XtraForm
    {
        private string _mode;
        private readonly AppointmentService _appointmentService = new AppointmentService();
        private readonly DoctorService doctorService = new DoctorService();
        private readonly PatientService patientService = new PatientService();
        private Appointment _appointment;

        public AppointmentForm(string mode = "add", Appointment appointment = null)
        {
            InitializeComponent();
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
            _mode = mode;
            _appointment = appointment;

            if (_mode == "add")
            {
                this.Text = "Add Appointment";
                lblID.Visible = false;
            }
            else if (_mode == "edit")
            {
                this.Text = "Edit Appointment";
                lblID.Visible = true;
            }
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            List<MedicalPracticeSuite.Data.Doctor> doctorList;
            doctorList = doctorService.GetAll();
            List<MedicalPracticeSuite.Data.Patient> patientList;
            patientList = patientService.GetAll();
            var fileter_patientList = patientList
                            .Select(p => new
                            {
                                p.Id,
                                p.Name,
                                p.DateOfBirth
                            })
                            .ToList();
            luDoctor.Properties.DataSource = doctorList;
            luDoctor.Properties.DisplayMember = "Name";
            luDoctor.Properties.ValueMember = "Id";
            luDoctor.Properties.NullText = "Select Doctor";
            luPatient.Properties.DataSource = fileter_patientList;
            luPatient.Properties.DisplayMember = "Name";
            luPatient.Properties.ValueMember = "Id";
            luPatient.Properties.NullText = "Select Patient";

            dtEndTime.Value = dtStartTime.Value.AddMinutes(30);
            if (_mode == "edit" && _appointment != null)
            {
                getEditAppointmentData(_appointment);
            }
        }
        /// <summary>
        /// Cancel Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        /// <summary>
        /// Save Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // Get raw EditValue
            var doctorObj = luDoctor.EditValue;
            var patientObj = luPatient.EditValue;

            // Validate IDs
            if (doctorObj == null || doctorObj == DBNull.Value)
            {
                MessageBox.Show("Please select a doctor.");
                return;
            }

            if (patientObj == null || patientObj == DBNull.Value)
            {
                MessageBox.Show("Please select a patient.");
                return;
            }

            // Try to convert to int (supports int, long, string, etc.)
            if (!int.TryParse(doctorObj.ToString(), out int doctorId))
            {
                try
                {
                    doctorId = Convert.ToInt32(doctorObj);
                }
                catch
                {
                    MessageBox.Show("Invalid doctor selection.");
                    return;
                }
            }

            if (!int.TryParse(patientObj.ToString(), out int patientId))
            {
                try
                {
                    patientId = Convert.ToInt32(patientObj);
                }
                catch
                {
                    MessageBox.Show("Invalid patient selection.");
                    return;
                }
            }

            // Get DateTime values
            DateTime startTime = dtStartTime.Value;
            DateTime endTime = dtEndTime.Value;

            if (startTime == null)
            {
                MessageBox.Show("Please select a start time.");
                return;
            }

            if (endTime == null)
            {
                MessageBox.Show("Please select an end time.");
                return;
            }

            // Ensure start < end
            if (startTime >= endTime)
            {
                MessageBox.Show("Start time must be earlier than end time.");
                return;
            }

            // Notes is optional
            string notes = string.Empty;
            if (txtNotes != null)
            {
                notes = txtNotes.Text?.Trim() ?? string.Empty;
            }

            // Create appointment object (example)
            var appointment = new Appointment
            {
                DoctorId = doctorId,
                PatientId = patientId,
                StartTime = startTime,
                EndTime = endTime,
                Notes = notes
            };

            // Check for conflicts
            if (HasConflict(appointment))
            {
                MessageBox.Show("This doctor already has an appointment in the selected time range. Please choose a different time.");
                return;
            }

            if (_mode == "add")
            {
                _appointmentService.Add(appointment);
            }
            else if (_mode == "edit")
            {
                int.TryParse(lblAppointmentID.Text, out int appointmentId);
                appointment.Id = appointmentId;
                _appointmentService.Update(appointment);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void getEditAppointmentData(Appointment appointment)
        {
            // Set ID
            lblAppointmentID.Text = appointment.Id.ToString();
            // Set Doctor
            luDoctor.EditValue = appointment.DoctorId;

            // Set Patient
            luPatient.EditValue = appointment.PatientId;

            // Set Start/End Time
            dtStartTime.Value = appointment.StartTime;   // DateTimeOffset or DateTime
            dtEndTime.Value = appointment.EndTime;

            // Set Notes
            txtNotes.Text = appointment.Notes ?? string.Empty;
        }

        private bool HasConflict(Appointment newAppointment)
        {
            var doctorAppointments = _appointmentService.GetByDoctorId(newAppointment.DoctorId);

            foreach (var appt in doctorAppointments)
            {
                if (_mode == "edit" && appt.Id == newAppointment.Id)
                    continue;

                bool overlap = newAppointment.StartTime < appt.EndTime &&
                               newAppointment.EndTime > appt.StartTime;
                if (overlap)
                    return true;
            }
            return false;
        }

    }
}
