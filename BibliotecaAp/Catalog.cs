using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    class Catalog : ElemList<AbstractElem, String>
    {
        private static Catalog instance;
        private Catalog() { }
        public static Catalog Instance()
        {
            if (instance == null) instance = new Catalog();
            return instance;
        }

        public AbstractElem AddElem(AbstractElem absElem, string id)
        {
            AbstractElem absElement;
            absElement = AddElement(absElem, id);
            return absElement;
        }

        public AbstractElem SearchElem(string id)
        {
            AbstractElem absElement;
            absElement = SearchElement(id);
            return absElement;
        }

        public bool RemoveElem(AbstractElem absElem, string id)
        {
            bool result = RemoveElement(absElem, id);
            return result;
        }
    }
}
