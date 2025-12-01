using DevExpress.LookAndFeel;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraScheduler;
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
