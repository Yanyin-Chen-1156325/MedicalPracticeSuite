using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraScheduler;
using MedicalPracticeSuite.Data;
using MedicalPracticeSuite.Services;
using MedicalPracticeSuite.UI;
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
        }

        #endregion
        
    }
}
