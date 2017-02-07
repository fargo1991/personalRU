using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace PersonalRU
{
    public class Project: IO_methods
    {
        public bool IsExist = false;
        public string projectDirPath = "";  //  .../Application/ Projects/ THIS.projectDirPath/
        string projectFilePath = ""; // .../Application/ Projects/ THIS.projectDirPath/ THIS.projectName.pdb
        public string projectName = ""; //     THIS.projectName
        public string tempDirPath = ""; //     .../Application/ Projects/ THIS.projectDirPath/ temp/
        string dataDirPath = ""; //     .../Application/ Projects/ THIS.projectDirPath/ data/
        public bool isNew = false;

        private UnitList UList;
        private VacancyList VacList = new VacancyList();

        private bool isTemporary = true;
        public Project() { }
        public Project(string projectDirPath, bool isNewProject)
        {
            this.isNew = isNewProject;
            if (this.isNew)
            {
                this.projectDirPath = projectDirPath;
                DirectoryInfo dinfo = new DirectoryInfo(this.projectDirPath);
                this.projectName = dinfo.Name;
                this.projectFilePath = projectDirPath + "\\" + projectName + ".pdb";
                this.tempDirPath = projectDirPath + "\\temp";
                this.dataDirPath = projectDirPath + "\\data";
                try
                {
                    Directory.CreateDirectory(this.projectDirPath);
                    if (Directory.Exists(tempDirPath)) this.kill();
                    Directory.CreateDirectory(this.tempDirPath);
                    Directory.CreateDirectory(this.dataDirPath);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                setTemporaryMode(true);
                UList = new UnitList(this.tempDirPath,true,"",0);
                VacList = new VacancyList();
            }
            else
            {
                DirectoryInfo dinfo = new DirectoryInfo(projectDirPath);
                this.projectName = dinfo.Name.Substring(0,dinfo.Name.Length-4);
                this.projectDirPath = projectDirPath.Substring(0, projectDirPath.Length -4 - this.projectName.Length);
                this.projectFilePath = this.projectDirPath + "\\" + projectName +".pdb";
                this.tempDirPath = this.projectDirPath + "\\temp";
                this.dataDirPath = this.projectDirPath + "\\data";
                
                try
                {
                    if (Directory.Exists(tempDirPath)) this.kill();
                    Directory.CreateDirectory(this.tempDirPath);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
                setTemporaryMode(true);
                
                this.FromFile(this.projectFilePath);
            }            
            this.IsExist = true;
        }
        public List<Unit> getUnitList() { return this.UList.getUnitList(); }
        public List<SRL.Vacancy> getVacancyList() { return this.VacList.getList(); }
        #region EDITING PROJECT
        public void addUnit(string unitString){ this.UList.add(unitString); }
        public void deleteUnit(Unit unit) { this.UList.delete(unit); }
        public void changeUnit(string unitString) { this.UList.change(unitString); }
        public void addVacancy(SRL.Vacancy vacancy) { this.VacList.add(vacancy); }
        public void deleteVacancy(SRL.Vacancy vacancy) { this.VacList.delete(vacancy); }
        public void changeVacancy(SRL.Vacancy vacancy) { this.VacList.exchange(vacancy); }
        public SRL.Vacancy getVacancyByLID(int LID) {return this.VacList.getVacancyByLID(LID); }
        public SRL.Vacancy getVacancyByID(string ID) { return this.VacList.getVacancyByID(ID); }
        public Unit getUnitByLID(int LID) { return this.UList.getUnitByLID(LID); }
        public Unit getUnitByID(string ID) { return UList.getUnitByID(ID); }
        public void addUnitFile(string sourceFilePath, Unit unit) { this.UList.addFile(sourceFilePath, unit); }
        public void deleteUnitFile(UnitFile ufile, Unit unit) { this.UList.deleteFile(ufile, unit); }
        public UnitFile getUnitFileByLID(Unit unit, int LID) { return this.UList.getUnitFileByLID(unit, LID); }
        #endregion
        public void saveProject()
        {
            createProjectFile();
            setTemporaryMode(false);
            this.isNew = false;
            setTemporaryMode(true);
        }
        private void createProjectFile()
        {
            string projectTempFilePath = this.tempDirPath + "\\" + this.projectName + ".pdb";
            FileInfo projectFile = new FileInfo(projectTempFilePath);
            FileStream fstream = projectFile.Create();
            fstream.Close();
            //File.Create(projectTempFilePath);

            StreamWriter sw = new StreamWriter(projectTempFilePath);
            sw.Write(this.toString());
            sw.Close();
        }
        private void setTemporaryMode(bool _isTemporary)
        {
            if (_isTemporary) {
                // Deleteing old temp Directory and creating new void /temp directory
                if (!Directory.Exists(this.tempDirPath)) Directory.CreateDirectory(this.tempDirPath);
                else
                {
                    this.kill(); Directory.CreateDirectory(this.tempDirPath);
                }
                // Copy ProjectFile from /data to /temp
                if (File.Exists(this.projectFilePath))
                    File.Copy(this.projectFilePath, this.tempDirPath +"\\"+ projectName + ".pdb");
                // Copy all files and folders from /data to /temp
                DirectoryInfo dinfo = new DirectoryInfo(dataDirPath);
                if (dinfo.Exists)
                {
                    foreach (FileInfo finfo in dinfo.GetFiles())
                        finfo.CopyTo(this.tempDirPath + "\\" + finfo.Name);
                    foreach (DirectoryInfo unitDir in dinfo.GetDirectories())
                    {
                        Directory.CreateDirectory(this.tempDirPath + "\\" + unitDir.Name);
                        foreach (FileInfo ufile in unitDir.GetFiles())
                            File.Copy(unitDir.FullName + "\\" + ufile.Name, this.tempDirPath + "\\" + unitDir.Name + "\\" + ufile.Name);
                    }
                }
                else
                {
                    dinfo.Create();
                }
                
            }
            else
            {
                {  // Delete projectFile
                    if (File.Exists(this.projectFilePath)) File.Delete(this.projectFilePath);
                    // Delete all folders and files from /data
                    DirectoryInfo dinfo = new DirectoryInfo(this.dataDirPath);
                    foreach (FileInfo finfo in dinfo.GetFiles())
                        finfo.Delete();
                    foreach (DirectoryInfo UnitDir in dinfo.GetDirectories())
                    {
                        foreach (FileInfo ufile in UnitDir.GetFiles())
                            ufile.Delete();
                        UnitDir.Delete();
                    }
                }
                { // Copy all files and folders from /temp to /data
                    File.Copy(this.tempDirPath + "\\" +this.projectName + ".pdb", this.projectFilePath);
                    DirectoryInfo dinfo = new DirectoryInfo(this.tempDirPath);
                    foreach (FileInfo f in dinfo.GetFiles())
                        f.CopyTo(this.dataDirPath + "\\" + f.Name);
                    File.Delete(this.dataDirPath + "\\" + this.projectName + ".pdb");
                    foreach (DirectoryInfo unitDir in dinfo.GetDirectories())
                    {
                        Directory.CreateDirectory(this.dataDirPath + "\\" + unitDir.Name);
                        foreach (FileInfo unitFile in unitDir.GetFiles())
                            unitFile.CopyTo(this.dataDirPath + "\\" + unitDir.Name + "\\" + unitFile.Name);
                    }
                }
                { // Delete all folders and files from /temp
                    this.kill();
                    //Directory.Delete(this.tempDirPath);
                    //Directory.CreateDirectory(this.tempDirPath);                    
                }
                this.UList.setUnitListDirPath(this.dataDirPath);
            }
            this.isTemporary = _isTemporary;
        }
        private void FromFile(string fname)
        {
            StreamReader sr = new StreamReader(fname);
            string project_str = sr.ReadToEnd();
            int begin = 0;
            string UnitsCount_str = make_single_data_string(begin, project_str, ';');
            int UnitListCount = Convert.ToInt16(UnitsCount_str);
            begin += UnitsCount_str.Length+1;
            string UnitListLength_str = make_single_data_string(begin, project_str, ';');
            int UnitListLength = Convert.ToInt16(UnitListLength_str);
            begin += UnitListLength_str.Length+1;
            string UListString = project_str.Substring(begin+1,UnitListLength - 1);
            begin += UListString.Length + 1;

            string VacListCount_str = make_single_data_string(begin, project_str, ';');
            begin+=VacListCount_str.Length+1;
            int VacListCount = Convert.ToInt16(VacListCount_str);
            string VacListLength_str = make_single_data_string(begin, project_str, ';');
            int VacListLength = Convert.ToInt16(VacListLength_str);
            begin += VacListLength_str.Length+1;
            string VacListString = project_str.Substring(begin+1, VacListLength);

            this.UList = new UnitList(this.tempDirPath, false, UListString, UnitListCount);
            this.VacList = new VacancyList(VacListString, VacListCount);
            sr.Close();
        }
        public string toString()
        {
            string projectString = "";
            projectString = this.UList.toString() + this.VacList.toString();
            return projectString;
        }
        public void kill()
        {
            if(this.UList != null) 
                this.UList.dispose();
            DirectoryInfo tempDirInfo = new DirectoryInfo(this.tempDirPath);
            
            if (tempDirInfo.Exists)
            {
                foreach (DirectoryInfo unitDir in tempDirInfo.GetDirectories())
                {
                    foreach (FileInfo unitFile in unitDir.GetFiles())
                        unitFile.Delete();
                    unitDir.Delete();
                }
                if (File.Exists(this.tempDirPath + "\\" + this.projectName + ".pdb"))
                    File.Delete(this.tempDirPath + "\\" + this.projectName + ".pdb");
                foreach (FileInfo finfo in tempDirInfo.GetFiles())
                    finfo.Delete();
                tempDirInfo.Delete();
            }
        }
    }
}