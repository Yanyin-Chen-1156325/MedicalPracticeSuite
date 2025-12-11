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

namespace MedicalPracticeSuite
{
    public partial class AppointmentsControl : UserControl
    {
        private AppointmentService appointmentService;
        public AppointmentsControl()
        {
            InitializeComponent();
            appointmentService = new AppointmentService();
        }

        private void AppointmentsControl_Load(object sender, EventArgs e)
        {
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gridView1.SelectionChanged += (s, ee) =>
            {
                if (gridView1.SelectedRowsCount > 1)
                {
                    // Keep only the last selected row
                    int[] selected = gridView1.GetSelectedRows();
                    int last = selected[selected.Length - 1];

                    gridView1.ClearSelection();
                    gridView1.SelectRow(last);
                }
            };
            LoadAppointmentData();

        }

        public void LoadAppointmentData(string searchTerm = null)
        {
            List<MedicalPracticeSuite.Data.Appointment> appointmentList;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                appointmentList = appointmentService.GetAll();
            }
            else
            {
                appointmentList = appointmentService.Search(searchTerm);
            }

            if (appointmentList != null)
            {
                gridControl1.DataSource = appointmentList.Select(a => new
                {
                    a.Id,
                    PatientId = a.PatientId,
                    PatientName = a.Patient.Name,
                    DoctorId = a.DoctorId,
                    DoctorName = a.Doctor.Name,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    a.Notes
                }).ToList();
            }
            else
            {
                gridControl1.DataSource = null;
            }

            gridControl1.RefreshDataSource();
            gridView1.Columns["PatientId"].Visible = false;
            gridView1.Columns["DoctorId"].Visible = false;
            gridView1.Columns["StartTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["StartTime"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            gridView1.Columns["EndTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView1.Columns["EndTime"].DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";


        }

        // Get selected appointment rows
        public int[] GetSelectedAppointmentRows()
        {
            return gridView1.GetSelectedRows();
        }

        public Appointment GetAppointmentByRowHandle(int rowHandle)
        {
            if (rowHandle < 0) return null;

            return new Appointment
            {
                Id = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Id")),
                DoctorId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "DoctorId")),
                PatientId = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "PatientId")),
                StartTime = (DateTime)gridView1.GetRowCellValue(rowHandle, "StartTime"),
                EndTime = (DateTime)gridView1.GetRowCellValue(rowHandle, "EndTime"),
                Notes = gridView1.GetRowCellValue(rowHandle, "Notes")?.ToString() ?? string.Empty
            };
        }
    }
}
