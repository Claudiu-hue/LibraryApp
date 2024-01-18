using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    internal class AbstractElemFactory
    {
        private static AbstractElemFactory instance;
        private AbstractElemFactory() { }
        public static AbstractElemFactory Instance()
        {
            if (instance == null) instance = new AbstractElemFactory();
            return instance;
        }

        public AbstractElem CreateElement(string type, string title, string author, string publisher, int tax)
        {
            switch (type)
            {
                case "Book": // Book
                    {
                        return new Book(title, author);
                    }
                case "Book_Hall":
                    {
                        return new ElemInHall(new Book(title, author));
                    }
                case "Book_Tax": // Book Tax
                    {
                        return new ElemWithTax(new Book(title, author), tax);
                    }
                case "Book_Hall_Tax":
                    {
                        return new ElemWithTax(new ElemInHall(new Book(title, author)), tax);
                    }
                case "Magazine": // Magazine
                    {
                        return new Magazine(title, publisher);
                    }
                case "Magazine_Hall":
                    {
                        return new ElemInHall(new Magazine(title, publisher));
                    }
                case "Magazine_Tax":
                    {
                        return new ElemWithTax(new Magazine(title, publisher), tax);
                    }
                case "Magazine_Hall_Tax":
                    {
                        return new ElemWithTax(new ElemInHall(new Magazine(title, publisher)), tax);
                    }
                default: return null;
            }
        }
    }
}
