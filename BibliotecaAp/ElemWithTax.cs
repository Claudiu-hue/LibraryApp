using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    public class ElemWithTax : AbstractElem
    {
        public AbstractElem AbsElem;
        public int Tax;

        public AbstractElem ElemWithTax_AbsElem { get => AbsElem; set => AbsElem = value; }
        public int ElemWithTax_Tax { get => Tax; set => Tax = value; }

        public ElemWithTax(AbstractElem AbsElem, int Tax)
        {
            this.AbsElem = AbsElem;
            this.Tax = Tax;
        }

        /*public string Accept(IAbstractElemVisitor viz)
        {
            return viz.Visit(this);
        }*/

        public override string ToString()
        {
            return "Element cu taxa adaugat";
        }
    }
}
