using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    public interface IAbstractElemVisitor
    {
        public string Visit(Book book, int type);
        public string Visit(Magazine magazine, int type);
        public string Visit(ElemInHall elemHall, int type);
        public string Visit(ElemWithTax elemTax, int type);
        public string Visit(AbstractElem absElem, int type);
    }
}
