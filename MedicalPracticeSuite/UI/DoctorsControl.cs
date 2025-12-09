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
    public partial class DoctorsControl : UserControl
    {
        private DoctorService doctorService;
        public DoctorsControl()
        {
            InitializeComponent();
            doctorService = new DoctorService();
        }

        private void DoctorsControl_Load(object sender, EventArgs e)
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
            LoadDoctorData();
        }

        public void LoadDoctorData(string searchTerm = null)
        {
            List<MedicalPracticeSuite.Data.Doctor> doctorList;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                doctorList = doctorService.GetAll();
            }
            else
            {
                doctorList = doctorService.Search(searchTerm);
            }

            if (doctorList != null)
            {
                gridControl1.DataSource = doctorList;
            }
            else
            {
                gridControl1.DataSource = null;
            }

            gridControl1.RefreshDataSource();
        }

        // Get selected doctor rows
        public int[] GetSelectedDoctorRows()
        {
            return gridView1.GetSelectedRows();
        }

        public Doctor GetDoctorByRowHandle(int rowHandle)
        {
            if (rowHandle < 0) return null;

            return new Doctor
            {
                Id = (int)gridView1.GetRowCellValue(rowHandle, "Id"),
                Name = (string)gridView1.GetRowCellValue(rowHandle, "Name"),
                Specialty = (string)gridView1.GetRowCellValue(rowHandle, "Specialty")
            };
        }
    }
}
