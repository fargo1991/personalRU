using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace PersonalRU
{
    public partial class MainWindow : Form
    {
        public Project CurrentProject;
        private Unit currentUnit;
        private SRL.Response currentResponse;
        private List<SRL.Response> RList;

        private Bitmap defUnitPhoto;

        private Bitmap delete_img;
        private string defaultUnitString = "";
        private string defaultProjectsDirPath = "";
        public MainWindow()
        {
            defaultProjectsDirPath = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - Application.ProductName.Length - 4) + "Projects\\";

            InitializeComponent();
            defUnitPhoto = new Bitmap(global::PersonalRU.Properties.Resources.No_photo);

            defaultUnitString = "{" + "LastName:;" + 
                                      "FirstName:;" + 
                                      "FatherName:;" + 
                                      "JobTitle:;" + 
                                      "ProductionSite:;" + 
                                      "Notes:Нет заметок;" +
                                      "IsPhotoTemporary:" + false.ToString() + ";" +
                                      "PhotoPath:none;" + 
                                      "TableDescription:;" + 
                                      "LID:1001;" + 
                                      "vID:;" + 
                                      "isVacExist:false;" + 
                                      "isVoid:false;" +
                                      "Wage:;"+
                                      "WageExt:;" +
                                      "ID:;:;}";

            delete_img = new Bitmap("img\\delete.png");
            makeMainProjectDirectory();
            this.CurrentProject = new Project();
            showDefaultUnitDesciption();
            Console.WriteLine("MainWindow costructoris loaded");
            //if (this.CurrentProject.IsExist) refreshVacancyJournalDATA();
        }
        private void makeMainProjectDirectory()
        {
            DirectoryInfo DInfo = new DirectoryInfo("Projects");
            if (!DInfo.Exists) { DInfo.Create();}
        }
        #region VACANCY JOURNAL
        #region MAIN

        SRL.SRL srl = new SRL.SRL(true);

        public int selected_row = 0;

        SRL.Vacancy defaultVacancy = new SRL.Vacancy();        
       
        private void refreshVacancyJournalDATA() {
            refreshVacList();
        }
        private void deleteVacancy(int rowIndex)
        {
            AskDeleteVacancyForm f = new AskDeleteVacancyForm("Удалить вакансию из базы?");
            f.ShowDialog();
            if (f.isDel)
            {
                DataGridViewCell cell = (DataGridViewCell)VacancyJournalDGV.Rows[rowIndex].Cells[0];
                SRL.Vacancy vac = new SRL.Vacancy();
                
                vac = this.CurrentProject.getVacancyByLID((int)cell.Value-1);
                if (vac.basedUnitID.Length > 0)
                {
                    Unit unit = CurrentProject.getUnitByID(vac.basedUnitID);
                    if (f.isDeleteUnit)
                    {
                        CurrentProject.deleteUnit(unit);
                    }
                    else
                    {
                        if (unit.IsVoid) unit.tableDescription = "Создать вакансию";
                        else unit.tableDescription = "Вакансия на замену"; ;
                        unit.vID = "";
                        unit.isVacExist = false;
                        string ustr = unit.toString_without_length();
                        unit.dispose();
                        CurrentProject.changeUnit(ustr);
                    }
                }
                CurrentProject.deleteVacancy(vac);
                RefreshAllData();
            }
        }
        private void refreshVacList()
        {
            VacancyJournalDGV.Rows.Clear();
            foreach (SRL.Vacancy vac in this.CurrentProject.getVacancyList())
            {
                int rowNumber = VacancyJournalDGV.Rows.Add();
                refreshVacResponsesList(rowNumber);
                VacancyJournalDGV.Rows[rowNumber].Cells["JobTitle"].Value = vac.JobTitle;
                VacancyJournalDGV.Rows[rowNumber].Cells["Responses"].Value = currentVacRList.Count;
                VacancyJournalDGV.Rows[rowNumber].Cells["WorkSet"].Value = vac.ProductionSite;
                VacancyJournalDGV.Rows[rowNumber].Cells["Updated"].Value = vac.UpdatedDate;
                VacancyJournalDGV.Rows[rowNumber].Cells["LID"].Value = vac.LID + 1;
                //VacancyJournalDGV.Rows[rowNumber].Cells["vIDs"].Value = "#" + vac.ID;
                if (vac.basedUnitID.Length > 0) VacancyJournalDGV.Rows[rowNumber].Cells["basedUID"].Value = "Посмотреть должность";
                else VacancyJournalDGV.Rows[rowNumber].Cells["basedUID"].Value = "";
                VacancyJournalDGV.Rows[rowNumber].Cells["basedUID"].Style.ForeColor = Color.FromArgb(70, 130, 180);
                VacancyJournalDGV.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }
        private void refreshVacData(SRL.Vacancy vac)
        {
            this.panel1.Controls.Clear();
            addSingleItem("Наименование должности", vac.JobTitle);
            addItemList("Требования к образованию", vac.EducationRequirements);
            addSingleItem("Требования к стажу работы", vac.WorkExperience);
            addItemList("Описание трудовой деятельности", vac.DescriptionOfWork);
            addSingleItem("Место работы и контакты", vac.Location);
            addItemList("Режим работы", vac.TimeTable);
            addSingleItem("Уровень зарплаты", vac.WageLevel);
            addItemList("Требования к знанию языка", vac.LanguageRequirements);
            addItemList("Требования к гражданству", vac.CitizenshipRequirements);
            addSingleItem("Подразделение", vac.ProductionSite);
        }
        int margin = 2;
        int Title_margin = 7;

        private void addSingleItem(string Title, string text)
        {
            addTitleLabel(Title);
            addContentLabel(text);
        }
        private void addItemList(string Title, SRL.ItemsList itList)
        {
            addTitleLabel(Title);
            foreach (SRL.Item item in itList.getList())
                addContentLabel(item.item_str);
        }
        private void addTitleLabel(string Title)
        {
            Label label = new Label();
            if (panel1.Controls.Count > 0) label.Location = new Point(15, this.panel1.Controls[panel1.Controls.Count - 1].Location.Y + this.panel1.Controls[panel1.Controls.Count - 1].Height + Title_margin);
            else label.Location = new Point(15, 5);
            label.Text = Title;
            label.MaximumSize = new Size(250, 0);
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.Controls.Add(label);
        }
        private void addContentLabel(string text)
        {
            Label label = new Label();
            if (panel1.Controls.Count > 0) label.Location = new Point(25, this.panel1.Controls[panel1.Controls.Count - 1].Location.Y + this.panel1.Controls[panel1.Controls.Count - 1].Height + margin);
            else label.Location = new Point(15, 5);
            label.Text = "   - " + text;
            label.MaximumSize = new Size(300, 0);
            label.AutoSize = true;
            label.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label.ForeColor = Color.DimGray;
            this.panel1.Controls.Add(label);
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            panel1.Focus();
        }
        private void showVacancyDescription(int LID){
            SRL.Vacancy vac = this.CurrentProject.getVacancyByLID(LID);
            refreshVacData(vac);
        }
        private void ChangeVacancy(int LID)
        {
            SRL.Vacancy CurrentVacancy = this.CurrentProject.getVacancyByLID(selected_row);
            VacancyForm vf = new VacancyForm(CurrentVacancy,this.srl, true);
            vf.ShowDialog();
            if (vf.isOKed)
            {
                SRL.Vacancy v = vf.get_vacancy();
                if (vf.isPublic){this.srl.updateVacancy(v);}
                this.CurrentProject.changeVacancy(v);
                refreshVacList();
            }
        }
        private void LockVacancy(int row_num)
        {
            AskForm af = new AskForm("Заблокировать вакансию?");
            af.ShowDialog();
            if (af.IsOKed)
            {
                int LID = (int)VacancyJournalDGV.Rows[row_num].Cells["LID"].Value - 1;
                SRL.Vacancy vac = this.CurrentProject.getVacancyByLID(LID);
                if (srl.lockVacancy(vac))
                {
                    MessageBox.Show("Вакансия заблокирована и убрана из списка просмотров вакансий");
                }
                else
                {
                    MessageBox.Show("Ошибка связи!");
                }
                refreshVacList();
            }
        }
        private void UpdateVacancy(int row_num)
        {
            AskForm af = new AskForm("Опубликовать/обновить вакансию на сайте?");
            af.ShowDialog();
            if (af.IsOKed)
            {
                SRL.Vacancy vac = this.CurrentProject.getVacancyByLID(row_num);
                string s_update = srl.updateVacancy(vac);
                if (s_update.Length > 0)
                {
                    MessageBox.Show("Вакансия обновлена!" + s_update);
                    vac.UpdatedDate = s_update;
                    this.CurrentProject.changeVacancy(vac);
                    refreshVacList();
                }
                else { MessageBox.Show("Ошибка связи!"); }
                refreshVacList();
            }
        }
        private void AddVacancy()
        {
            this.defaultVacancy = new SRL.Vacancy();
            VacancyForm vf = new VacancyForm(defaultVacancy, this.srl,false);
            vf.ShowDialog();
            if (vf.isOKed)
            {
                SRL.Vacancy v = vf.get_vacancy();
                if (vf.isPublic) srl.updateVacancy(v);
                this.CurrentProject.addVacancy(v);
                refreshVacList();
            }
            else { }
        }
        private void showVacancy(int LID)
        {            
            tabControl1.SelectedTab = vacancies_book_tabpage;
            //SRL.Vacancy vac = VacList.getVacancyByLID(LID);
            VacancyJournalDGV.Rows[LID].Selected = true;
            showVacancyDescription(LID);
            refreshVacResponsesList(LID);
        }
        private List<SRL.Response> currentVacRList;
        private void refreshVacResponsesList(int LID)
        {
            SRL.Vacancy vac = srl.getVacList().getVacancyByLID(LID);
            List<SRL.Response> RespList = srl.getResponsesList();
            currentVacRList = new List<SRL.Response>();

            foreach (SRL.Response resp in RespList)
                if (resp.ID == vac.ID) currentVacRList.Add(resp);

            VacResponsesList_DGV.Rows.Clear();
            foreach (SRL.Response resp in currentVacRList)
            {
                int row_num = VacResponsesList_DGV.Rows.Add();
                VacResponsesList_DGV.Rows[row_num].Cells["vDate"].Value = resp.datePublished;
                SRL.Recrut recrut = srl.getRecrutByID(resp.basedRecrutID);
                VacResponsesList_DGV.Rows[row_num].Cells["vRecrutName"].Value = recrut.LastName + " " + recrut.FirstName + " " + recrut.FatherName;
                VacResponsesList_DGV.Rows[row_num].Cells["Link"].Value = "Посмотреть";
            }
        }
        #endregion
        #region EVENTS
        private void VacResponsesList_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                SRL.Response resp = this.currentVacRList[e.RowIndex];
                int respLID = this.RList.IndexOf(resp);
                this.ResponsesList_DGV.Rows[respLID].Selected = true;
                this.currentResponse = this.RList[respLID];
                initResponse(this.currentResponse);
                this.tabControl1.SelectedIndex = 2;
            }
        }
        private void NewVac_butt_Click(object sender, EventArgs e)
        {
            AddVacancy();
        }
        private void VacancyJournalDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = (DataGridViewCell)VacancyJournalDGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
            DataGridViewRow row = (DataGridViewRow)VacancyJournalDGV.Rows[e.RowIndex];            
            selected_row = e.RowIndex;

            if (e.ColumnIndex == 7)
            {
                deleteVacancy(e.RowIndex);
            }
            if (e.ColumnIndex == 6)
            {
                LockVacancy(e.RowIndex);
            }
            if (e.ColumnIndex == 5)
            {
                UpdateVacancy(e.RowIndex);
            }
            
        }        
        private void VacancyJournalDGV_SelectionChanged(object sender, EventArgs e)
        {
            if (this.CurrentProject.getVacancyList().Count > 0)
            {
                if (VacancyJournalDGV.CurrentRow != null)
                {
                    if (VacancyJournalDGV.CurrentRow.Index < this.CurrentProject.getVacancyList().Count)
                    {
                        if (VacancyJournalDGV.CurrentRow != null)
                        {
                            DataGridViewCell cell = (DataGridViewCell)VacancyJournalDGV.Rows[VacancyJournalDGV.CurrentRow.Index].Cells["LID"];
                            if (cell.Value != null)
                            {
                                tabControl1.SelectedTab = vacancies_book_tabpage;
                                //SRL.Vacancy vac = VacList.getVacancyByLID(LID);
                                //VacancyJournalDGV.Rows[(int)cell.Value-1].Selected = true;
                                showVacancyDescription((int)cell.Value - 1);
                                refreshVacResponsesList((int)cell.Value - 1);
                            };
                        }
                    }
                }
            }
        }
        private void VacancyJournalDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                DataGridViewCell cell = (DataGridViewCell)VacancyJournalDGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value.ToString().Length > 0)
                {
                    SRL.Vacancy vac = this.CurrentProject.getVacancyByLID((int)(VacancyJournalDGV.Rows[e.RowIndex].Cells[0].Value) - 1);
                    Unit unit = this.CurrentProject.getUnitByID(vac.basedUnitID);
                    showUnit(unit.getLID());
                }
            }
        }
        private void VacancyJournalDGV_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5) this.toolTip1.Show("Обновить", this, Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
            if (e.ColumnIndex == 6) this.toolTip1.Show("Заблокировать", this, Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
            if (e.ColumnIndex == 7) this.toolTip1.Show("Удалить", this, Cursor.Position.X - this.Location.X, Cursor.Position.Y - this.Location.Y);
        }
        private void VacancyJournalDGV_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5) { this.toolTip1.Hide(this); }
            if (e.ColumnIndex == 6) { this.toolTip1.Hide(this); }
            if (e.ColumnIndex == 7) { this.toolTip1.Hide(this); }
        }
        private void ChancgeVacancy_butt_Click(object sender, EventArgs e)
        {
            ChangeVacancy(selected_row);
        }
        #endregion
        #endregion        
        #region PERSONNEL_DEPARTMENT
        #region MAIN
        private void addUnit()
        {
            
            UnitForm uf = new UnitForm(this.defaultUnitString, this.srl, true, this.CurrentProject.projectDirPath,makeDepartmetList(),makeJobTitleList());
            uf.ShowDialog();
            if (uf.isOked)
            {
                string unitString = uf.toString();
                this.CurrentProject.addUnit(unitString);
                refreshUnitList();                
            }
            else return;        
        }
        List<string> makeDepartmetList()
        {
            List<string> depList = new List<string>();
            foreach (Unit unit in this.CurrentProject.getUnitList())
                if (depList.Find(String => String == unit.ProductionSite) == null) depList.Add(unit.ProductionSite);
            return depList;
        }
        List<string> makeJobTitleList()
        {
            List<string> jtList = new List<string>();
            foreach (Unit unit in this.CurrentProject.getUnitList())
                if (jtList.Find(String => String == unit.jobTitle)==null) jtList.Add(unit.jobTitle);
            return jtList;
        }
        private void deleteUnit(int LID)
        {
            DeleteUnitForm duf = new DeleteUnitForm();
            duf.ShowDialog();
            if (duf.isOKed)
            {
                if (duf.isSaveVoidUnit) 
                {
                    Unit unit = this.CurrentProject.getUnitByLID(LID);
                    // Change for default photo
                    Bitmap def_unit_photo = new Bitmap(PersonalRU.Properties.Resources.No_photo, 300, 340);
                    string tempPhotoPath = this.CurrentProject.tempDirPath + "\\temp_avatar.png";
                    this.defUnitPhoto.Save(tempPhotoPath);
                    this.defUnitPhoto.Dispose();

                    // Delete all UnitFiles
                    unit.deleteAllUFiles();
                    string str = "{" + "LastName:Фамилия;" +
                                "FirstName:Имя;" +
                                "FatherName:Отчество;" +
                                "JobTitle:" + unit.jobTitle + ";" +
                                "ProductionSite:" + unit.ProductionSite + ";" +
                                "Notes:;" +
                                "IsPhotoTemporary:" + true.ToString() + ";" +
                                "PhotoPath:" + tempPhotoPath + ";" +
                                "TableDescription:" + unit.tableDescription + ";" +
                                "LID:" + unit.getLID() + ";" +
                                "vID:" + unit.vID + ";" +
                                "isVacExist:" + unit.isVacExist.ToString() + ";" +
                                "isVoid:" + true.ToString() + ";" +
                                "ID:" + unit.ID + ";" +
                                "UnitFileList:" + unit.UFList.toString() + ";" +
                                ":;}";
                    unit.dispose();
                    showDefaultUnitDesciption();
                    this.CurrentProject.changeUnit(str);
                    RefreshAllData();
                }
                else
                {
                    Unit unit = this.CurrentProject.getUnitByLID(LID);
                    if (unit.isVacExist)
                    {
                        SRL.Vacancy vac = this.CurrentProject.getVacancyByID(unit.vID);
                        this.CurrentProject.deleteVacancy(vac);
                    }
                    showDefaultUnitDesciption();
                    this.CurrentProject.deleteUnit(unit);
                    RefreshAllData();
                }
            }
        }
        private void addNewPost()
        {
            CreateVacancyMenu cvmf = new CreateVacancyMenu(this.CurrentProject.getUnitList());
            cvmf.ShowDialog();
            if (cvmf.isOKed)
            {
                if (!cvmf.Choose) { CreateNewVacAndPerson(); }
                else { createNewVacBasedPerson(cvmf.currentID); }
            }
        }
        private void changeUnit()
        {
            UnitForm uf = new UnitForm(currentUnit.toString_without_length(),this.srl,false, this.CurrentProject.projectDirPath,makeDepartmetList(),makeJobTitleList());
            
            uf.ShowDialog();
            if (uf.isOked)
            {
                string unitString = uf.toString();
                this.currentUnit.dispose();
                this.photo_pictureBox.Image.Dispose();
                this.bmp.Dispose();
                
                this.CurrentProject.changeUnit(unitString);
                refreshUnitList();
            }
            else { return; }
        }
        private void refreshUnitList()
        {
            PersonnnelDep_DGV.Rows.Clear();
            
            List<Unit> UnList = this.CurrentProject.getUnitList();
            Int32 SummaryWageRU = 0;
            Int32 SummaryWageUSD = 0;
            Int32 SummaryWageEUR = 0;
            foreach (Unit unit in UnList)
            {
                if (unit.wage_ext == "руб.") SummaryWageRU += Convert.ToInt32(unit.wage);
                else if (unit.wage_ext == "usd") SummaryWageUSD += Convert.ToInt32(unit.wage);
                else if (unit.wage_ext == "eur") SummaryWageEUR += Convert.ToInt32(unit.wage);
                string description_text = "";
                int rowNumber = PersonnnelDep_DGV.Rows.Add();

                PersonnnelDep_DGV.Rows[rowNumber].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (unit.IsVoid)
                {
                    PersonnnelDep_DGV.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.RoyalBlue;
                    if (unit.isVacExist){
                        PersonnnelDep_DGV.Rows[rowNumber].Cells["Description"].Value = "Есть вакансия";
                    }
                    else{PersonnnelDep_DGV.Rows[rowNumber].Cells["Description"].Value = "Создать вакансию"; }
                }
                else
                {
                    if (unit.isVacExist) { PersonnnelDep_DGV.Rows[rowNumber].Cells["Description"].Value = "Есть вакансия"; }
                    else { PersonnnelDep_DGV.Rows[rowNumber].Cells["Description"].Value = "Создать вакансию"; ; }
                }
                if ((unit.tableDescription != "Создать вакансию") &&(!unit.IsVoid))
                {
                    PersonnnelDep_DGV.Rows[rowNumber].DefaultCellStyle.ForeColor = Color.Orange;
                }
                PersonnnelDep_DGV.Rows[rowNumber].Cells["ProductionSite"].Value = unit.ProductionSite;
                PersonnnelDep_DGV.Rows[rowNumber].Cells["UnitJobTitle"].Value = unit.jobTitle;
                PersonnnelDep_DGV.Rows[rowNumber].Cells["UnitName"].Value = unit.lastName +" \n" + unit.firstName +" \n" + unit.fatherName;
                PersonnnelDep_DGV.Rows[rowNumber].Cells["DelButt"].Value = "Удалить";
                PersonnnelDep_DGV.Rows[rowNumber].Cells["UnitLID"].Value = unit.getLID() + 1;
                PersonnnelDep_DGV.Rows[rowNumber].Cells["wage"].Value = unit.wage + " " + unit.wage_ext;
                PersonnnelDep_DGV.Rows[rowNumber].Cells["wage"].Style.ForeColor = Color.IndianRed;
                //PersonnnelDep_DGV.Rows[rowNumber].Cells["IDs"].Value = unit.ID;
            }
            if (UnList.Count == 0)
            {
                showDefaultUnitDesciption();
                currentUnit = null;
            }
            else
            {
                PersonnnelDep_DGV.Rows[0].Selected = true;
            }
            if (SummaryWageRU > 0) SummaryWage_label.Text = SummaryWageRU.ToString() + " руб. \n";
            if (SummaryWageUSD > 0) SummaryWage_label.Text += SummaryWageUSD.ToString() + " USD \n";
            if (SummaryWageEUR > 0) SummaryWage_label.Text += SummaryWageUSD.ToString() + " EUR ";
        }
        Bitmap bmp;
        private void showUnitDescription(int LID) 
        {
            List<Unit> UList = this.CurrentProject.getUnitList();
            if (UList.Count != 0)
            {
                Unit unit = this.CurrentProject.getUnitByLID(LID);
                if (unit.IsVoid){
                    if (unit.isVacExist) table_description_label.Text = unit.tableDescription;
                    else table_description_label.Text = "Создать вакансию"; }
                else{
                    if (unit.isVacExist)  table_description_label.Text = unit.tableDescription; 
                    else  table_description_label.Text = unit.tableDescription; }

                this.LAST_NAME_LABEL.Text = unit.lastName;
                this.FIRST_NAME_LABEL.Text = unit.firstName;
                this.FATHER_NAME_LABEL.Text = unit.fatherName;
                this.JOBTITLE_LABEL.Text = unit.jobTitle;
                this.PRODUCTIONSITE_LABEL.Text = unit.ProductionSite;
                this.NOTES_RichTextbox.Text = unit.Notes;
                bmp = new Bitmap(unit.photo, photo_pictureBox.Size);
                this.photo_pictureBox.Image = bmp;                
                this.wage_label.Text = unit.wage + " " + unit.wage_ext;
                refreshUnitFileList(unit);
            }
        }
        private void refreshUnitFileList(Unit unit)
        {
            List<UnitFile> UFList = unit.getUFList();
            FilesDGV.Rows.Clear();
            foreach (UnitFile ufile in UFList)
            {
                int rowCount = this.FilesDGV.Rows.Add();
                this.FilesDGV.Rows[rowCount].Cells["ufLID"].Value = ufile.getLID()+1;
                this.FilesDGV.Rows[rowCount].Cells["FName"].Value = ufile.FileName.Substring(1,ufile.FileName.Length-1);

                FileInfo finfo = new FileInfo(ufile.fullPath);
                if (finfo.Extension == ".doc") this.FilesDGV.Rows[rowCount].Cells["img"].Value = PersonalRU.Properties.Resources.doc_img;
                else if (finfo.Extension == ".docx") this.FilesDGV.Rows[rowCount].Cells["img"].Value = PersonalRU.Properties.Resources.doc_img;
                else if (finfo.Extension == ".xls") this.FilesDGV.Rows[rowCount].Cells["img"].Value = PersonalRU.Properties.Resources.xls_img;
                else if (finfo.Extension == ".xlsx") this.FilesDGV.Rows[rowCount].Cells["img"].Value = PersonalRU.Properties.Resources.xls_img;
                else { this.FilesDGV.Rows[rowCount].Cells["img"].Value = PersonalRU.Properties.Resources.def__file_img; }
            }
        }
        private void showUnit(int LID)
        {
            tabControl1.SelectedTab = Personal_tabPage;
            PersonnnelDep_DGV.Rows[LID].Selected = true;
            showUnitDescription(LID);
        }
        private void showDefaultUnitDesciption()
        {
            this.LAST_NAME_LABEL.Text = "Фамилия";
            this.FIRST_NAME_LABEL.Text = "Имя";
            this.FATHER_NAME_LABEL.Text = "Отчество";
            this.JOBTITLE_LABEL.Text = "Должность";
            this.PRODUCTIONSITE_LABEL.Text = "Участок производства";
            this.NOTES_RichTextbox.Text = "Заметки";
            Bitmap bmp = new Bitmap(PersonalRU.Properties.Resources.No_photo, photo_pictureBox.Size);
            this.photo_pictureBox.Image = bmp;
            this.table_description_label.Text = "";
        }
        private void addUFile(string sourceFilePath)
        {
            //currentUnit.AddFile(dirPath + fname, CurrentProject.getDirPath() + "\\data\\", fname);
                this.CurrentProject.addUnitFile(sourceFilePath, this.currentUnit);
                this.currentUnit = this.CurrentProject.getUnitByLID(this.currentUnit.getLID());
                showUnitDescription(currentUnit.getLID());
        }
        private void deleteUfile(int LID)
        {
            
            UnitFile ufile = CurrentProject.getUnitFileByLID(this.currentUnit, LID);
            this.CurrentProject.deleteUnitFile(ufile, this.currentUnit);
            this.currentUnit = this.CurrentProject.getUnitByLID(currentUnit.getLID());
            showUnitDescription(currentUnit.getLID());
        }
        private void openUFile(UnitFile ufile)
        {
            Process.Start(ufile.fullPath);
        }
        #endregion 
        #region EVENTS
        private void PersonnnelDep_DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                DataGridViewCell Description_cell = (DataGridViewCell)PersonnnelDep_DGV.Rows[e.RowIndex].Cells["Description"];
                DataGridViewCell cell_ = (DataGridViewCell)PersonnnelDep_DGV.Rows[e.RowIndex].Cells["UnitLID"];
                    Unit unit_ = this.CurrentProject.getUnitByLID((int)cell_.Value - 1);
                if ((Description_cell.Value.ToString() == "Создать вакансию")&(unit_.IsVoid))
                {
                    DataGridViewCell cell = (DataGridViewCell)PersonnnelDep_DGV.Rows[e.RowIndex].Cells["UnitLID"];
                    Unit unit = this.CurrentProject.getUnitByLID((int)cell.Value - 1);

                    SRL.Vacancy new_vac = defaultVacancy;
                    new_vac.JobTitle = unit.jobTitle;
                    new_vac.ProductionSite = unit.ProductionSite;
                    new_vac.basedUnitID = unit.ID;

                    VacancyForm vf = new VacancyForm(new_vac, this.srl, false);
                    vf.ShowDialog();
                    if (vf.isOKed)
                    {
                        SRL.Vacancy vac = vf.get_vacancy();
                        if (vf.isPublic) srl.updateVacancy(vac);
                        unit.isVacExist = true;
                        unit.vID = vac.ID;
                        unit.tableDescription = "Создана вакансия #" + vac.ID;

                        string ustr = unit.toString_without_length();
                        unit.dispose();
                        this.CurrentProject.changeUnit(ustr);
                        this.CurrentProject.addVacancy(vac);
                        RefreshAllData();
                        vac = CurrentProject.getVacancyByID(vac.ID);
                        showVacancy(vac.LID);
                    }
                }
                else if ((Description_cell.Value.ToString() == "Создать вакансию")&&(!unit_.IsVoid))
                {
                    DataGridViewCell cell = (DataGridViewCell)PersonnnelDep_DGV.Rows[e.RowIndex].Cells["UnitLID"];
                    Unit unit = this.CurrentProject.getUnitByLID((int)cell.Value - 1);

                    SRL.Vacancy new_vac = defaultVacancy;
                    new_vac.JobTitle = unit.jobTitle;
                    new_vac.ProductionSite = unit.ProductionSite;
                    new_vac.basedUnitID = unit.ID;

                    VacancyForm vf = new VacancyForm(new_vac, this.srl, false);
                    vf.ShowDialog();
                    if (vf.isOKed)
                    {
                        SRL.Vacancy vac = vf.get_vacancy();
                        if (vf.isPublic) srl.updateVacancy(vac);
                        unit.isVacExist = true;
                        unit.vID = vac.ID;
                        unit.tableDescription = "Вакансия на замену #" + vac.ID;
                        string ustr = unit.toString_without_length();
                        unit.dispose();
                        this.CurrentProject.changeUnit(ustr);
                        this.CurrentProject.addVacancy(vac);
                        RefreshAllData();
                        vac = CurrentProject.getVacancyByID(vac.ID);
                        showVacancy(vac.LID);
                        //VacancyJournalDGV.Rows[vac.LID].Selected = true;
                    }
                }
                else
                {
                    DataGridViewCell cell = (DataGridViewCell)PersonnnelDep_DGV.Rows[e.RowIndex].Cells["UnitLID"];
                    Unit unit = this.CurrentProject.getUnitByLID((int)cell.Value - 1);
                    if (unit.isVacExist)
                    {
                        SRL.Vacancy vac = this.CurrentProject.getVacancyByID(unit.vID);
                        showVacancy(vac.LID);
                    }
                }
            }
            else if (e.ColumnIndex == 1)
            {
                DepartmentNameForm dpnf = new DepartmentNameForm(currentUnit.ProductionSite);
                dpnf.ShowDialog();
                if (dpnf.isOKed)
                {
                    string old_production_site = currentUnit.ProductionSite;
                    foreach (Unit unit in CurrentProject.getUnitList())
                    {
                        if (unit.ProductionSite == old_production_site)
                        {
                            unit.ProductionSite = dpnf.ProductionSite;
                            //CurrentProject.changeUnit(unit.toString_without_length());
                            if (unit.isVacExist)
                            {
                                SRL.Vacancy vac = CurrentProject.getVacancyByID(unit.vID);
                                vac.ProductionSite = unit.ProductionSite;
                                CurrentProject.changeVacancy(vac);
                            }
                        }
                    }
                    RefreshAllData();
                }
            }
        }
        private void table_description_label_Click(object sender, EventArgs e)
        {
            if (table_description_label.Text == "Создать вакансию")
            {
                SRL.Vacancy new_vac = defaultVacancy;
                new_vac.JobTitle = this.currentUnit.jobTitle;
                new_vac.ProductionSite = this.currentUnit.ProductionSite;
                new_vac.basedUnitID = this.currentUnit.ID;

                VacancyForm vf = new VacancyForm(new_vac, this.srl, false);
                vf.ShowDialog();
                if (vf.isOKed)
                {
                    SRL.Vacancy vac = vf.get_vacancy();
                    if (vf.isPublic) srl.updateVacancy(vac);
                    this.currentUnit.isVacExist = true;
                    this.currentUnit.vID = vac.ID;
                    this.currentUnit.tableDescription = "Создана вакансия #" + vac.ID;

                    string ustr = this.currentUnit.toString_without_length();
                    this.currentUnit.dispose();
                    this.CurrentProject.changeUnit(ustr);
                    this.CurrentProject.addVacancy(vac);
                    RefreshAllData();
                    vac = CurrentProject.getVacancyByID(vac.ID);
                    showVacancy(vac.LID);
                }
            }
            else if (table_description_label.Text == "Вакансия на замену")
            {

                SRL.Vacancy new_vac = defaultVacancy;
                new_vac.JobTitle = this.currentUnit.jobTitle;
                new_vac.ProductionSite = this.currentUnit.ProductionSite;
                new_vac.basedUnitID = this.currentUnit.ID;

                VacancyForm vf = new VacancyForm(new_vac, this.srl, false);
                vf.ShowDialog();
                if (vf.isOKed)
                {
                    SRL.Vacancy vac = vf.get_vacancy();
                    if (vf.isPublic) srl.updateVacancy(vac);
                    this.currentUnit.isVacExist = true;
                    this.currentUnit.vID = vac.ID;
                    this.currentUnit.tableDescription = "Вакансия на замену #" + vac.ID;
                    string ustr = this.currentUnit.toString_without_length();
                    this.currentUnit.dispose();
                    this.CurrentProject.changeUnit(ustr);
                    this.CurrentProject.addVacancy(vac);
                    RefreshAllData();
                    vac = CurrentProject.getVacancyByID(vac.ID);
                    showVacancy(vac.LID);
                }
            }
            else
            {
                if (this.currentUnit.isVacExist)
                {
                    SRL.Vacancy vac = this.CurrentProject.getVacancyByID(this.currentUnit.vID);
                    showVacancy(vac.LID);
                }
            }
        }
        private void addUnit_picbox_MouseHover(object sender, EventArgs e){
            addUnit_picbox.Image = global::PersonalRU.Properties.Resources.add_unit_hover;
            Cursor = Cursors.Hand;
        }
        private void addUnit_picbox_MouseLeave(object sender, EventArgs e){
            addUnit_picbox.Image = global::PersonalRU.Properties.Resources.add_unit_def;
            Cursor = Cursors.Default;
        }
        private void CreateVacancyPersonnel_butt_Click(object sender, EventArgs e)
        {
        }
        private void PersonnnelDep_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = (DataGridViewRow)PersonnnelDep_DGV.Rows[e.RowIndex];
            DataGridViewCell cell = (DataGridViewCell)PersonnnelDep_DGV.Rows[e.RowIndex].Cells[e.ColumnIndex];
            row.Selected = true;

            if (cell.Value == "Удалить"){ deleteUnit((int)PersonnnelDep_DGV.Rows[e.RowIndex].Cells[0].Value-1);}
        }
        private void PersonnnelDep_DGV_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = (DataGridViewCell)PersonnnelDep_DGV.Rows[PersonnnelDep_DGV.CurrentRow.Index].Cells["UnitLID"];
            List<Unit> UList = CurrentProject.getUnitList();
            if (cell.Value != null  && (int)cell.Value <= UList.Count) 
            {
              showUnitDescription((int)cell.Value-1);
              this.currentUnit = this.CurrentProject.getUnitByLID((int)cell.Value-1);
            }
        }
        private void ExchangeVacButt_Click(object sender, EventArgs e){//createNewVacancyForEchangeUnit();
        }
        private void addFileButt_Click(object sender, EventArgs e)
        {
            if (currentUnit != null)
                addUFile_openFileDialog.ShowDialog();
        }
        private void addUFile_openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string sourceFilePath = addUFile_openFileDialog.FileName;
            addUFile(sourceFilePath);
        }
        private void DeleteFileButt_Click(object sender, EventArgs e)
        {
            AskForm af = new AskForm("Вы действительно хотите удалить файл из досье сотрудника?");
            af.ShowDialog();
            if (af.IsOKed)
            {
                if (FilesDGV.CurrentRow != null)
                {
                    int LID = (int)FilesDGV.CurrentRow.Cells["ufLID"].Value-1;
                    deleteUfile(LID); 
                }
            }
        }
        private void FilesDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int LID = FilesDGV.CurrentRow.Index;
            UnitFile uFile = this.CurrentProject.getUnitFileByLID(this.currentUnit, LID);
            this.openUFile(uFile);
        }
        private void openFileButt_Click(object sender, EventArgs e)
        {
            if (FilesDGV.CurrentRow != null)
            {
                int LID = FilesDGV.CurrentRow.Index;
                UnitFile uFile = this.CurrentProject.getUnitFileByLID(this.currentUnit, LID);
                if (uFile != null) this.openUFile(uFile);
            }
        }
        private void PersonnnelDep_DGV_DoubleClick(object sender, EventArgs e)
        {
            PersonnnelDep_DGV.CurrentRow.Selected = true;
        }
        #endregion
        #region PHOTO
        private void photo_pictureBox_MouseEnter(object sender, EventArgs e)
        {
            VSBitmap photo = new VSBitmap((Bitmap)photo_pictureBox.Image);
            this.photo_pictureBox.Image = photo.overLay(global::PersonalRU.Properties.Resources.changePhotoBar);
            Cursor = Cursors.Hand;
        }
        private void photo_pictureBox_MouseLeave(object sender, EventArgs e)
        {
            Bitmap bmp;
            if (currentUnit!=null) bmp = new Bitmap(currentUnit.getPhoto(), photo_pictureBox.Size);
            else bmp = new Bitmap(PersonalRU.Properties.Resources.No_photo, photo_pictureBox.Size);
            this.photo_pictureBox.Image = bmp;

            Cursor = Cursors.Default;
        }
        private void photo_pictureBox_Click(object sender, EventArgs e)
        {
            PhotoDesignForm pdf = new PhotoDesignForm(currentUnit.photo, true,  currentUnit.PhotoFPath);
            pdf.ShowDialog();
            if (pdf.isOKed)
            {
                Bitmap new_photo = new Bitmap(pdf.getPhoto());
                
                string tempPhotoPath = this.CurrentProject.tempDirPath + "\\temp_avatar.png";
                new_photo.Save(tempPhotoPath);
                new_photo.Dispose();
                currentUnit.setNewPhoto(tempPhotoPath);
                RefreshAllData();
            }
        }
        #endregion
        #endregion
        #region RESPONSES
        #region MAIN
        private void InitResponsesList()
        {
            this.RList = new List<SRL.Response>();
            List<SRL.Response> srlResponsesList = srl.getResponsesList();
            foreach (SRL.Vacancy vac in this.CurrentProject.getVacancyList())
                foreach (SRL.Response resp in srlResponsesList)
                    if (resp.ID == vac.ID)
                        this.RList.Add(resp);
            if (this.RList.Count > 0) {
                currentResponse = RList[0];
                initResponse(currentResponse);
            }
        }
        private void refreshResponsesList()
        {
            ResponsesList_DGV.Rows.Clear();
            foreach (SRL.Response resp in this.RList)
            {
                int row_num = this.ResponsesList_DGV.Rows.Add();
                ResponsesList_DGV.Rows[row_num].Cells["RespLID"].Value = this.RList.IndexOf(resp);
                ResponsesList_DGV.Rows[row_num].Cells["ResponsesDatePublished"].Value = resp.datePublished + " " + resp.timePublished;
                ResponsesList_DGV.Rows[row_num].Cells["ResponseJobTitle"].Value = resp.JobTitle;
                SRL.Vacancy vac = CurrentProject.getVacancyByID(resp.ID);
                ResponsesList_DGV.Rows[row_num].Cells["ResponseProductionSite"].Value = vac.ProductionSite;
                SRL.Recrut recrut = srl.getRecrutByID(resp.basedRecrutID);
                ResponsesList_DGV.Rows[row_num].Cells["RecrutName"].Value = recrut.LastName + " " + recrut.FirstName + " " + recrut.FatherName;
            }
        }
        private void initResponse(SRL.Response v)
        {
            this.JobTitleLabel.Text = "\"" + v.JobTitle + "\"";
            SRL.Vacancy vac = this.CurrentProject.getVacancyByID(v.ID);
            this.ProductionSiteLabel.Text = "\"" + vac.ProductionSite+ "\"";
            int margin = 5;
            Point new_l17_location = new Point(JobTitleLabel.Location.X + JobTitleLabel.Width + margin,label17.Location.Y);
            this.label17.Location = new_l17_location;
            Point newPSLabelLocation = new Point(new_l17_location.X + label17.Width + margin, ProductionSiteLabel.Location.Y);
            this.ProductionSiteLabel.Location = newPSLabelLocation;

            Bitmap recrutPhoto = new Bitmap(PersonalRU.Properties.Resources.No_photo,RecrutPhoto_PictureBox.Size);
            RecrutPhoto_PictureBox.Image = recrutPhoto;
            
            SRL.Recrut rec = srl.getRecrutByID(v.basedRecrutID);
            this.recrutFIO_label.Text = rec.LastName + " " + rec.FirstName + " " + rec.FatherName;

            this.e_mail_label.Text = rec.e_mail;
            this.telephone_label.Text = rec.telephone;

            this.dateLabel.Text = v.datePublished;
            this.time_label.Text = v.timePublished;

            refreshResponse(v);
        }
        private void refreshResponse(SRL.Response resp)
        {
            this.ResponsePanel.Controls.Clear();

            showSingleRespBlock("Наименование должности", resp.JobTitle, resp.isJobTitleOKed, resp.JobTitle_comment);
            showListRespBlock("Требования к образованию", resp.EducationRequirements);
            showSingleRespBlock("Требования к стажу работы", resp.WorkExperience, resp.isWorkExperienceOKed, resp.WorkExperience);
            showListRespBlock("Описание трудовой деятельности", resp.DescriptionOfWork);
            showSingleRespBlock("Место работы", resp.Location, resp.isLocationOKed, resp.Location_comment);
            showListRespBlock("Режим работы", resp.TimeTable);
            showSingleRespBlock("Уровень зарплаты", resp.WageLevel, resp.isWageLevelOKed, resp.WageLevel_comment);
            showListRespBlock("Требования к знанию языка", resp.LanguageRequirements);
            showListRespBlock("Требования к гражданству", resp.CitizenshipRequirements);
            showSingleRespBlock("Подразделение", resp.ProductionSite, resp.isProductionSiteOKed, resp.ProductionSite_comment);
        }
        int lastMaxHeight = 0;
        Point lastRowlLocation;
        int resp_margin = 5;
        private void showSingleRespBlock(string title, string content, bool isOked, string comment)
        {
            showTitle(title);
            showItem(content, isOked, comment);
        }
        private void showListRespBlock(string title, SRL.ItemsList itemList)
        {
            showTitle(title);
            foreach (SRL.Item item in itemList.getList())
                showItem(item.item_str, item.isOKed, item.comment);
        }
        private void showTitle(string title)
        {
            Label label = new Label();
            if (ResponsePanel.Controls.Count > 0)
            {
                label.Location = new Point(5, lastRowlLocation.Y + lastMaxHeight);
            }
            else label.Location = new Point(25, 25);
            label.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Underline | System.Drawing.FontStyle.Bold))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            label.MaximumSize = new Size(250, 0);
            label.Text = title;
            label.AutoSize = true;
            this.ResponsePanel.Controls.Add(label);
            this.lastMaxHeight = label.Height;
            this.lastRowlLocation = label.Location;
        }
        private void showItem(string content, bool isOked, string comment)
        {
            Label contentLabel = new Label();
            PictureBox statePBox = new PictureBox();
            Label commentLabel = new Label();

            contentLabel.Location = new Point(45, lastRowlLocation.Y + lastMaxHeight + resp_margin);
            statePBox.Location = new Point(contentLabel.Location.X + 250 + resp_margin,contentLabel.Location.Y);
            commentLabel.Location = new Point(statePBox.Location.X + 50, statePBox.Location.Y);

            contentLabel.MaximumSize = new Size(250, 0);
            contentLabel.Text = content;
            contentLabel.Font = this.ResponsePanel.Font;
            contentLabel.AutoSize = true;

            if (isOked) statePBox.Image = PersonalRU.Properties.Resources.ok_icon;
            else statePBox.Image = PersonalRU.Properties.Resources.no_icon;
            statePBox.Size = statePBox.Image.Size;

            commentLabel.MaximumSize = new Size(250, 0);
            commentLabel.Text = comment;
            commentLabel.Font = this.ResponsePanel.Font;
            commentLabel.AutoSize = true;

            this.ResponsePanel.Controls.Add(contentLabel);
            this.ResponsePanel.Controls.Add(statePBox);
            this.ResponsePanel.Controls.Add(commentLabel);

            lastMaxHeight = contentLabel.Height;
            if (statePBox.Height > lastMaxHeight) lastMaxHeight = statePBox.Height;
            if (commentLabel.Height > lastMaxHeight) lastMaxHeight = commentLabel.Height;
            lastRowlLocation = contentLabel.Location;
        }
        #endregion
        #region EVENTS
        private void ResponsesList_DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.currentResponse = this.RList[e.RowIndex];
            initResponse(this.currentResponse);
        }
        private void deleteResponse_butt_Click(object sender, EventArgs e)
        {
            AskForm af = new AskForm("Удалить отклик?");
            af.ShowDialog();
            if (af.IsOKed) {
                this.srl.deleteResponse(currentResponse.RespToString());
                RefreshAllData();
                InitResponsesList();
                refreshResponsesList();
                showVacancy(this.CurrentProject.getVacancyByID(currentResponse.ID).LID);
            }
            else { return; }
        }
        private void JobTitleLabel_Click(object sender, EventArgs e){showVacancy(this.CurrentProject.getVacancyByID(currentResponse.ID).LID);}
        private void sentMailButt_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:" + srl.getRecrutByID(currentResponse.basedRecrutID).e_mail);
        }
        #endregion
        #endregion
        #region FILE
        private string make_single_data_string(int begin, string file_string, char separator)
        {
            string str = "";
            int end = get_end_of_single_string(begin, file_string, separator);
            if (end - begin > 0) return str = file_string.Substring(begin, end - begin);
            else return "";
        }
        private int get_end_of_single_string(int begin, string file_string, char separator)
        {
            char char_ = ' ';
            while (char_ != separator)
            {
                char_ = file_string.ElementAt(begin);                
                begin++;
            }
            return begin - 1;
        }
        #endregion
        #region COMMON
        private void CreateNewVacAndPerson()
        {
            NewPostForm npf = new NewPostForm();
            npf.ShowDialog();
            if (npf.isOKed){
                if (npf.isCreateVacancy){
                    SRL.Vacancy new_vac = this.defaultVacancy;
                    new_vac.JobTitle = npf.JobTitle;
                    new_vac.ProductionSite = npf.Department;
                    VacancyForm vf = new VacancyForm(new_vac,srl,false);
                    vf.ShowDialog();

                    if (vf.isOKed){
                        SRL.Vacancy vac = vf.get_vacancy();
                        if (vf.isPublic) srl.updateVacancy(vac);
                        string unitID = this.srl.CreateID();
                        Bitmap photo = PersonalRU.Properties.Resources.No_photo;
                        string tempDirPath = this.CurrentProject.projectDirPath + "\\temp";
                        string tempPhotoPath = tempDirPath + "//temp_avatar.png";
                        photo.Save(tempPhotoPath);
                        string unit_string = "{" + "LastName:;" +
                                       "FirstName:;" + 
                                       "FatherName:;" +
                                       "JobTitle:" + vac.JobTitle + ";" +
                                       "ProductionSite:" + vac.ProductionSite + ";" +
                                       "Notes:нет заметок;" +
                                       "IsPhotoTemporary:" + true.ToString() + ";" +
                                       "PhotoPath:" + tempPhotoPath + ";" +
                                       "TableDescription:Создана вакансия #" + vac.ID + ";" +
                                       "LID:1001;" +
                                       "vID:" + vac.ID + ";" +
                                       "isVacExist:" + true.ToString() + ";" +
                                       "isVoid:" + true.ToString() + ";" +
                                       "UnitFileList:" + "0;4;{:;}" + ";" +
                                       "ID:" + unitID + ";" +
                                       "Wage:;" +
                                       "WageExt:;" +
                                       ":;}";
                        
                        vac.basedUnitID = unitID;
                        this.CurrentProject.addUnit(unit_string);
                        this.CurrentProject.addVacancy(vac);
                        RefreshAllData();
                        vac = this.CurrentProject.getVacancyByID(vac.ID);
                        showVacancy(vac.LID);
                    }
                }
                else
                {
                    string unitID = this.srl.CreateID();
                    Bitmap photo = PersonalRU.Properties.Resources.No_photo;
                    string tempDirPath = this.CurrentProject.projectDirPath + "\\temp";
                    string tempPhotoPath = tempDirPath + "//temp_avatar.png";
                    photo.Save(tempPhotoPath);
                    string unit_string = "{" + "LastName: ;" +
                                   "FirstName: ;" +
                                   "FatherName: ;" +
                                   "JobTitle:" + npf.JobTitle + ";" +
                                   "ProductionSite:" + npf.Department + ";" +
                                   "Notes:нет заметок;" +
                                   "IsPhotoTemporary:" + true.ToString() + ";" +
                                   "PhotoPath:" + tempPhotoPath + ";" +
                                   "TableDescription:" + ";" +
                                   "LID:1001;" +
                                   "vID:" + ";" +
                                   "isVacExist:" + false.ToString() + ";" +
                                   "isVoid:" + true.ToString() + ";" +
                                   "UnitFileList:" + "0;4;{:;}" + ";" +
                                   "ID:" + unitID + ";"+
                                   "Wage:;" +
                                   "WageExt:;" +
                                   ":;}";

                    this.CurrentProject.addUnit(unit_string);
                    RefreshAllData();
                }
            }
            else{return;}
        }
        private void createNewVacBasedPerson(string UnitID)
        {
            Unit unit = this.CurrentProject.getUnitByID(UnitID);
            SRL.Vacancy new_Vac = this.defaultVacancy;
            new_Vac.JobTitle = unit.jobTitle;
            new_Vac.ProductionSite = unit.ProductionSite;

            AskForm af = new AskForm("Создать вакансию для поиска кандидатов на новую должность?");
            af.ShowDialog();
            if (af.IsOKed) 
            {
                VacancyForm vf = new VacancyForm(new_Vac, srl, false);
                vf.ShowDialog();
                if (vf.isOKed)
                {
                    SRL.Vacancy vac = vf.get_vacancy();
                    if (vf.isPublic) srl.updateVacancy(vac);
                    vac.JobTitle = unit.jobTitle;
                    vac.ProductionSite = unit.ProductionSite;

                    string unitID = this.srl.CreateID();
                    Bitmap photo = PersonalRU.Properties.Resources.No_photo;
                    string tempDirPath = this.CurrentProject.projectDirPath + "\\temp";
                    string tempPhotoPath = tempDirPath + "//temp_avatar.png";
                    photo.Save(tempPhotoPath);
                    string unit_string = "{" + "LastName: Наймов;" +
                                   "FirstName: Найм;" +
                                   "FatherName: Иванович;" +
                                   "JobTitle:" + vac.JobTitle + ";" +
                                   "ProductionSite:" + vac.ProductionSite + ";" +
                                   "Notes:нет заметок;" +
                                   "IsPhotoTemporary:" + true.ToString() + ";" +
                                   "PhotoPath:" + tempPhotoPath + ";" +
                                   "TableDescription:Создана вакансия #" + vac.ID + ";" +
                                   "LID:1001;" +
                                   "vID:" + vac.ID + ";" +
                                   "isVacExist:" + true.ToString() + ";" +
                                   "isVoid:" + true.ToString() + ";" +
                                   "UnitFileList:" + "0;4;{:;}" + ";" +
                                   "ID:" + unitID + ";"+
                                   "Wage:;" +
                                   "WageExt:;" + 
                                   ":;}";
                    
                    vac.basedUnitID = unitID;

                    this.CurrentProject.addUnit(unit_string);
                    this.CurrentProject.addVacancy(vac);
                    RefreshAllData();
                    vac = this.CurrentProject.getVacancyByID(vac.ID);
                    showVacancy(vac.LID);
                }
                else
                {
                    string unitID = this.srl.CreateID();
                    Bitmap photo = PersonalRU.Properties.Resources.No_photo;
                    string tempDirPath = this.CurrentProject.projectDirPath + "\\temp";
                    string tempPhotoPath = tempDirPath + "//temp_avatar.png";
                    photo.Save(tempPhotoPath);
                    string unit_string = "{" + "LastName:;" +
                                   "FirstName:;" +
                                   "FatherName:;" +
                                   "JobTitle:" + unit.jobTitle + ";" +
                                   "ProductionSite:" + unit.ProductionSite + ";" +
                                   "Notes:нет заметок;" +
                                   "IsPhotoTemporary:" + true.ToString() + ";" +
                                   "PhotoPath:" + tempPhotoPath + ";" +
                                   "TableDescription:Создать вакансию;" +
                                   "LID:1001;" +
                                   "vID:;" +
                                   "isVacExist:" + false.ToString() + ";" +
                                   "isVoid:" + true.ToString() + ";" +
                                   "UnitFileList:" + "0;4;{:;}" + ";"  +
                                   "Wage:" + unit.wage + ";" +
                                   "WageExt:" + unit.wage_ext + ";" +
                                   "ID:" + unitID + ";:;}";
                    this.CurrentProject.addUnit(unit_string);
                    RefreshAllData();
                }
            }
            else
            {
                string unitID = this.srl.CreateID();
                Bitmap photo = PersonalRU.Properties.Resources.No_photo;
                string tempDirPath = this.CurrentProject.projectDirPath + "\\temp";
                string tempPhotoPath = tempDirPath + "//temp_avatar.png";
                photo.Save(tempPhotoPath);
                string unit_string = "{" + "LastName:;" +
                               "FirstName:;" +
                               "FatherName:;" +
                               "JobTitle:" + unit.jobTitle + ";" +
                               "ProductionSite:" + unit.ProductionSite + ";" +
                               "Notes:нет заметок;" +
                               "IsPhotoTemporary:" + true.ToString() + ";" +
                               "PhotoPath:" + tempPhotoPath + ";" +
                               "TableDescription:" + ";" +
                               "LID:1001;" +
                               "vID:" + ";" +
                               "isVacExist:" + false.ToString() + ";" +
                               "isVoid:" + true.ToString() + ";" +
                               "UnitFileList:" + "0;4;{:;}" + ";" +
                               "ID:" + unitID + ";" + 
                               "Wage:" + unit.wage + ";" +
                               "WageExt:" + unit.wage_ext + ";" +
                               ":;}";

                this.CurrentProject.addUnit(unit_string);
                RefreshAllData();
            }
        }
        private void RefreshAllData()
        {
            this.Text = this.CurrentProject.projectName + " - PersonalRU";
            refreshUnitList();
            if (this.currentUnit != null) refreshUnitFileList(currentUnit);
            else FilesDGV.Rows.Clear();
            
            refreshVacList();
            refreshVacancyJournalDATA();
            refreshResponsesList();
            
            //refreshVacancyJournalDATA();
        }
        public void Exit(FormClosingEventArgs e)
        {
            if (this.CurrentProject.IsExist)
            {
                AskExitForm aef = new AskExitForm(this.CurrentProject.projectName);
                aef.ShowDialog();
                if (!aef.isCanceled)
                {
                    if (aef.isOKed)
                    {
                        if (currentUnit != null) currentUnit.dispose();
                        showDefaultUnitDesciption();
                        if (this.CurrentProject.IsExist)
                        {
                            this.CurrentProject.saveProject();
                            this.CurrentProject.kill();
                        }
                        CurrentProject.IsExist = false;
                        this.Close();
                        Application.Exit();
                    }
                    else
                    {
                        if (currentUnit != null) currentUnit.dispose();
                        showDefaultUnitDesciption();
                        if (this.CurrentProject.IsExist) this.CurrentProject.kill();
                        CurrentProject.IsExist = false;
                        this.Close();
                        Application.Exit();
                    }
                }
                else { e.Cancel = true; return; }
            }
            else Application.Exit();
        }
        public void Exit()
        {
            if (this.CurrentProject.IsExist)
            {
                AskExitForm aef = new AskExitForm(this.CurrentProject.projectName);
                aef.ShowDialog();
                if (!aef.isCanceled)
                {
                    if (aef.isOKed)
                    {
                        if (currentUnit != null) currentUnit.dispose();
                        showDefaultUnitDesciption();
                        if (this.CurrentProject.IsExist)
                        {
                            this.CurrentProject.saveProject();
                            this.CurrentProject.kill();
                        }
                        CurrentProject.IsExist = false;
                        Application.Exit();
                    }
                    else
                    {
                        if (currentUnit != null) currentUnit.dispose();
                        showDefaultUnitDesciption();
                        if (this.CurrentProject.IsExist) this.CurrentProject.kill();
                        CurrentProject.IsExist = false;
                        Application.Exit();
                    }
                }
                else {  return; }
            }
        }
        public void InitNewProj()
        {
            NewProjectForm npf = new NewProjectForm(defaultProjectsDirPath, ref this.CurrentProject);
            npf.ShowDialog();
            if (npf.ISOked)
            {
                this.CurrentProject = new Project(npf.ProjectPath +  npf.ProjectName, true);
                //this.RList = this.srl.getResponsesList();
                InitResponsesList();
                RefreshAllData();
            }
        }
        public void openProj(string FileName)
        {
            this.CurrentProject = new Project(FileName, false);
            InitResponsesList();
            //this.RList = this.srl.getResponsesList();
            RefreshAllData();
        }
        #endregion                
        #region EVENTS
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e){InitNewProj();}
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e){
            Exit(e);
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e){
            this.CurrentProject.saveProject();
            //this.CurrentProject.kill();
            this.CurrentProject = new Project(CurrentProject.projectDirPath+"\\"+CurrentProject.projectName+".pdb", false);// CurrentProject.isNew);
            RefreshAllData();
        }
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.ShowDialog();
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            openFileDialog1.InitialDirectory = this.defaultProjectsDirPath;
            this.Cursor = Cursors.WaitCursor;
            this.showDefaultUnitDesciption();
            if ((this.CurrentProject.IsExist)&&(this.CurrentProject != null))this.CurrentProject.kill();
            this.openProj(openFileDialog1.FileName);
            this.Cursor = Cursors.Default;
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e){this.Close();}
        private void MainWindow_Shown_1(object sender, EventArgs e)
        {
            InitMenu imf = new InitMenu();
            imf.ShowDialog();
            if (imf.IsOKed)
            {
                if (!imf.Choose) this.InitNewProj();
                else {
                    openFileDialog1.InitialDirectory = Application.StartupPath;
                    openFileDialog1.ShowDialog();}
            }
            else{this.Exit();}
        }
        #endregion
        #region INTERFACE
        private void Personal_tabPage_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.Personal_tabPage.CreateGraphics();
            g.DrawRectangle(new Pen(Color.Black), this.NOTES_RichTextbox.Location.X - 1, this.NOTES_RichTextbox.Location.Y - 1, this.NOTES_RichTextbox.Width + 1, this.NOTES_RichTextbox.Height + 1);
        }
        Color buttonSelectionColor = Color.LightSteelBlue;
        Color buttonBackColor = Color.SteelBlue;
        #region addUnitButton
        private void addUnitLabel_MouseEnter(object sender, EventArgs e)
        {
            this.addUnitLabel.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)(( System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addUnitLabel.BackColor = buttonSelectionColor;
            this.addUnitPBox.BackColor = buttonSelectionColor;
        }
        private void addUnitLabel_MouseLeave(object sender, EventArgs e)
        {
            this.addUnitLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addUnitLabel.BackColor = buttonBackColor;
            this.addUnitPBox.BackColor = buttonBackColor;
        }
        private void addUnitPBox_MouseEnter(object sender, EventArgs e)
        {
            this.addUnitLabel.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addUnitLabel.BackColor = buttonSelectionColor;
            this.addUnitPBox.BackColor = buttonSelectionColor;
        }
        private void addUnitPBox_MouseLeave(object sender, EventArgs e)
        {
            this.addUnitLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.addUnitLabel.BackColor = buttonBackColor;
            this.addUnitPBox.BackColor = buttonBackColor;
        }
        private void addUnitPBox_Click(object sender, EventArgs e){addUnit();}
        private void addUnitLabel_Click(object sender, EventArgs e){addUnit();}
        #endregion
        #region addNewPost
        private void add_new_post_label_Click(object sender, EventArgs e){addNewPost();}
        private void add_new_post_pictureBox_Click(object sender, EventArgs e){addNewPost();}
        private void add_new_post_label_MouseEnter(object sender, EventArgs e)
        {
            this.add_new_post_label.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_new_post_label.BackColor = buttonSelectionColor;
            this.add_new_post_pictureBox.BackColor = buttonSelectionColor;
        }
        private void add_new_post_label_MouseLeave(object sender, EventArgs e)
        {
            this.add_new_post_label.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_new_post_label.BackColor = buttonBackColor;
            this.add_new_post_pictureBox.BackColor = buttonBackColor;
        }
        private void add_new_post_pictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.add_new_post_label.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)(( System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_new_post_label.BackColor = buttonSelectionColor;
            this.add_new_post_pictureBox.BackColor = buttonSelectionColor;
        }
        private void add_new_post_pictureBox_MouseLeave(object sender, EventArgs e)
        {
            this.add_new_post_label.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_new_post_label.BackColor = buttonBackColor;
            this.add_new_post_pictureBox.BackColor = buttonBackColor;
        }
        #endregion
        #region editUnit
        private void label29_Click(object sender, EventArgs e){
            if (currentUnit != null)
            {
                changeUnit();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e){changeUnit();}
        private void label29_MouseEnter(object sender, EventArgs e)
        {
            this.label29.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.BackColor = buttonSelectionColor;
            this.pictureBox1.BackColor = buttonSelectionColor;
        }
        private void label29_MouseLeave(object sender, EventArgs e)
        {
            this.label29.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.BackColor = buttonBackColor;
            this.pictureBox1.BackColor = buttonBackColor;
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            this.label29.Font = new System.Drawing.Font("Verdana", 9.75F, ((System.Drawing.FontStyle)(( System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.BackColor = buttonSelectionColor;
            this.pictureBox1.BackColor = buttonSelectionColor;
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.label29.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label29.BackColor = buttonBackColor;
            this.pictureBox1.BackColor = buttonBackColor;
        }
        #endregion
        #endregion        
    }
}