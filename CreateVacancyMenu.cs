using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalRU
{
    public partial class CreateVacancyMenu : Form
    {
        public bool isOKed = false;
        public bool Choose = false; // 0 - new Vac and Person, 1 - new Vac based Unit
        public string currentID = "";
        //private UnitList UList;
        List<Unit> UList;
        public CreateVacancyMenu(List<Unit> UList)
        {
            this.UList = UList;
            InitializeComponent();
            refreshUnitList();
            if (UList.Count == 0) newVac_base_Person_rbutt.Enabled = false;
        }
        private void refreshUnitList()
        {
            this.dataGridView1.Rows.Clear();            
            foreach (Unit unit in this.UList)
            {                
                int rowNumber = this.dataGridView1.Rows.Add();                
                if (unit.IsVoid)
                {
                    this.dataGridView1.Rows[rowNumber].DefaultCellStyle.BackColor = Color.FromArgb(62, 215, 46);                    
                }
                this.dataGridView1.Rows[rowNumber].Cells["ProductionSite"].Value = unit.ProductionSite;
                this.dataGridView1.Rows[rowNumber].Cells["JTitle"].Value = unit.jobTitle;
                this.dataGridView1.Rows[rowNumber].Cells["FIO"].Value = unit.lastName + " \n" + unit.firstName + " \n" + unit.fatherName;
                this.dataGridView1.Rows[rowNumber].Cells["ID"].Value = unit.ID;
            }
        }
        private void OK_butt_Click(object sender, EventArgs e)
        {
            if (newVac_n_Person_rbutt.Checked) Choose = false;
            else Choose = true;
            isOKed = true;
            this.Close();
        }
        private void Cancel_butt_Click(object sender, EventArgs e)
        {
            isOKed = false;
            this.Close();
        }

        private void newVac_base_Person_rbutt_CheckedChanged(object sender, EventArgs e)
        {
            if (newVac_base_Person_rbutt.Checked) this.dataGridView1.Enabled = true;
            else this.dataGridView1.Enabled = false;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.currentID = Convert.ToString(this.dataGridView1.CurrentRow.Cells["ID"].Value);
        }

    }
}
