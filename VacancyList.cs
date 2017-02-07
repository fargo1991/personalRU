using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PersonalRU
{
    public class VacancyList: IO_methods
    {
        List<SRL.Vacancy> VacList = new List<SRL.Vacancy>();
        public VacancyList() { }

        public SRL.Vacancy getVacancyByLID(int LID) { return VacList[LID]; }
        public SRL.Vacancy getVacancyByID(string ID){ 
            return VacList.Find(Vacancy => Vacancy.ID == ID);
        }
        public List<SRL.Vacancy> getList() { return this.VacList; }
        public void add(SRL.Vacancy vac){
            vac.LID = this.VacList.Count;
            this.VacList.Add(vac);
        }
        public void delete(SRL.Vacancy vac) { 
            this.VacList.Remove(vac);
            foreach (SRL.Vacancy v in this.VacList)
                v.LID = VacList.IndexOf(v);
        }
        public void exchange(SRL.Vacancy vac) { this.VacList[vac.LID] = vac; }
        #region FILE
        public VacancyList(string file_string, int num_of_vac)
        {
            int begin = 0;
            string vac_string = "";

            char ch = ' ';
            for (int counter = 0; counter < num_of_vac; counter++)
            {
                int end = 0;
                while(ch != '{'){
                    ch = file_string.ElementAt(begin);
                    begin++;
                }
                while (ch != '}'){
                    ch = file_string.ElementAt(begin + end);
                    end++;
                }
                Console.WriteLine("length = " + file_string.Length);
                vac_string = file_string.Substring(begin, end-1);
                SRL.Vacancy vac = new SRL.Vacancy(vac_string);
                this.add(vac);
            }
            Console.WriteLine("ReadingFile complete. VacancyList");
        }
        public string toString()
        {
            string str = "";
            for (int i = 0; i < this.VacList.Count; i++)
                str += this.VacList[i].getStringForFile();
            str = this.VacList.Count + ";" + str.Length + ";" + "{" + str + "}";
            return str;
        }
        #endregion
    }
}