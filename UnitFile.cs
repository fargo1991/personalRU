using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PersonalRU
{
    public class UnitFile:IO_methods
    {
        public string FileName = "";
        private string unitDirPath = ""; // ...// ProjectName// &temp or &data// UnitName
        public string fullPath = ""; // dirPath + FileName
        private string sourceFilePath = "";

        private int LID;
        public void setLID(int LID) { this.LID = LID; }
        public int getLID() { return this.LID; }
        public UnitFile(string sourceFilePath, string unitDirPath) // Initialize new UnitFIle
        {
            this.sourceFilePath = sourceFilePath;
            FileInfo file = new FileInfo(this.sourceFilePath);
            this.FileName = file.FullName.Substring(file.DirectoryName.Length, file.FullName.Length - file.DirectoryName.Length);
            this.unitDirPath = unitDirPath;
            this.fullPath = this.unitDirPath + this.FileName;
            try { File.Copy(this.sourceFilePath, this.fullPath); }
            catch (Exception e) { MessageBox.Show("Error in UnitFile(constructor) File.Copy(,)!!! \n e.Message: " + e.Message); }
        }
        public UnitFile(string ufString, string unitDirPath, bool isFrom_ufString) // Initialize existing UnitFile from ufString or reading UnitFile data from projectFile
        {
            this.fromString(ufString);
            this.unitDirPath = unitDirPath;
            this.fullPath = this.unitDirPath + this.FileName;
            //FileInfo unitFile = new FileInfo(this.fullPath);
            //this.FileName = unitFile.FullName.Substring(unitFile.DirectoryName.Length, unitFile.FullName.Length - unitFile.DirectoryName.Length);
            //this.unitDirPath = unitFile.FullName.Substring(0, unitFile.FullName.Length - this.FileName.Length);
            //this.fullPath = this.unitDirPath + this.FileName;
        }
        public void deleteFile() { File.Delete(this.fullPath); }
        public void setUnitDirPath(string unitDirPath){this.unitDirPath = unitDirPath;}
        public string toString()
        {
            string str = "";
            str += "{";
            str += "fullPath:" + fullPath + ";";
            str += "fileName:" + FileName + ";";
            str += "}";
            return str;
        }
        private void fromString(string ufString)
        {
            //this.fullPath = findFromString("fullPath", ufString);
            this.FileName = findFromString("fileName", ufString);
        }
        private string findFromString(string obj_name, string unitString)
        {
            int begin = 1;
            string str_name = " ";
            while (str_name != "")
            {
                str_name = make_single_data_string(begin, unitString, ':'); begin += str_name.Length + 1;
                if (str_name == obj_name)
                {
                    string str = make_single_data_string(begin, unitString, ';');
                    begin += str.Length + 1;
                    return str;
                }
                else
                {
                    string str = make_single_data_string(begin, unitString, ';');
                    begin += str.Length + 1;
                }
            }
            return "Not found!";
        }
    }
}
