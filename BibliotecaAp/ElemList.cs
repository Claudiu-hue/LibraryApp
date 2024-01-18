using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    class ElemList<T, K> where T : ICompare<K>
    {
        public static List<T> ElementsList = new List<T>();

        public static T AddElement(T elem, K id)
        {
            ElementsList.Add(elem);
            return elem;
        }

        public static T SearchElement(K id)
        {
            foreach (T e in ElementsList)
            {
                if (e.Compare(id)) return e;
            }
            return default;
        }

        public static bool RemoveElement(T elem, K id)
        {
            ElementsList.Remove(elem);
            return true;
        }
    }
}
