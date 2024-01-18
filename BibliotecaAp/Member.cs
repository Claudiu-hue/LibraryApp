using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaAp
{
    public class Member : ICompare<String>
    {
        public string ID;
        public string Name;
        public string Address;
        public string Phone;
        public AbstractElem BorrowedElem;
        public DateTime? ReturnDateElem;
        public AbstractElem RetentedElem;
        public int Penalties;

        public string Member_ID { get => ID; set => ID = value; }
        public string Member_Name { get => Name; set => Name = value; }
        public string Member_Address { get => Address; set => Address = value; }
        public string Member_Phone { get => Phone; set => Phone = value; }
        public AbstractElem Member_BorrowedElem { get => BorrowedElem; set => BorrowedElem = value; }
        public DateTime? Member_ReturnDateElem { get => ReturnDateElem; set => ReturnDateElem = value; }
        public AbstractElem Member_RetentedElem { get => RetentedElem; set => RetentedElem = value; }
        public int Member_Penalties { get => Penalties; set => Penalties = value; }

        public Member(string Name, string Address, string Phone)
        {
            this.ID = Generate_Member_ID();
            this.Name = Name;
            this.Address = Address;
            this.Phone = Phone;
            this.BorrowedElem = null;
            this.ReturnDateElem = null;
            this.RetentedElem = null;
            this.Penalties = 0;
        }

        public string Generate_Member_ID()
        {
            if (ElemList<Member, String>.ElementsList.Count == 0) return "1";
            else return (Convert.ToInt32(ElemList<Member, String>.ElementsList[ElemList<Member, String>.ElementsList.Count - 1].ID) + 1).ToString();
        }

        public bool Compare(String id)
        {
            if (this.ID == id) return true;
            return false;
        }

        public override string ToString()
        {
            return "Membru adaugat";
        }
    }
}
