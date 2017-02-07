using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace PersonalRU
{
    public class UnitList : IO_methods
    {
        List<Unit> UList = new List<Unit>();
        private string unitListDirPath = ""; // .../ Project/ $data or $temp
        public UnitList(string unitListDirPath, bool isNewProj, string UnitListFileString, int count)// default isNewProj = false, UnitListFileString = "", count = 0
        {
            if (isNewProj) this.unitListDirPath = unitListDirPath;
            else { this.unitListDirPath = unitListDirPath; this.fromString(UnitListFileString, count); }
        }
        public List<Unit> getUnitList() { return this.UList; }
        public Unit getUnitByLID(int LID) { return this.UList[LID]; }
        public Unit getUnitByID(string _ID){ return UList.Find(Unit => Unit.ID == _ID);}
        public void setUnitListDirPath(string unitListDirPath) 
        {
            this.unitListDirPath = unitListDirPath;
            foreach (Unit unit in this.UList)
                unit.setUnitListDirPath(unitListDirPath);
        }
        public void add(string unitString)
        {
            int LID = this.UList.Count;
            Unit unit = new Unit(unitString, this.unitListDirPath, LID);
            //unit.setLID(this.UList.Count);
            this.UList.Add(unit);
        }
        public void delete(Unit unit)
        {
            //unit.delete();
            this.UList[unit.getLID()].delete();
            this.UList.Remove(unit);
            foreach (Unit un in this.UList)
                un.setLID(UList.IndexOf(un));
        }
        public void change(string unitString){this.UList[Convert.ToInt16(findFromString("LID",unitString))].FromString(unitString);}
        public void addFile(string sourceFilePath, Unit unit) { this.UList[unit.getLID()].AddFile(sourceFilePath); }
        public void deleteFile(UnitFile ufile, Unit unit) { this.UList[unit.getLID()].deleteFile(ufile); }
        public UnitFile getUnitFileByLID(Unit unit, int LID) { return this.UList[unit.getLID()].getUnitFileByLID(LID); }
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
        public string toString()
        {
            string UListString = "{";
            foreach (Unit unit in this.UList)
                UListString += unit.toString();
            UListString += "}";
            string str = this.UList.Count + ";" + UListString.Length + ";" + UListString;
            return str;
        }
        public void fromString(string fstring, int count)
        {
            int begin = 0;
            for (int i = 0; i < count; i++)
            {
                string length_str = make_single_data_string(begin, fstring, ';');
                int length = Convert.ToInt16(length_str);
                begin += length_str.Length + 1;
                string str = fstring.Substring(begin, length);                
                begin += str.Length;
                this.add(str);
            }
        }
        public void dispose()
        {
            foreach (Unit unit in this.UList)
                unit.dispose();
        }
    }
}