using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalRU
{
    public class UnitFileList:IO_methods
    {
        List<UnitFile> UFList = new List<UnitFile>();
        private string unitDirPath = "";
        public UnitFileList() { }
        public UnitFileList(string unitDirPath)
        {
            this.unitDirPath = unitDirPath;
        }
        public UnitFileList(string unitDirPath, string f_string)
        {
            this.unitDirPath = unitDirPath;
            if (f_string !="") this.fromString(f_string);
        }
        public void setUnitDirPath(string unitDirPath)
        {
            this.unitDirPath = unitDirPath;
            foreach (UnitFile ufile in UFList)
                ufile.setUnitDirPath(this.unitDirPath);
        }
        public List<UnitFile> getUFList() { return this.UFList; }
        public void addUFile(string ufString, string unitDirPath)
        {
            UnitFile ufile = new UnitFile(ufString, unitDirPath);
            ufile.setLID(this.UFList.Count);
            this.UFList.Add(ufile);
        }
        public void addExistingUFile(string ufstring) // For adding UnitFile from ufString, witch extracted from ProjectName.pdb
        {
            UnitFile ufile = new UnitFile(ufstring,this.unitDirPath,true);
            ufile.setLID(this.UFList.Count);
            this.UFList.Add(ufile);
        }
        public void deleteUnitFile(UnitFile ufile)
        {
            ufile.deleteFile();
            this.UFList.Remove(ufile);
            foreach (UnitFile unitfile in this.UFList)
                unitfile.setLID(UFList.IndexOf(unitfile));
        }
        public UnitFile getUnitFileByLID(int LID) { return this.UFList[LID]; }
        public string toString()
        {
            string str = "";                        
            str += "{";
            foreach (UnitFile ufile in UFList)
                str += ufile.toString();
            str += ":;";
            str += "}";
            str = Convert.ToString(UFList.Count) + ";" + str.Length + ";" + str;
            return str;
        }
        public void fromString(string f_string)
        {
            int begin = 0;
            string count_str = make_single_data_string(begin, f_string, ';'); begin += count_str.Length + 1;
            int count = Convert.ToInt16(count_str);
            string temp = make_single_data_string(begin, f_string, ';'); begin += temp.Length + 1;
            begin++; // skip 1-st level {
            for (int i = 0; i < count; i++)
            {
                string str = ""; char ch = '{'; 
                while (ch != '}')
                {                   
                    str += ch;
                    begin++;
                    ch = f_string.ElementAt(begin);
                }
                this.addExistingUFile(str);
                begin++;
            }
        }
    }
}