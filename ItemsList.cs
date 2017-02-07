using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalRU
{
    public class ItemsList
    {
        string[] array = new string[100];
        public int count = 0;
        public ItemsList() {array[count] = "Nothing items in ItemsList"; }
        public ItemsList(string item) { this.Add(item); }
        public ItemsList(string[] arr, int count_of_items)
        {
            for (int i = 0; i < count_of_items; i++)
                array[i] = arr[i];
            this.count = count_of_items;
        }
        public string Get(int num) { return array[num];}
        public string GetFullString()
        {
            string str = "";
            for (int i = 0; i < count; i++)
            {
                str += array[i] + ";";
            }
            return str;
        }
        public void Add(string item)
        {
            array[count] = item;
            count++;
        }
        public void Remove(int num)
        {
            for (int i = num; i < count; i++)
                if (i < count) { array[i] = array[i + 1]; }
                else { break; }
            count--;
        }
        public void Write()
        {
            for (int i=0;i<this.count;i++)
                Console.WriteLine(this.array[i]);
        }
    }
}
