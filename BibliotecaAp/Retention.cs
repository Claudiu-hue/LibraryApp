using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    class Retention : ICompare<String>
    {
        public string ID;
        public Member Member_ID;
        public AbstractElem Elem_ID;
        public DateTime PlacedOnDate;

        public string Retention_ID { get => ID; set => ID = value; }
        public Member Retention_Member_ID { get => Member_ID; set => Member_ID = value; }
        public AbstractElem Retention_Elem_ID { get => Elem_ID; set => Elem_ID = value; }
        public DateTime Retention_PlacedOnDate { get => PlacedOnDate; set => PlacedOnDate = value; }

        public Retention(string ID, Member Member_ID, AbstractElem Elem_ID)
        {
            //CreateID();
            this.ID = ID;
            this.Member_ID = Member_ID;
            this.Elem_ID = Elem_ID;
            this.PlacedOnDate = DateTime.Now;
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
            return "Retinere adaugata";
        }
    }
}
