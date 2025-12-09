using DevExpress.XtraGrid;
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

        // Get selected patient rows
        public int[] GetSelectedPatientRows()
        {
            return gridView1.GetSelectedRows();
        }

        public Patient GetPatientByRowHandle(int rowHandle)
        {
            if (rowHandle < 0) return null;

            return new Patient
            {
                Id = (int)gridView1.GetRowCellValue(rowHandle, "Id"),
                Name = (string)gridView1.GetRowCellValue(rowHandle, "Name"),
                Phone = (string)gridView1.GetRowCellValue(rowHandle, "Phone"),
                Email = (string)gridView1.GetRowCellValue(rowHandle, "Email"),
                DateOfBirth = (DateTime)gridView1.GetRowCellValue(rowHandle, "DateOfBirth")
            };
        }
    }
}
