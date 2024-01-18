using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    public class ElemInHall : AbstractElem
    {
        public AbstractElem AbsElem;
        public bool In_Hall;

        public AbstractElem ElemInHall_AbsElem { get => AbsElem; set => AbsElem = value; }
        public bool ElemInHall_In_Hall { get => In_Hall; set => In_Hall = value; }

        public ElemInHall(AbstractElem AbsElem)
        {
            this.AbsElem = AbsElem;
            this.In_Hall = true;
        }

        /*public string Accept(IAbstractElemVisitor viz)
        {
            return viz.Visit(this);
        }*/

        public override string ToString()
        {
            return "Element in sala adaugat";
        }
    }
}
