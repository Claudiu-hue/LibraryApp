using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    public class AbstractElem : ICompare<String>
    {
        public string ID;
        public string Title;
        public Member BorrowedBy;
        public DateTime? ReturnDate;
        public Member RetentedBy;

        public string AbstractElem_ID { get => ID; set => ID = value; }
        public string AbstractElem_Title { get => Title; set => Title = value; }
        public Member AbstractElem_BorrowedBy { get => BorrowedBy; set => BorrowedBy = value; }
        public DateTime? AbstractElem_ReturnDate { get => ReturnDate; set => ReturnDate = value; }
        public Member AbstractElem_RetentedBy { get => RetentedBy; set => RetentedBy = value; }

        /*public AbstractElem(string ID, string Title)
        {
            this.ID = ID;
            this.Title = Title;
            this.BorrowedBy = "No one";
            this.ReturnDate = "-";
        }*/

        public string Accept(IAbstractElemVisitor viz, int type)
        {
            return viz.Visit(this, type);
        }

        public bool Compare(String id)
        {
            if (this.ID == id) return true;
            if (this is ElemInHall)
            {
                ElemInHall elemHall = (ElemInHall)this;
                if (elemHall.AbsElem.ID == id) return true;
            }
            if (this is ElemWithTax)
            {
                ElemWithTax elemTax = (ElemWithTax)this;
                if (elemTax.AbsElem is ElemInHall)
                {
                    ElemInHall elemHall = (ElemInHall)elemTax.AbsElem;
                    if (elemHall.AbsElem.ID == id) return true;
                }
                else
                {
                    if (elemTax.AbsElem.ID == id) return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return "Element added succesfully!";
        }
    }
}
