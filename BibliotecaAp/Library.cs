using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BibliotecaAp
{
   public class Library
    {
        private static Library instance = null;
        private Library() { }
        public static Library Instance()
        {
            if (instance == null) instance = new Library();
            return instance;
        }

        static MembersList membersList = MembersList.Instance();
        static Catalog catalog = Catalog.Instance();
        static TransactionsList transactionsList = TransactionsList.Instance();
        static RetentionsList retentionsList = RetentionsList.Instance();

        private static int LateReturn_Penalty = 1;

        public static int GetPenaltyValue()
        {
            return LateReturn_Penalty;
        }

        public static bool SetPenaltyValue(int value)
        {
            if (value >= 1 && value <= 10)
            {
                LateReturn_Penalty = value;
                return true;
            }
            else
                return false;
        }

        public static Member Add_Member(string Name, string Address, string Phone)
        {
            Member m = new Member(Name, Address, Phone);
            Member added = membersList.AddMember(m, m.ID);
            return m;
        }

        public static Member Search_Member(string ID)
        {
            Member m = membersList.SearchMember(ID);
            return m;
        }

        public static int Delete_Member(string ID)
        {
            Member m = membersList.SearchMember(ID);
            if (m.BorrowedElem != null) return 1; // member has a borrowed book
            if (m.RetentedElem != null) return 2; // member has a retented book
            bool result = membersList.RemoveMember(m, m.ID);
            if (!result) return 3; // member could not be removed
            return 0;
        }

        /*public static Book AdaugaCarte(string ID, string Title, string Author)
        {
            Book b = new Book(ID, Title, Author);
            Book added = catalog.AddBook(b, b.ID);
            return b;
        }*/

        public static AbstractElem AddElement(string type, string Title, string Author, string Publisher, int Tax) // type: 1 = Book, 2 = Magazine
        {
            AbstractElemFactory absElemFactory = AbstractElemFactory.Instance();
            AbstractElem absElem = absElemFactory.CreateElement(type, Title, Author, Publisher, Tax);
            AbstractElem added = catalog.AddElem(absElem, absElem.ID);
            return absElem;
        }

        public static AbstractElem SearchElement(string ID)
        {
            AbstractElem absElem = catalog.SearchElem(ID);
            return absElem;
        }

        public static int DeleteElement(string ID)
        {
            AbstractElem absElem = catalog.SearchElem(ID);
            if (absElem.BorrowedBy != null) return 1; // book is currently borrowed
            if (absElem.RetentedBy != null) return 2; // book is currently retented
            bool result = catalog.RemoveElem(absElem, absElem.ID);
            if (!result) return 3; // book could not be removed
            return 0;
        }

        public static int BorrowElement(string m_id, string b_id)
        {
            Member mem = Search_Member(m_id);
            AbstractElem absElem = SearchElement(b_id);
            if (mem.BorrowedElem != null) return 1; // member already has a borrowed book
            if (mem.RetentedElem != null) return 2; // member already has a retented book
            if (absElem.BorrowedBy != null) return 3; // book is already borrowed
            if (mem.Penalties > 0) return 4; // member have penalties
            mem.BorrowedElem = absElem;
            mem.ReturnDateElem = (DateTime.Now.AddDays(30));
            absElem.BorrowedBy = mem;
            absElem.ReturnDate = (DateTime.Now.AddDays(30));
            Transaction tr = new Transaction(((ElemList<Transaction, String>.ElementsList.Count) + 1).ToString(), (DateTime.Now), "Borrow", mem.Name, absElem.Title);
            Transaction added = transactionsList.AddTransaction(tr, tr.ID);
            return 0;
        }

        public static int ReturnElement(string m_id)
        {
            Member mem = Search_Member(m_id);
            AbstractElem absElem = mem.BorrowedElem;
            if (mem.BorrowedElem == null) return -2; // member does not have a borrowed book
            Transaction tr = new Transaction(((ElemList<Transaction, String>.ElementsList.Count) + 1).ToString(), (DateTime.Now), "Return", mem.Name, mem.BorrowedElem.Title);
            Transaction added = transactionsList.AddTransaction(tr, tr.ID);
            if (DateTime.Now > mem.ReturnDateElem) // the ReturnDate has passed
            {
                TimeSpan date_diff = (TimeSpan)(DateTime.Now - mem.ReturnDateElem);
                int days_diff = (int)(date_diff.TotalDays); // no of elapsed days
                int penalties = days_diff * LateReturn_Penalty; // penalties calculation - days elapsed * per day late return penalty
                if (absElem.RetentedBy != null) penalties *= 2; // book had a retention, so the penalties got doubled
                mem.Penalties = penalties;
            }
            if (mem.BorrowedElem.RetentedBy != null) // book is retented by someone
            {
                int borrow_to = Convert.ToInt32(mem.BorrowedElem.RetentedBy.ID);
                int borrow_book = BorrowElement(mem.BorrowedElem.RetentedBy.ID, mem.BorrowedElem.ID);
                switch (borrow_book)
                {
                    case 1: return -3; // member already has a borrowed book
                    case 2: return -4; // member already has a retented book
                    case 3: return -5; // book is already borrowed
                    default: // book borrowed
                        {
                            Transaction tr2 = new Transaction(((ElemList<Transaction, String>.ElementsList.Count) + 1).ToString(), (DateTime.Now), "Borrow", mem.BorrowedElem.RetentedBy.Name, mem.BorrowedElem.Title);
                            Transaction added2 = transactionsList.AddTransaction(tr2, tr2.ID);
                            for (int i = 0; i < ElemList<Retention, String>.ElementsList.Count; i++)
                            {
                                if (ElemList<Retention, String>.ElementsList[i].Member_ID == mem.BorrowedElem.RetentedBy)
                                {
                                    bool remove_ret = retentionsList.RemoveRetention(ElemList<Retention, String>.ElementsList[i], ElemList<Retention, String>.ElementsList[i].ID); // remove retention from retentionslist
                                    //if (remove_ret) { }
                                    //else { }
                                }
                            }
                            mem.BorrowedElem.RetentedBy = null;
                            mem.BorrowedElem.RetentedBy.RetentedElem = null;
                            return borrow_to; // book borrowed
                        }
                }
            }
            else
            {
                mem.BorrowedElem.BorrowedBy = null;
                mem.BorrowedElem.ReturnDate = null;
            }
            mem.BorrowedElem = null;
            mem.ReturnDateElem = null;
            return -1;
        }

        public static int RetentElement(string m_id, string b_id)
        {
            Member mem = Search_Member(m_id);
            AbstractElem absElem = SearchElement(b_id);
            if (mem.BorrowedElem != null) return 1; // member already has a book borrowed
            if (mem.RetentedElem != null) return 2; // member already has a book retented
            if (absElem.BorrowedBy == null) return 3; // book is not borrowed
            if (absElem.RetentedBy != null) return 4; // book is already retented
            if (mem.Penalties > 0) return 5; // member has penalties
            mem.RetentedElem = absElem;
            absElem.RetentedBy = mem;
            Retention ret = new Retention(((ElemList<Retention, String>.ElementsList.Count) + 1).ToString(), mem, absElem);
            Retention added = retentionsList.AddRetention(ret, ret.ID);
            return 0;
        }

        public static int CancelRetentElement(string m_id)
        {
            Member mem = Search_Member(m_id);
            if (mem.RetentedElem == null) return 1; // member does not have a retented book
            for (int i = 0; i < ElemList<Retention, String>.ElementsList.Count; i++)
            {
                if (ElemList<Retention, String>.ElementsList[i].Member_ID == mem)
                {
                    bool remove_ret = retentionsList.RemoveRetention(ElemList<Retention, String>.ElementsList[i], ElemList<Retention, String>.ElementsList[i].ID); // remove retention from retentionslist
                    if (remove_ret) { }
                    else { }
                }
            }
            mem.RetentedElem.RetentedBy = null;
            mem.RetentedElem = null;
            return 0;
        }

        public static string ShowAllMembers()
        {
            string book_desc;
            string book_return;
            string result = "";
            for (int i = 0; i < ElemList<Member, String>.ElementsList.Count; i++)
            {
                if (ElemList<Member, String>.ElementsList[i].BorrowedElem == null) book_desc = "None";
                else book_desc = "[ID: " + ElemList<Member, String>.ElementsList[i].BorrowedElem.ID + "] " + ElemList<Member, String>.ElementsList[i].BorrowedElem.Title;
                if (ElemList<Member, String>.ElementsList[i].ReturnDateElem == null) book_return = "None";
                else book_return = ElemList<Member, String>.ElementsList[i].ReturnDateElem.ToString();
                //Console.WriteLine("[" + ElemList<Member, String>.ElementsList[i].ID + "] | " + ElemList<Member, String>.ElementsList[i].Name + " | " + ElemList<Member, String>.ElementsList[i].Address + " | " + ElemList<Member, String>.ElementsList[i].Phone + " | " + book_desc + " | " + ElemList<Member, String>.ElementsList[i].ReturnDateElem + ElemList<Member, String>.ElementsList[i].Penalties + ".");

                result += "[" + ElemList<Member, String>.ElementsList[i].ID + "] | " + ElemList<Member, String>.ElementsList[i].Name + " | " + ElemList<Member, String>.ElementsList[i].Address + " | " + ElemList<Member, String>.ElementsList[i].Phone + " | " + book_desc + " | " + book_return + " | " + ElemList<Member, String>.ElementsList[i].Penalties + ".";
                if (i < ElemList<Member, String>.ElementsList.Count - 1) result += "\n";
            }
            return result;
        }
        public static string ShowCatalog()
        {
            string result = "";
            foreach (AbstractElem absElem in ElemList<AbstractElem, String>.ElementsList)
            {
                result += absElem.Accept(new DisplayFormat(), 3) + "\n";
            }
            return result;
        }
        public static string ShowTransactions()
        {
            string result = "";
            for (int i = 0; i < ElemList<Transaction, String>.ElementsList.Count; i++)
            {
                result += "[" + ElemList<Transaction, String>.ElementsList[i].ID + "] | " + ElemList<Transaction, String>.ElementsList[i].Date + " | " + ElemList<Transaction, String>.ElementsList[i].Type + " | " + ElemList<Transaction, String>.ElementsList[i].Member_Name + " | " + ElemList<Transaction, String>.ElementsList[i].Book_Title + ".";
                if (i < ElemList<Transaction, String>.ElementsList.Count - 1) result += "\n";
            }
            return result;
        }

        public static string ShowRetentions()
        {
            string result = "";
            for (int i = 0; i < ElemList<Retention, String>.ElementsList.Count; i++)
            {
                result += "[" + ElemList<Retention, String>.ElementsList[i].ID + "] | " + ElemList<Retention, String>.ElementsList[i].Member_ID.Name + " | " + ElemList<Retention, String>.ElementsList[i].Elem_ID.Title + " | " + ElemList<Retention, String>.ElementsList[i].PlacedOnDate + ".";
                if (i < ElemList<Retention, String>.ElementsList.Count - 1) result += "\n";
            }
            return result;
        }
    }
}
