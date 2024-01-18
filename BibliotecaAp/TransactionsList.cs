using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    class TransactionsList : ElemList<Transaction, String>
    {
        private static TransactionsList instance = null;
        public static TransactionsList Instance()
        {
            if (instance == null) instance = new TransactionsList();
            return instance;
        }

        public Transaction AddTransaction(Transaction t, string id)
        {
            Transaction tr;
            tr = AddElement(t, id);
            return tr;
        }

        public Transaction SearchTransaction(string id)
        {
            Transaction tr;
            tr = SearchElement(id);
            return tr;
        }

        public bool RemoveTransaction(Transaction t, string id)
        {
            bool result = RemoveElement(t, id);
            return result;
        }
    }
}
