using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalRU
{
    public class IO_methods
    {
        protected string make_single_data_string(int begin, string file_string, char separator)
        {
            string str = "";
            int end = get_end_of_single_string(begin, file_string, separator);
            if (end - begin > 0) { str = file_string.Substring(begin, end - begin); 
                return str; }
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
            return begin-1;
        }
        //protected string make_single_data_string(int begin, string f_string, string separator)
        //{
        //    bool first_simbol_condition = false;
        //    bool second_simbol_conditiom = false;
        //    char ch = ' ';
        //    for (int end = begin; first_simbol_condition && second_simbol_conditiom; end++)
        //    {
        //        ch = f_string.ElementAt(begin);
        //        if (ch == separator.ElementAt(0)) { first_simbol_condition = true; }
        //        if (ch != separator.ElementAt(0) && second_simbol_conditiom == false) { first_simbol_condition = false; }
        //        if (first_simbol_condition == true && ch == separator.ElementAt(1)) { second_simbol_conditiom = true; }
        //        if (first_simbol_condition == true && ch != separator.ElementAt(1)) { first_simbol_condition = false; }
        //    }
        //        return f_string.Substring(begin,get_end_of_single_string-begin);
        //}
    }
}
