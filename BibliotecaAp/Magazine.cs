using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    public class Magazine : AbstractElem
    {
        public string Publisher;

        public string Magazine_Publisher { get => Publisher; set => Publisher = value; }

        public Magazine(string Title, string Publisher)
        {
            this.ID = Generate_Magazine_ID();
            this.Title = Title;
            this.Publisher = Publisher;
            this.BorrowedBy = null;
            this.ReturnDate = null;
            this.RetentedBy = null;
        }

        public string Generate_Magazine_ID()
        {
            if (ElemList<AbstractElem, String>.ElementsList.Count == 0) return "1";
            else
            {
                AbstractElem absElem = ElemList<AbstractElem, String>.ElementsList[ElemList<AbstractElem, String>.ElementsList.Count - 1];
                if (absElem is Book || absElem is Magazine) return (Convert.ToInt32(absElem.ID) + 1).ToString();
                if (absElem is ElemInHall)
                {
                    ElemInHall elemHall = (ElemInHall)absElem;
                    return (Convert.ToInt32(elemHall.AbsElem.ID) + 1).ToString();
                }
                else//if(absElem is ElemWithTax)
                {
                    ElemWithTax elemTax = (ElemWithTax)absElem;
                    if (elemTax.AbsElem is ElemInHall)
                    {
                        ElemInHall elemHall = (ElemInHall)elemTax.AbsElem;
                        return (Convert.ToInt32(elemHall.AbsElem.ID) + 1).ToString();
                    }
                    else
                    {
                        return (Convert.ToInt32(elemTax.AbsElem.ID) + 1).ToString();
                    }
                }
            }
        }

        /*public bool Compare(String id)
        {
            if (this.ID == id) return true;
            return false;
        }*/

        /*public void Accept(IAbstractElemVisitor viz, int type)
        {
            viz.Visit(this, type);
        }*/

        public override string ToString()
        {
            return "Revista adaugata";
        }
    }
}
