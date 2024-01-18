using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    public class Transaction : ICompare<String>
    {
        public string ID;
        public DateTime Date;
        public string Type;
        public string Member_Name;
        public string Book_Title;

        public string Transaction_ID { get => ID; set => ID = value; }
        public DateTime Transaction_Date { get => Date; set => Date = value; }
        public string Transaction_Type { get => Type; set => Type = value; }
        public string Transaction_Member_Name { get => Member_Name; set => Member_Name = value; }
        public string Transaction_Book_Title { get => Book_Title; set => Book_Title = value; }

        public Transaction(string ID, DateTime Date, string Type, string Member_Name, string Book_Title)
        {
            //CreateID();
            this.ID = ID;
            this.Date = Date;
            this.Type = Type;
            this.Member_Name = Member_Name;
            this.Book_Title = Book_Title;
        }

        /*public void CreateID()
        {
            this.ID = Transaction_ID;
        }*/

        public bool Compare(String id)
        {
            if (this.ID == id) return true;
            return false;
        }

        public override string ToString()
        {
            return "Tranzactie adaugata";
        }
    }
}
