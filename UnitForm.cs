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
    public partial class UnitForm : Form
    {
        private Bitmap result_photo;
        private Size p_size = new Size(140, 160);
        private SRL.SRL srl = new SRL.SRL(true);
        private bool isNew = true;
        private string unit_string = "";
        public bool isOked = false;

        private string firstName = "";
        private string lastName = "";
        private string fatherName = "";
        private string jobTitle = "";
        private string productionSite = "";
        private string Notes = "";
        private string UnitPhotoPath = "";
        private string Wage = "";
        private string WageExt = "";

        private int LID;
        private string ID = "";
        private string vID = "";
        private bool isVacExist = false;
        private bool isVoid = false;
        private string tableDescription = "";
        private UnitFileList UFList;
        private string UFListString = "";
        Bitmap resizedPhoto;
        private string tempDirPath = "";

        public UnitForm(string unit_string, SRL.SRL srl, bool isNew, string projectDirPath, List<string> departmentList, List<string> jobTitleList)  // ProjectDirPath = ...//Application//Projects//ProjectName
        {
            this.srl = srl;
            this.isNew = isNew;
            this.tempDirPath = projectDirPath + "\\temp";

            InitializeComponent();

            if (!isNew) this.unit_string = extractUnitStringExcludingUnitFileListStr(unit_string);
            else this.unit_string = unit_string;
            this.firstName = findFromString("FirstName", this.unit_string);
            this.lastName = findFromString("LastName", this.unit_string);
            this.fatherName = findFromString("FatherName", this.unit_string);
            this.jobTitle = findFromString("JobTitle", this.unit_string);
            this.productionSite = findFromString("ProductionSite", this.unit_string);
            this.Notes = findFromString("Notes", this.unit_string);
            this.Wage = findFromString("Wage", this.unit_string);
            for (int i = 0; i < this.Wage.Length; i++)
            {
                this.Wage_textBox.Text += this.Wage.ElementAt(i);
            }
            this.WageExt = findFromString("WageExt", this.unit_string);

            this.LID = Convert.ToInt16(findFromString("LID", this.unit_string));
            this.ID = findFromString("ID", this.unit_string);
            this.vID = findFromString("vID", this.unit_string);
            this.isVacExist = Convert.ToBoolean(findFromString("isVacExist", this.unit_string));
            this.isVoid = Convert.ToBoolean(findFromString("isVoid", this.unit_string));
            this.tableDescription = findFromString("TableDescription", this.unit_string);
            this.UnitPhotoPath = findFromString("PhotoPath", this.unit_string);
            
            if (WageExt.Length>0) this.wageExt_comboBox.Text = WageExt;

            this.UFListString = extractUnitFileListString(this.unit_string);
            this.first_name_textbox.Text = this.firstName;
            this.last_name_textbox.Text = this.lastName;
            this.father_name_textbox.Text = this.fatherName;
            this.UFListString = extractUnitFileListString(unit_string);

            foreach (string jobTitle in jobTitleList)
                this.jobTitle_comboBox.Items.Add(jobTitle);
            foreach (string department in departmentList)
                ProductionSite_comboBox.Items.Add(department);
            this.jobTitle_comboBox.Text = this.jobTitle;
            this.ProductionSite_comboBox.Text = this.productionSite;
            this.Notes_textbox.Text = this.Notes;

            if (isNew) resizedPhoto = new Bitmap(PersonalRU.Properties.Resources.No_photo, pictureBox1.Size);
            else { 
                Bitmap bmp = new Bitmap(this.tempDirPath + "\\" + this.UnitPhotoPath);
                resizedPhoto = new Bitmap(bmp, pictureBox1.Size);
                bmp.Dispose();
            }
            this.pictureBox1.Image = resizedPhoto;
            
            if (isNew) this.wageExt_comboBox.SelectedItem = wageExt_comboBox.Items[0];
            
        }
        private string extractUnitStringExcludingUnitFileListStr(string f_string)
        {
            string str = "";
            string UFL_str = "";
            int begin = 0;
            string str_name = " ";
            while (str_name != "")
            {
                str_name = make_single_data_string(begin, f_string, ':'); begin += str_name.Length + 1;
                if (str_name == "UnitFileList")
                {
                    string count_str = make_single_data_string(begin, f_string, ';'); begin += count_str.Length + 1;
                    int count = Convert.ToInt16(count_str);
                    string length_str = make_single_data_string(begin, f_string, ';'); begin += length_str.Length + 1;
                    int length = Convert.ToInt16(length_str);
                    UFL_str = f_string.Substring(begin, length);
                    str = f_string.Substring(0, begin - "UnitFileList".Length - count_str.Length - length_str.Length - 3);
                    begin += UFL_str.Length + 1;
                    str += f_string.Substring(begin);
                    return str;
                }
                else { string s = make_single_data_string(begin, f_string, ';'); begin += s.Length + 1; }
            }
            return str;
        }
        private string extractUnitFileListString(string f_string)
        {
            string UFL_str = "";
            int first_begin = 0;
            int begin = 0;
            string str_name = " ";
            while (str_name != "")
            {
                str_name = make_single_data_string(begin, f_string, ':'); first_begin = begin += str_name.Length + 1;
                if (str_name == "UnitFileList")
                {
                    string count_str = make_single_data_string(begin, f_string, ';'); begin += count_str.Length + 1;
                    int count = Convert.ToInt16(count_str);
                    string length_str = make_single_data_string(begin, f_string, ';'); begin += length_str.Length + 1;
                    int length = Convert.ToInt16(length_str);
                    UFL_str = f_string.Substring(first_begin, length + count_str.Length + 1 + length_str.Length + 1);
                    return UFL_str;
                }
                else { string s = make_single_data_string(begin, f_string, ';'); begin += s.Length + 1; }
            }
            return UFL_str;
        }
        private void makeUnit_HINSTANCE()
        {
            this.lastName = last_name_textbox.Text;
            this.firstName = first_name_textbox.Text;
            this.fatherName = father_name_textbox.Text;
            this.jobTitle = jobTitle_comboBox.Text;
            this.productionSite = this.ProductionSite_comboBox.Text;
            this.Notes = Notes_textbox.Text;
            this.UFList = new UnitFileList();
            this.Wage = Wage_textBox.Text.Trim(' ');
            this.WageExt = wageExt_comboBox.Text;
            if (this.isNew) this.ID = srl.CreateID();

        }
        private void setImageToPictureBox(Bitmap photo)
        {
            Bitmap employee_bmp_resized = new Bitmap(photo, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = employee_bmp_resized;
        }
        private void saveResultPhoto()
        {
            this.result_photo = new Bitmap(pictureBox1.Image,pictureBox1.Size);// p_size);
        }
        public string toString()
        {
            string str;
            if (this.isNew)
            {
                this.tableDescription = "Создать вакансию";
                str = "{" + "LastName:" + this.lastName + ";" +
                             "FirstName:" + this.firstName + ";" +
                             "FatherName:" + this.fatherName + ";" +
                             "JobTitle:" + this.jobTitle + ";" +
                             "ProductionSite:" + this.productionSite + ";" +
                             "Notes:" + this.Notes + ";" +
                             "IsPhotoTemporary:" + true.ToString() + ";" +
                             "PhotoPath:" + this.UnitPhotoPath + ";" +
                             "TableDescription:" + this.tableDescription + ";" +
                             "LID:" + this.LID + ";" +
                             "vID:" + this.vID + ";" +
                             "isVacExist:" + Convert.ToString(this.isVacExist) + ";" +
                             "isVoid:" + Convert.ToString(this.isVoid) + ";" +
                             "ID:" + this.ID + ";" +
                             "UnitFileList:" + this.UFList.toString() + ";" +
                             "Wage:" + this.Wage + ";" +
                             "WageExt:" + this.WageExt + ";" +
                             ":;}";
            }
            else
            {
                str = "{" + "LastName:" + this.lastName + ";" +
                             "FirstName:" + this.firstName + ";" +
                             "FatherName:" + this.fatherName + ";" +
                             "JobTitle:" + this.jobTitle + ";" +
                             "ProductionSite:" + this.productionSite + ";" +
                             "Notes:" + this.Notes + ";" +
                             "IsPhotoTemporary:" + true.ToString() + ";" +
                             "PhotoPath:" + this.UnitPhotoPath + ";" +
                             "TableDescription:" + this.tableDescription + ";" +
                             "LID:" + this.LID + ";" +
                             "vID:" + this.vID + ";" +
                             "isVacExist:" + Convert.ToString(this.isVacExist) + ";" +
                             "isVoid:" + Convert.ToString(this.isVoid) + ";" +
                             "ID:" + this.ID + ";" +
                             "UnitFileList:" + this.UFListString + ";" +
                             "Wage:" + this.Wage + ";" +
                             "WageExt:" + this.WageExt + ";" +
                             ":;}";
            }
            return str;
        }        

        #region EVENTS
        private void OKButt_Click(object sender, EventArgs e)
        {
            string forbiddenChars = "qwer tyuiop[]asdfghjkl;'\\zxcvbnm,./`-=~!@#$%^&*()_+QWERTYUIOP{}ASDFGHJKL:\"|ZXCVBNM<>?ёйцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ";
            for (int i = 0; i < forbiddenChars.Length; i++)
            {
                char ch = forbiddenChars.ElementAt(i);
                if (Wage_textBox.Text.Contains(ch))
                {
                    MessageBox.Show("Некореектно введены данные о зарплате. Строка не должна содержать букв, знаков препинания и пробелов.");
                    Wage_textBox.Focus();
                    return;
                }
            }
            if (jobTitle_comboBox.Text.Length == 0) { MessageBox.Show("Введите наименование должности.", "Наименование должности"); jobTitle_comboBox.Focus(); return; }
            if (last_name_textbox.Text.Length == 0) { MessageBox.Show("Введите фамилию сотрудника", "Фамилия сотрудника"); last_name_textbox.Focus(); return; }
            if (this.first_name_textbox.Text.Length == 0) { MessageBox.Show("Введите имя сотрудника", "Имя сотрудника"); first_name_textbox.Focus(); return; }
            if (father_name_textbox.Text.Length == 0) { MessageBox.Show("Введите отчество сотрудника", "Отчество сотрудника");father_name_textbox.Focus(); return; }
            if (ProductionSite_comboBox.Text.Length == 0) { MessageBox.Show("Укажите отдел", "Отдел");ProductionSite_comboBox.Focus(); return; }
            if (Wage_textBox.Text.Length == 0) { MessageBox.Show("Укадите зарплаты", "Зарплата"); this.Wage_textBox.Focus(); return; }
            makeUnit_HINSTANCE();

            string tempPhotoPath = this.tempDirPath + "\\temp_avatar.png";
            this.pictureBox1.Image.Save(tempPhotoPath);
            this.pictureBox1.Image = PersonalRU.Properties.Resources.ImageNone;
            this.pictureBox1.Image.Dispose();
            this.resizedPhoto.Dispose();
            //this.result_photo.Dispose();
            if (this.result_photo != null)this.result_photo.Dispose();
           

            this.UnitPhotoPath = tempPhotoPath;
            this.isOked = true;
            this.Close();
        }
        private void Cancel_butt_Click(object sender, EventArgs e)
        {
            this.isOked = false;
            this.Close();
        }
        private void openFileDialogPhoto_FileOk(object sender, CancelEventArgs e)
        {
         
                
        }
        private void ChangePhoto_butt_Click(object sender, EventArgs e)
        {
            Bitmap photo = new Bitmap(this.pictureBox1.Image);
            Bitmap new_photo;

            PhotoDesignForm pdf = new PhotoDesignForm(photo, true, "");
            if (pdf.Enabled) pdf.ShowDialog();
            if (pdf.isOKed)
            {
                new_photo = new Bitmap(pdf.getPhoto());
                setImageToPictureBox(new_photo);
            }
        }
        int oldLength = 0;
        private void Wage_textBox_TextChanged(object sender, EventArgs e)
        {
            //if (Wage_textBox.Text.Length > 0)
            //{
            //    char ch;
            //    if (Wage_textBox.Text.Length < oldLength)
            //    {
            //        if ((Wage_textBox.Text.Length == 3) || (Wage_textBox.Text.Length == 7) || (Wage_textBox.Text.Length == 11) || (Wage_textBox.Text.Length == 15))
            //        {
            //            Wage_textBox.Text = Wage_textBox.Text.Substring(0, Wage_textBox.Text.Length - 1);
            //            return;
            //        }
            //    }
            //    if ((Wage_textBox.Text.ElementAt(Wage_textBox.Text.Length - 1) != ' '))
            //    {
            //        if (Wage_textBox.Text.Length == 3) Wage_textBox.Text = Wage_textBox.Text.Insert(Wage_textBox.Text.Length - 3, " ");
            //        if (Wage_textBox.Text.Length == 7) Wage_textBox.Text = Wage_textBox.Text.Insert(Wage_textBox.Text.Length - 7, " ");
            //        if (Wage_textBox.Text.Length == 11) Wage_textBox.Text = Wage_textBox.Text.Insert(Wage_textBox.Text.Length - 11, " ");
            //        if (Wage_textBox.Text.Length == 15) Wage_textBox.Text = Wage_textBox.Text.Insert(Wage_textBox.Text.Length - 15, " ");
            //        Wage_textBox.Select(Wage_textBox.Text.Length, 0);
                    if (Wage_textBox.Text.Length > 19) Wage_textBox.Text = Wage_textBox.Text.Substring(1, Wage_textBox.Text.Length - 1);
            //    }

            //    this.oldLength = Wage_textBox.Text.Length;
            //}
            
        }
        #endregion
        #region StringHelper
        private string findFromString(string obj_name, string unitString)
        {
            int begin = 1;
            string str_name = " ";
            while (str_name != "")
            {
                str_name = make_single_data_string(begin, unitString, ':'); begin += str_name.Length+1;
                if (str_name == obj_name)
                {
                    string str = make_single_data_string(begin, unitString, ';');
                    begin += str.Length+1;
                    return str;
                }
                else
                {
                    string str = make_single_data_string(begin, unitString, ';');
                    begin += str.Length+1;
                }
            }
            return "Not found!";
        }
        protected string make_single_data_string(int begin, string file_string, char separator)
        {
            string str = "";
            int end = get_end_of_single_string(begin, file_string, separator);
            if (end - begin > 0) return str = file_string.Substring(begin, end - begin);
            else return "";
        }
        private int get_end_of_single_string(int begin, string file_string, char separator)
        {
            char char_ = ' ';
            while (char_ != separator && char_ != '}')
            {
                char_ = file_string.ElementAt(begin);
                begin++;
            }
            return begin - 1;
        }
        #endregion
    }
}