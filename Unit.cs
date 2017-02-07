using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
namespace PersonalRU
{
    public class Unit: IO_methods
    {
        public UnitFileList UFList;
        public string lastName = "",    //Фамилия
                      firstName = "",   // Имя
                      fatherName = "",  // Отчество
                      jobTitle = "",    // Должность
                      ProductionSite = "", // Участок производства
                      Notes = "", // Примечания
                      PhotoFPath = "", // Путь к файлу фотографии сотрудника
                      tableDescription="", // примечания в таблице
                      wage = "", // зарплата
                      wage_ext = ""; // валюта
        public Bitmap photo;
        public bool Exist = false;
        public bool IsVoid = false;
        private int LID = 1001;
        public string ID = "";
        public string vID = "";
        public bool isVacExist = false;
        
        private string unitListDirPath; // .../ Application/ Projects/ Project/ &data or &temp/ 
        private string unitDirPath; // unitListDirPath + unitDir
        
        #region MAIN
        public Unit(string unitString, string unitListDirPath, int LID)
        {
            this.LID = LID;
            this.unitListDirPath = unitListDirPath;
            this.unitDirPath = unitListDirPath + "\\" + Convert.ToString(this.LID);
            this.FromString(unitString);
            Directory.CreateDirectory(unitDirPath);
        }
        public void setUnitListDirPath(string _unitListDirPath)
        {
            this.unitListDirPath = _unitListDirPath;
            this.unitDirPath = this.unitListDirPath + "\\" + LID;
        }
        public void setLID(int LID)
        {
            if (LID == 1001) return;
            if (this.LID != LID)
            {
                this.LID = LID;
                string newUnitDirPath = this.unitListDirPath + "\\" + Convert.ToString(LID);
                Directory.Move(this.unitDirPath, newUnitDirPath);
                this.unitDirPath = newUnitDirPath;
            }
            else { this.LID = LID; }            
        }
        public int getLID() { return this.LID; }
        public List<UnitFile> getUFList() { return this.UFList.getUFList(); }
        public void AddFile(string sourceFilePath)
        {
            UFList.addUFile(sourceFilePath, this.unitDirPath);
        }
        public void deleteFile(UnitFile ufile)
        {
            UFList.deleteUnitFile(ufile);
        }
        public UnitFile getUnitFileByLID(int LID) { return this.UFList.getUnitFileByLID(LID); }
        public void setNewPhoto(string tempPhotoPath){
            Bitmap temp_photo = new Bitmap(tempPhotoPath);

            this.PhotoFPath = this.unitListDirPath + "\\" + this.LID + ".png";
            this.dispose();
            if (File.Exists(PhotoFPath)) File.Delete(PhotoFPath);
            temp_photo.Save(this.PhotoFPath);
            this.photo = new Bitmap(temp_photo);
            temp_photo.Dispose();
            File.Delete(tempPhotoPath);
        }
        public void delete()
        {
            DirectoryInfo DInfo = new DirectoryInfo(this.unitDirPath);
            foreach (FileInfo f in DInfo.GetFiles())
                f.Delete();
            DInfo.Delete();
            this.photo.Dispose();
            File.Delete(this.PhotoFPath);
        }
        #region FILE
        public void FromString (string f_string)
        {
            f_string = f_string.Substring(1, f_string.Length - 2);
            string UnitFileListString = extractUnitFileListString(f_string);
            string UnitListString = extractUnitListString(f_string);

            this.lastName = findFromString("LastName", UnitListString);
            this.firstName = findFromString("FirstName", UnitListString);
            this.fatherName = findFromString("FatherName", UnitListString);
            this.jobTitle = findFromString("JobTitle", UnitListString);
            this.ProductionSite = findFromString("ProductionSite", UnitListString);
            this.Notes = findFromString("Notes", UnitListString);
            this.wage = findFromString("Wage", UnitListString);
            this.wage_ext = findFromString("WageExt", UnitListString);
            
            bool isPhotoTemporary = Convert.ToBoolean(findFromString("IsPhotoTemporary", UnitListString));
            if (isPhotoTemporary)
            {
                string temp_photo_path = findFromString("PhotoPath", UnitListString);
                Bitmap temp_photo = new Bitmap(temp_photo_path);
                
                
                this.PhotoFPath = this.unitListDirPath + "\\" + this.LID + ".png";
                this.dispose();
                if (File.Exists(PhotoFPath)) File.Delete(PhotoFPath);
                temp_photo.Save(this.PhotoFPath);
                this.photo = new Bitmap(temp_photo);
                temp_photo.Dispose();
                File.Delete(temp_photo_path);
            }
            else
            {
                this.PhotoFPath = this.unitListDirPath +"\\"+ findFromString("PhotoPath", UnitListString);
                this.photo = new Bitmap(this.PhotoFPath);
            }
            this.setLID(Convert.ToInt16(findFromString("LID", UnitListString)));
            this.unitDirPath = unitListDirPath + "\\" + Convert.ToString(this.LID);
            this.ID = findFromString("ID", UnitListString);
            this.vID = findFromString("vID", UnitListString);
            this.isVacExist = Convert.ToBoolean(findFromString("isVacExist", UnitListString));
            this.IsVoid = Convert.ToBoolean(findFromString("isVoid", UnitListString));
            this.tableDescription = findFromString("TableDescription", UnitListString);
            this.UFList = new UnitFileList(this.unitDirPath, UnitFileListString);
        }
        private string extractUnitListString(string f_string)
        {
            string str = "";
            string UFL_str = "";
            int begin = 0;
            string str_name = " ";
            while(str_name != "")
            {
                str_name = make_single_data_string(begin, f_string, ':'); begin += str_name.Length + 1;                
                if (str_name == "UnitFileList"){
                    string count_str = make_single_data_string(begin,f_string,';'); begin+=count_str.Length+1;
                    int count = Convert.ToInt16(count_str);
                    string length_str = make_single_data_string(begin,f_string,';');begin+=length_str.Length+1;
                    int length = Convert.ToInt16(length_str);
                    UFL_str = f_string.Substring(begin, length);
                    str = f_string.Substring(0, begin - "UnitFileList".Length - count_str.Length - length_str.Length-3);
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
                str_name = make_single_data_string(begin, f_string, ':');first_begin = begin += str_name.Length + 1;
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
        private string findFromString(string obj_name, string unitString)
        {
            int begin = 0;
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
        public string toString()
        {
            FileInfo finfo = new FileInfo(this.PhotoFPath);
            string photoFileName = finfo.Name;
            string str = "{" + "LastName:" + this.lastName + ";" +
                               "FirstName:" + this.firstName + ";" +
                               "FatherName:" + this.fatherName + ";" +
                               "JobTitle:" + this.jobTitle + ";" +
                               "ProductionSite:" + this.ProductionSite + ";" +
                               "Notes:" + this.Notes + ";" +
                               "IsPhotoTemporary:" + false.ToString() + ";" +
                               "PhotoPath:" + photoFileName + ";" +
                               "TableDescription:" + this.tableDescription + ";" +
                               "LID:" + this.LID + ";" +
                               "vID:" + this.vID + ";" +
                               "isVacExist:" + Convert.ToString(this.isVacExist) + ";" +
                               "isVoid:" + Convert.ToString(this.IsVoid) + ";" +
                                "ID:" + this.ID + ";" +
                                "UnitFileList:"+ this.UFList.toString() + ";" +
                                "Wage:" + this.wage + ";" +
                                "WageExt:" + this.wage_ext + ";" +
                               ":;}";
            str = str.Length + ";" + str;
            return str;
        }
        public string toString_without_length()
        {
            FileInfo finfo = new FileInfo(this.PhotoFPath);
            string photoFileName = finfo.Name;
            string str = "{" + "LastName:" + this.lastName + ";" +
                               "FirstName:" + this.firstName + ";" +
                               "FatherName:" + this.fatherName + ";" +
                               "JobTitle:" + this.jobTitle + ";" +
                               "ProductionSite:" + this.ProductionSite + ";" +
                               "Notes:" + this.Notes + ";" +
                               "IsPhotoTemporary:" + false.ToString() + ";" +
                               "PhotoPath:" + photoFileName + ";" +
                               "TableDescription:" + this.tableDescription + ";" +
                               "LID:" + this.LID + ";" +
                               "vID:" + this.vID + ";" +
                               "isVacExist:" + Convert.ToString(this.isVacExist) + ";" +
                               "isVoid:" + Convert.ToString(this.IsVoid) + ";" +
                               "ID:" + this.ID + ";" +
                               "UnitFileList:" + this.UFList.toString() + ";" +
                               "Wage:" + this.wage + ";" +
                               "WageExt:" + this.wage_ext + ";" +
                               ":;}";
            return str;
        }
        public void deleteAllUFiles()
        {
            foreach (UnitFile ufile in this.UFList.getUFList())
                ufile.deleteFile();
        }
        #endregion
        #endregion                
        public void dispose()
        {
            if (this.photo != null) this.photo.Dispose();
        }
        public Bitmap getPhoto() {
            return this.photo;
        }
    }
}
