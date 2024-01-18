using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    class MembersList : ElemList<Member, String>
    {
        private static MembersList instance = null;
        private MembersList() { }
        public static MembersList Instance()
        {
            if (instance == null) instance = new MembersList();
            return instance;
        }

        public Member AddMember(Member m, string id)
        {
            Member mem;
            mem = AddElement(m, id);
            return mem;
        }

        public Member SearchMember(string id)
        {
            Member mem;
            mem = SearchElement(id);
            return mem;
        }

        public bool RemoveMember(Member m, string id)
        {
            bool result = RemoveElement(m, id);
            return result;
        }
    }
}
