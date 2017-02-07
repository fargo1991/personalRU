using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PersonalRU
{
    public partial class NewProjectForm : Form
    {
        public bool ISOked = false;
        public string ProjectName = "";
        public string ProjectPath = "";
        Project CurrentProject;
        public NewProjectForm(string defaultProjectDirPath,ref Project currentProject)
        {
            InitializeComponent();
            ProjectPath =  defaultProjectDirPath;
            ProjectPath_textbox.Text = ProjectPath;
            this.CurrentProject = currentProject;
        }
        private void BrowseButt_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = ProjectPath;
            folderBrowserDialog.ShowDialog();
            this.ProjectPath_textbox.Text = folderBrowserDialog.SelectedPath;
        }
        private void Ok_butt_Click(object sender, EventArgs e)
        {
            ProjectName = ProjectName_textBox.Text;
            ProjectPath = ProjectPath_textbox.Text;
            DirectoryInfo DataInfo = new DirectoryInfo(ProjectPath + "\\" + ProjectName +"\\data");
            DirectoryInfo ProjInfo = new DirectoryInfo(ProjectPath + "\\" + ProjectName);
            if (!DataInfo.Exists)
            {
                ISOked = true;
                this.Close();
            }
            else
            {                
                AskForm askf = new AskForm("    Проект с таким именем уже существует. Вы все равно хотите создать проект с этим именем?     \n ВНИМАНИЕ! Данные старого проекта будут утеряны!", global::PersonalRU.Properties.Resources.Alert);
                askf.ShowDialog();

                if (askf.IsOKed){
                    if (this.CurrentProject.IsExist)
                        this.CurrentProject.kill();
                    foreach (DirectoryInfo dinfo in ProjInfo.GetDirectories()){
                        foreach (DirectoryInfo dirinf in dinfo.GetDirectories()){
                            foreach (DirectoryInfo dinf in dirinf.GetDirectories()){
                                foreach (FileInfo f in dinf.GetFiles())
                                    f.Delete();
                                    dinf.Delete();}
                            foreach (FileInfo finfo in dirinf.GetFiles())
                                finfo.Delete();
                            dirinf.Delete();}
                        foreach (FileInfo fi in dinfo.GetFiles())
                            fi.Delete();
                        dinfo.Delete();}
                    foreach (FileInfo finfi in ProjInfo.GetFiles())
                        finfi.Delete();
                    ProjInfo.Delete();
                    ISOked = true;
                    this.Close();}
                else{ ISOked = false; }
            }
        }
        private void Cancel_butt_Click(object sender, EventArgs e)
        {
            ISOked = false;
            this.Close();
        }
    }
}