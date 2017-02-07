using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalRU
{
    public class Vacancy : IO_methods
    {
        public string    JobTitle = "";                             // Наименование должности
        public ItemsList EducationRequirements = new ItemsList(),   // Требования к образованию              
                         WorkExperience = new ItemsList(),          // Стаж работы
                         DescriptionOfWork = new ItemsList();       // Описание трудовой деятельности
        public string    Location = "",                             // Место работы
                         TimeTable = "",                            // Режим/график работы
                         WageLevel = "";                            // Уровень зарплаты
        public ItemsList LanguageRequirements = new ItemsList(),    // Требования к знанию языков
                         CitizenshipRequirements = new ItemsList(); // Требования к гражданству
        public string    ProductionSite = "";                       // Участок производства
        
        public int    Responses = 0;                // Отклики
        public string UpdatedDate = "1.01.15";      // Дата последнего обновления

        public int LID; // ID вакансии в списке вакансий аккаунта
        public string ID = "";  // глобальный ID
        public string basedUnitID = ""; // ID должности, на основе которой создана вакансия.

        
        public Vacancy()
        {
            LID = 1001;
            this.JobTitle = "Не указано";
            this.EducationRequirements.Add("Не указано");
            this.WorkExperience.Add("Не указано");
            this.DescriptionOfWork.Add("Не указано");
            this.Location = "Не указано";
            this.TimeTable = "Не указано";
            this.WageLevel = "Не указано";
            this.LanguageRequirements.Add("Не указано");
            this.CitizenshipRequirements.Add("Не указано");
            this.ProductionSite = "Не указано";
        }
        #region FILE
        public Vacancy(string f_string){
            int begin = 0;
            string str_name = " ";
            
            while (str_name != "") {
                str_name = make_single_data_string(begin, f_string, ':'); begin += str_name.Length;
                if (str_name == "LID")
                {
                    string str = make_single_data_string(begin + 1, f_string, ';'); begin += str.Length + 2;
                    this.LID = Convert.ToInt16(str); Console.WriteLine("LID = " + this.LID);
                    str_name = " ";
                }
                if (str_name == "ID")
                {
                    string str = make_single_data_string(begin + 1, f_string, ';'); begin += str.Length + 2;
                    this.ID = str;
                    str_name = " ";
                }
                if (str_name == "buID")
                {
                    string str = make_single_data_string(begin + 1, f_string, ';'); begin += str.Length + 2;
                    this.basedUnitID = str;
                    str_name = " ";
                }
                if (str_name == "JobTitle")
                {
                    string str = make_single_data_string(begin + 1, f_string, ';'); begin += str.Length + 2;
                    this.JobTitle = str; Console.WriteLine("JobTitle = " + this.JobTitle);
                    str_name = " ";
                }
                if (str_name == "EducationRequirements")
                {
                    string s_num_of_items = make_single_data_string(begin + 1, f_string, ';'); begin += s_num_of_items.Length + 2;
                    int num_of_items = Convert.ToInt16(s_num_of_items);
                    string[] items_arr = new string[num_of_items];

                    for (int i = 0; i < num_of_items; i++)
                    {
                        items_arr[i] = make_single_data_string(begin, f_string, ';'); begin += items_arr[i].Length + 1;                        
                    }
                    this.EducationRequirements = new ItemsList(items_arr, num_of_items);
                    Console.WriteLine("EducationRequirements:");
                    this.EducationRequirements.Write();
                    str_name = " ";
                }
                if (str_name == "WorkExperience")
                {
                    string s_num_of_items = make_single_data_string(begin + 1, f_string, ';'); begin += s_num_of_items.Length + 2;
                    int num_of_items = Convert.ToInt16(s_num_of_items);
                    string[] items_arr = new string[num_of_items];

                    for (int i = 0; i < num_of_items; i++)
                    {
                        items_arr[i] = make_single_data_string(begin, f_string, ';'); begin += items_arr[i].Length + 1;
                    }
                    this.WorkExperience = new ItemsList(items_arr, num_of_items);
                    Console.WriteLine("WorkExperience:");
                    this.WorkExperience.Write();
                    str_name = " ";
                }
                if (str_name == "DescriptionOfWork")
                {
                    string s_num_of_items = make_single_data_string(begin + 1, f_string, ';'); begin += s_num_of_items.Length + 2;
                    int num_of_items = Convert.ToInt16(s_num_of_items);
                    string[] items_arr = new string[num_of_items];

                    for (int i = 0; i < num_of_items; i++)
                    {
                        items_arr[i] = make_single_data_string(begin, f_string, ';'); begin += items_arr[i].Length + 1;
                    }
                    this.DescriptionOfWork = new ItemsList(items_arr, num_of_items);
                    Console.WriteLine("DescriptionOfWork:");
                    this.DescriptionOfWork.Write();
                    str_name = " ";
                }
                if (str_name == "Location")
                {
                    string str = make_single_data_string(begin + 1, f_string, ';'); begin += str.Length + 2;
                    this.Location = str; Console.WriteLine("Location = " + this.Location);
                    str_name = " ";
                }
                if (str_name == "TimeTable")
                {
                    string str = make_single_data_string(begin + 1, f_string, ';'); begin += str.Length + 2;
                    this.TimeTable = str; Console.WriteLine("TimeTable = " + this.TimeTable);
                    str_name = " ";
                }
                if (str_name == "WageLevel")
                {
                    string str = make_single_data_string(begin + 1, f_string, ';'); begin += str.Length + 2;
                    this.WageLevel = str; Console.WriteLine("WageLevel = " + this.WageLevel);
                    str_name = " ";
                }
                if (str_name == "LanguageRequirements")
                {
                    string s_num_of_items = make_single_data_string(begin + 1, f_string, ';'); begin += s_num_of_items.Length + 2;
                    int num_of_items = Convert.ToInt16(s_num_of_items);
                    string[] items_arr = new string[num_of_items];

                    for (int i = 0; i < num_of_items; i++)
                    {
                        items_arr[i] = make_single_data_string(begin, f_string, ';'); begin += items_arr[i].Length + 1;
                    }
                    this.LanguageRequirements = new ItemsList(items_arr, num_of_items);
                    Console.WriteLine("LanguageRequirements:");
                    this.LanguageRequirements.Write();
                    str_name = " ";
                }
                if (str_name == "CitizenshipRequirements")
                {
                    string s_num_of_items = make_single_data_string(begin + 1, f_string, ';'); begin += s_num_of_items.Length + 2;
                    int num_of_items = Convert.ToInt16(s_num_of_items);
                    string[] items_arr = new string[num_of_items];

                    for (int i = 0; i < num_of_items; i++)
                    {
                        items_arr[i] = make_single_data_string(begin, f_string, ';'); begin += items_arr[i].Length + 1;
                    }
                    this.CitizenshipRequirements = new ItemsList(items_arr, num_of_items);
                    Console.WriteLine("CitizenshipRequirements:");
                    this.CitizenshipRequirements.Write();
                    str_name = " ";
                }
                if (str_name == "ProductionSite")
                {
                    string str = make_single_data_string(begin + 1, f_string, ';'); begin += str.Length + 2;
                    this.ProductionSite = str; Console.WriteLine("ProductionSite = " + this.ProductionSite);
                    str_name = " ";
                }
            }
        }
        public string getStringForFile()
        {
            string str = "";
            str = "{"
                + "LID:" + this.LID + ";"
                + "ID:" + this.ID + ";"
                + "buID:" + this.basedUnitID + ";"
                + "JobTitle:" + this.JobTitle + ";"
                + "EducationRequirements:"+ this.EducationRequirements.count + ";" + this.EducationRequirements.GetFullString()
                + "WorkExperience:" + this.WorkExperience.count + ";" + this.WorkExperience.GetFullString()
                + "DescriptionOfWork:" + this.DescriptionOfWork.count + ";" + this.DescriptionOfWork.GetFullString()
                + "Location:" + this.Location + ";"
                + "TimeTable:" + this.TimeTable + ";"
                + "WageLevel:" + this.WageLevel + ";"
                + "LanguageRequirements:" + this.LanguageRequirements.count + ";" + this.LanguageRequirements.GetFullString()
                + "CitizenshipRequirements:" + this.CitizenshipRequirements.count + ";" + this.CitizenshipRequirements.GetFullString()
                + "ProductionSite:" + this.ProductionSite + ";"
                + ":}";
            return str;
        }
        #endregion
    }
}
