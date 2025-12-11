using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.UI;
using MedicalPracticeSuite.Data;
using MedicalPracticeSuite.Services;
using MedicalPracticeSuite.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppointmentForm = MedicalPracticeSuite.UI.AppointmentForm;

namespace MedicalPracticeSuite
{
    public partial class MainForm : Form
    {
        PatientsControl patientsControl = new PatientsControl();
        DoctorsControl doctorsControl = new DoctorsControl();
        AppointmentsControl appointmentsControl = new AppointmentsControl();

        public MainForm()
        {
            InitializeComponent();
            // Apply dark theme
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");
            // Prevent direct editing of appointments in SchedulerControl
            schedulerControl1.OptionsCustomization.AllowAppointmentEdit = DevExpress.XtraScheduler.UsedAppointmentType.Custom;
            schedulerControl1.EditAppointmentFormShowing += SchedulerControl1_EditAppointmentFormShowing;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Show Dashboard by default
            ShowDashboard();
        }

        private void ribbonControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            var page = ribbonControl1.SelectedPage;

            if (page == ribbonHome)
                ShowDashboard();
            else if (page == ribbonPatients)
                ShowControl(patientsControl);
            else if (page == ribbonDoctors)
                ShowControl(doctorsControl);    
            else if (page == ribbonAppointments)
                ShowControl(appointmentsControl);
        }

        private void ShowControl(UserControl control)
        {
            mainPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(control);
        }

        private void ShowDashboard()
        {
            mainPanel.Controls.Clear();

            // DateNavigator on the right
            this.dateNavigator1.Dock = DockStyle.Right;
            if (!mainPanel.Controls.Contains(dateNavigator1))
            {
                dateNavigator1.Dock = DockStyle.Right;
                mainPanel.Controls.Add(dateNavigator1);
            }

            // SchedulerControl fills the rest of the space(Left)
            this.schedulerControl1.Dock = DockStyle.Fill;
            if (!mainPanel.Controls.Contains(schedulerControl1))
            {
                schedulerControl1.Dock = DockStyle.Fill;
                mainPanel.Controls.Add(schedulerControl1);
            }

            // Scheduler
            var appointmentService = new AppointmentService();
            var appointments = appointmentService.GetAll();

            LoadAppointmentsToScheduler(appointments);
            schedulerControl1.Start = DateTime.Today;
        }

        #region Patients Ribbon Events
        /// <summary>
        /// Patient Add New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var form = new PatientForm("add"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    patientsControl.LoadPatientData();
                }
            }
        }
        /// <summary>
        /// Patient Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRows = patientsControl.GetSelectedPatientRows();

            if (selectedRows.Length == 1)
            {
                int rowHandle = selectedRows[0];
                var patient = patientsControl.GetPatientByRowHandle(rowHandle);
                using (var form = new PatientForm("edit", patient))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        patientsControl.LoadPatientData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a single patient to edit.", "Edit Patient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        /// <summary>
        /// Patient Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRows = patientsControl.GetSelectedPatientRows();

            if (selectedRows.Length == 1)
            {
                int rowHandle = selectedRows[0];
                var patient = patientsControl.GetPatientByRowHandle(rowHandle);
                if (patient == null)
                    return;
                var result = MessageBox.Show(
                    $"Are you sure you want to delete patient '{patient.Name}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var patientService = new PatientService();
                        patientService.Remove(patient.Id);

                        patientsControl.LoadPatientData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a single patient to delete.", "Delete Patient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Patient Refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            patientsControl.LoadPatientData();
        }

        /// <summary>
        /// Patients Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem1_EditValueChanged(object sender, EventArgs e)
        {
            ExecuteSearch();
        }
        #endregion
        #region Doctors Ribbon Events
        /// <summary>
        /// Doctor Add New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var form = new DoctorForm("add"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    doctorsControl.LoadDoctorData();
                }
            }
        }
        /// <summary>
        /// Doctor Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRows = doctorsControl.GetSelectedDoctorRows();

            if (selectedRows.Length == 1)
            {
                int rowHandle = selectedRows[0];
                var doctor = doctorsControl.GetDoctorByRowHandle(rowHandle);
                using (var form = new DoctorForm("edit", doctor))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        doctorsControl.LoadDoctorData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a single doctor to edit.", "Edit Patient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Doctor Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRows = doctorsControl.GetSelectedDoctorRows();
            if (selectedRows.Length == 1)
            {
                int rowHandle = selectedRows[0];
                var doctor = doctorsControl.GetDoctorByRowHandle(rowHandle);
                if (doctor == null)
                    return;
                var result = MessageBox.Show(
                    $"Are you sure you want to delete doctor '{doctor.Name}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var doctorService = new DoctorService();
                        doctorService.Remove(doctor.Id);

                        doctorsControl.LoadDoctorData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete doctor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a single doctor to delete.", "Delete Doctor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Doctor Refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            doctorsControl.LoadDoctorData();
        }
        /// <summary>
        /// Doctor Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem2_EditValueChanged(object sender, EventArgs e)
        {
            ExecuteSearch();
        }
        #endregion
        #region Appointments Ribbon Events
        /// <summary>
        /// Appointment Add New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var form = new AppointmentForm("add"))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    appointmentsControl.LoadAppointmentData();
                }
            }
        }
        /// <summary>
        /// Appointment Edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRows = appointmentsControl.GetSelectedAppointmentRows();

            if (selectedRows.Length == 1)
            {
                int rowHandle = selectedRows[0];
                var appointment = appointmentsControl.GetAppointmentByRowHandle(rowHandle);
                using (var form = new AppointmentForm("edit", appointment))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        appointmentsControl.LoadAppointmentData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a single appointment to edit.", "Edit Appointment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Appointment Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var selectedRows = appointmentsControl.GetSelectedAppointmentRows();

            if (selectedRows.Length == 1)
            {
                int rowHandle = selectedRows[0];
                var appointment = appointmentsControl.GetAppointmentByRowHandle(rowHandle);
                if (appointment == null)
                    return;
                var result = MessageBox.Show(
                    $"Are you sure you want to delete appointment '{appointment.Id}'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var appointmentService = new AppointmentService();
                        appointmentService.Remove(appointment.Id);

                        appointmentsControl.LoadAppointmentData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a single appointment to delete.", "Delete Appointment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Appointment Refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            appointmentsControl.LoadAppointmentData();
        }
        /// <summary>
        /// Appointment Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barEditItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExecuteSearch();
        }
        #endregion
        #region Gerneral
        /// <summary>
        /// Search Execution
        /// </summary>
        private void ExecuteSearch()
        {
            if (ribbonControl1.SelectedPage.Text == "Patients")
            {
                if (this.barEditItem1 != null)
                {
                    string searchTerm = this.barEditItem1.EditValue?.ToString();
                    patientsControl.LoadPatientData(searchTerm);
                }
                else
                {                     
                    patientsControl.LoadPatientData(); 
                }
            }
            else if (ribbonControl1.SelectedPage.Text == "Doctors")
            {
                if (this.barEditItem2 != null)
                {
                    string searchTerm = this.barEditItem2.EditValue?.ToString();
                    doctorsControl.LoadDoctorData(searchTerm);
                }
                else
                {
                    doctorsControl.LoadDoctorData();
                }
            }
            else if (ribbonControl1.SelectedPage.Text == "Appointments")
            {
                if (this.barEditItem3 != null)
                {
                    string searchTerm = this.barEditItem3.EditValue?.ToString();
                    appointmentsControl.LoadAppointmentData(searchTerm);
                }
                else
                {
                    appointmentsControl.LoadAppointmentData();
                }
            }
        }

        private void LoadAppointmentsToScheduler(List<Data.Appointment> appointments)
        {
            schedulerDataStorage1.Appointments.Clear();

            foreach (var appt in appointments)
            {
                var schedAppt = schedulerDataStorage1.CreateAppointment(AppointmentType.Normal);

                schedAppt.Subject = $"{appt.Patient.Name} with {appt.Doctor.Name}";
                schedAppt.Start = appt.StartTime;
                schedAppt.End = appt.EndTime;
                schedAppt.CustomFields["Notes"] = appt.Notes;
                schedAppt.Description = appt.Notes;

                schedulerDataStorage1.Appointments.Add(schedAppt);
            }
        }
        /// <summary>
        /// Prevent Default Appointment Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SchedulerControl1_EditAppointmentFormShowing(object sender, DevExpress.XtraScheduler.AppointmentFormEventArgs e)
        {
            e.Handled = true;

            // 之後才是您客製化表單的邏輯：
            // MyCustomAppointmentForm customForm = new MyCustomAppointmentForm(schedulerControl1, e.Appointment);
            // customForm.ShowDialog();
        }



        #endregion

        
    }
}
