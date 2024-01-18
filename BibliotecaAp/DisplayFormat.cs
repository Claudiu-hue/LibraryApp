using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    class DisplayFormat : IAbstractElemVisitor
    {
        public string Visit(Book book, int type)
        {
            switch (type)
            {
                case 1: // ADD Book
                    {
                        return "[RESULT] Book ADDED succesfully! [Info:] ID: " + book.ID + ", Title: " + book.Title + ", Author: " + book.Author;
                    }
                case 2: // SEARCH Book
                    {
                        return "[RESULT] Book FOUND! [Info:] ID: " + book.ID + ", Title: " + book.Title + ", Author: " + book.Author + ".";
                    }
                case 3: // Show Books list
                    {
                        string mem_desc;
                        if (book.BorrowedBy == null) mem_desc = "No one";
                        else mem_desc = "[ID: " + book.BorrowedBy.ID + "] " + book.BorrowedBy.Name;
                        return "[ID: " + book.ID + "] | Element: Book | Title: " + book.Title + " | Author: " + book.Author + " | Borrowed by: " + mem_desc + " | Return date: " + (book.BorrowedBy == null ? ("None") : ("" + book.ReturnDate));
                    }
            }
            return "";
        }
        public string Visit(Magazine magazine, int type)
        {
            switch (type)
            {
                case 1: // ADD Magazine
                    {
                        return "[RESULT] Magazine ADDED succesfully! [Info:] ID: " + magazine.ID + ", Title: " + magazine.Title + ", Publisher: " + magazine.Publisher + ".";
                    }
                case 2: // SEARCH Book
                    {
                        return "[RESULT] Magazine FOUND! [Info:] ID: " + magazine.ID + ", Title: " + magazine.Title + ", Publisher: " + magazine.Publisher + ".";
                    }
                case 3: // Show Magazines list
                    {
                        string mem_desc;
                        if (magazine.BorrowedBy == null) mem_desc = "No one";
                        else mem_desc = "[ID: " + magazine.BorrowedBy.ID + "] " + magazine.BorrowedBy.Name;
                        return "[ID: " + magazine.ID + "] | Element: Magazine | Title: " + magazine.Title + " | Publisher: " + magazine.Publisher + " | Borrowed by: " + mem_desc + " | Return date: " + (magazine.BorrowedBy == null ? ("None") : ("" + magazine.ReturnDate));
                    }
            }
            return "";
        }

        public string Visit(ElemInHall elemHall, int type)
        {
            Book bk;
            if (elemHall.AbsElem is Book)
            {
                bk = (Book)elemHall.AbsElem;
                //Console.WriteLine("[RESULT] Book ADDED succesfully! [Info:] ID: " + bk.ID + ", Title: " + bk.Title + ", Author: " + bk.Author + ", Tax: " + elemTax.Tax + ".");
                return Visit(bk, type) + " | Reading: only in hall";
            }
            return "";
        }

        public string Visit(ElemWithTax elemTax, int type)
        {
            if (elemTax.AbsElem is ElemInHall)
            {
                return Visit((ElemInHall)elemTax.AbsElem, type) + " | Tax: " + elemTax.Tax;
            }
            else
            {
                if (elemTax.AbsElem is Book)
                {
                    Book bk;
                    bk = (Book)elemTax.AbsElem;
                    //Console.WriteLine("[RESULT] Book ADDED succesfully! [Info:] ID: " + bk.ID + ", Title: " + bk.Title + ", Author: " + bk.Author + ", Tax: " + elemTax.Tax + ".");
                    return Visit(bk, type) + " | Reading: can be borrowed | Tax: " + elemTax.Tax;
                }
            }
            return "";
        }

        public string Visit(AbstractElem absElem, int type)
        {
            string result = "";
            if (absElem is Book) result = Visit((Book)absElem, type) + " | Reading: can be borrowed | Tax: 0";
            if (absElem is Magazine) result = Visit((Magazine)absElem, type) + " | Reading: can be borrowed | Tax: 0";
            if (absElem is ElemInHall) result = Visit((ElemInHall)absElem, type) + " | Tax: 0";
            if (absElem is ElemWithTax) result = Visit((ElemWithTax)absElem, type);
            return result;
        }
    }
}
