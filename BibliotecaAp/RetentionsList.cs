using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    class RetentionsList : ElemList<Retention, String>
    {
        private static RetentionsList instance = null;
        public static RetentionsList Instance()
        {
            if (instance == null) instance = new RetentionsList();
            return instance;
        }

        public Retention AddRetention(Retention r, string id)
        {
            Retention ret;
            ret = AddElement(r, id);
            return ret;
        }

        public Retention SearchRetention(string id)
        {
            Retention ret;
            ret = SearchElement(id);
            return ret;
        }

        public bool RemoveRetention(Retention ret, string id)
        {
            bool result = RemoveElement(ret, id);
            return result;
        }
    }
}
