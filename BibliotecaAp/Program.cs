using System;
using System.Diagnostics.Metrics;
using System.Transactions;

namespace BibliotecaAp
{
    class Program
    {
        static int Main()
        {
            bool isOptionNumeric;
            int option;
            do
            {
                Console.WriteLine("Choose option:\n");
                Console.WriteLine("1. Add member | 2. Search member | 3. Remove member | 4. Show members");
                Console.WriteLine("5. Add book | 6. Search book | 7. Remove book");
                Console.WriteLine("8. Add magazine | 9. Search magazine | 10. Remove magazine");
                Console.WriteLine("11. Borrow book | 12. Return book | 13. Retent book | 14. Cancel book retention");
                Console.WriteLine("15. Borrow magazine | 16. Return magazine | 17. Retent magazine | 18. Cancel magazine retention");
                Console.WriteLine("19. Show catalog | 20. Show transactions | 21. Show retentions");
                Console.WriteLine("22. Show penalty value | 23. Set penalty value | 24. Show member penalties | 25. Pay member penalties");
                Console.WriteLine("0. Exit\n");
                Console.WriteLine("Enter option number: ");

                isOptionNumeric = int.TryParse(Console.ReadLine(), out option);
            } while (!isOptionNumeric);

            Console.WriteLine("\n");

            switch (option)
            {
                case 0:
                    {
                        Console.WriteLine("\n\n[COMMAND] Application CLOSED!\n\n\n\n");
                        break;
                    }
                case 1:
                    {
                        Console.WriteLine("Add member\n");
                        string name, address, phone;
                        Console.WriteLine("Insert member's NAME:");
                        name = Console.ReadLine();
                        Console.WriteLine("Insert member's ADDRESS:");
                        address = Console.ReadLine();
                        Console.WriteLine("Insert member's PHONE NUMBER:");
                        phone = Console.ReadLine();
                        Member mem = Library.Add_Member(name, address, phone);
                        Console.WriteLine("[RESULT] Member ADDED succesfully! [Info:] ID: " + mem.ID + ", Name: " + mem.Name + ", Address: " + mem.Address + ", Phone: " + mem.Phone + ".");
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Search member\n");
                        string id;
                        Console.WriteLine("Insert member's ID:");
                        id = Console.ReadLine().ToString();
                        Member mem = Library.Search_Member(id);
                        Console.WriteLine("[RESULT] Member FOUND! [Info:] ID: " + mem.ID + ", Name: " + mem.Name + ", Address: " + mem.Address + ", Phone number: " + mem.Phone + ".");
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Remove a member\n");
                        string id;
                        Console.WriteLine("Insert member's ID:");
                        id = Console.ReadLine().ToString();
                        Member mem = Library.Search_Member(id);
                        int remove_mem = Library.Delete_Member(id);
                        switch (remove_mem)
                        {
                            case 1: // member has a borrowed book
                                {
                                    Console.WriteLine("[RESULT] Member has a borrowed book!");
                                    break;
                                }
                            case 2: // member has a retented book
                                {
                                    Console.WriteLine("[RESULT] Member has a retented book!");
                                    break;
                                }
                            case 3: // member could not be removed
                                {
                                    Console.WriteLine("[RESULT] Error! Member could not be removed (possibly it was not found)!");
                                    break;
                                }
                            default: // member removed
                                {
                                    Console.WriteLine("[RESULT] Member REMOVED!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 4:
                    {
                        Console.WriteLine("Members List\n");
                        if (ElemList<Member, String>.ElementsList.Count == 0) Console.WriteLine("[RESULT] The library has no members.");
                        else
                        {
                            Console.WriteLine("[ID] | Name | Address | Phone number | Borrowed element | Borrowed until | Penalties");
                            Console.WriteLine("---------------------------------------------------------------------");
                            Console.WriteLine(Library.ShowAllMembers());
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 5:
                    {
                        Console.WriteLine("Add book\n");
                        string title, author;
                        int tax, b_status;
                        Console.WriteLine("Insert book's TITLE:");
                        title = Console.ReadLine();
                        Console.WriteLine("Insert book's AUTHOR:");
                        author = Console.ReadLine();
                        Console.WriteLine("Insert book's reading status (1 = can be borrowed, 2 = can be read only in hall");
                        b_status = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Insert book's TAX (0 = without tax):");
                        tax = Convert.ToInt32(Console.ReadLine());
                        AbstractElem absElem;
                        if (b_status == 1 && tax == 0)
                            absElem = Library.AddElement("Book", title, author, "", tax);
                        else if (b_status == 1 && tax > 0)
                            absElem = Library.AddElement("Book_Tax", title, author, "", tax);
                        else if (b_status == 2 && tax == 0)
                            absElem = Library.AddElement("Book_Hall", title, author, "", tax);
                        else //(b_status == 2 && tax > 0)
                            absElem = Library.AddElement("Book_Hall_Tax", title, author, "", tax);
                        Console.WriteLine(absElem.Accept(new DisplayFormat(), 1));
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 6:
                    {
                        Console.WriteLine("Search book\n");
                        string id;
                        Console.WriteLine("Insert book's ID:");
                        id = Console.ReadLine().ToString();
                        AbstractElem absElem = Library.SearchElement(id);
                        if (absElem == null)
                            Console.WriteLine("[RESULT] Book NOT FOUND!");
                        else
                            Console.WriteLine(absElem.Accept(new DisplayFormat(), 2));
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 7:
                    {
                        Console.WriteLine("Remove a book\n");
                        string id;
                        Console.WriteLine("Insert book's ID:");
                        id = Console.ReadLine().ToString();
                        AbstractElem absElem = Library.SearchElement(id);
                        int remove_bk = Library.DeleteElement(id);
                        switch (remove_bk)
                        {
                            case 1: // book is currently borrowed
                                {
                                    Console.WriteLine("[RESULT] Book is currently borrowed!");
                                    break;
                                }
                            case 2: // book is currently retented
                                {
                                    Console.WriteLine("[RESULT] Book is currently retented!");
                                    break;
                                }
                            case 3: // book could not be removed
                                {
                                    Console.WriteLine("[RESULT] Error! Book could not be removed (possibly it was not found)!");
                                    break;
                                }
                            default: // book removed
                                {
                                    Console.WriteLine("[RESULT] Book REMOVED!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 8:
                    {
                        Console.WriteLine("Add magazine\n");
                        string title, publisher;
                        int tax, m_status;
                        Console.WriteLine("Insert magazine's TITLE:");
                        title = Console.ReadLine();
                        Console.WriteLine("Insert magazine's PUBLISHER:");
                        publisher = Console.ReadLine();
                        Console.WriteLine("Insert magazine's reading status (1 = can be borrowed, 2 = can be read only in hall");
                        m_status = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Insert magazine's TAX (0 = without tax):");
                        tax = Convert.ToInt32(Console.ReadLine());
                        //Book bk = Library.AdaugaCarte(id, title, author);
                        AbstractElem absElem;
                        if (m_status == 1 && tax == 0)
                            absElem = Library.AddElement("Magazine", title, "", publisher, tax);
                        else if (m_status == 1 && tax > 0)
                            absElem = Library.AddElement("Magazine_Tax", title, "", publisher, tax);
                        else if (m_status == 2 && tax == 0)
                            absElem = Library.AddElement("Magazine_Hall", title, "", publisher, tax);
                        else //(b_status == 2 && tax > 0)
                            absElem = Library.AddElement("Magazine_Hall_Tax", title, "", publisher, tax);
                        Console.WriteLine(absElem.Accept(new DisplayFormat(), 1));
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 9:
                    {
                        Console.WriteLine("Search magazine\n");
                        string id;
                        Console.WriteLine("Insert magazine's ID:");
                        id = Console.ReadLine().ToString();
                        AbstractElem absElem = Library.SearchElement(id);
                        if (absElem == null)
                            Console.WriteLine("[RESULT] Magazine NOT FOUND!");
                        else
                            Console.WriteLine(absElem.Accept(new DisplayFormat(), 2));
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 10:
                    {
                        Console.WriteLine("Remove a magazine\n");
                        string id;
                        Console.WriteLine("Insert magazine's ID:");
                        id = Console.ReadLine().ToString();
                        AbstractElem absElem = Library.SearchElement(id);
                        int remove_mag = Library.DeleteElement(id);
                        switch (remove_mag)
                        {
                            case 1: // magazine is currently borrowed
                                {
                                    Console.WriteLine("[RESULT] Magazine is currently borrowed!");
                                    break;
                                }
                            case 2: // magazine is currently retented
                                {
                                    Console.WriteLine("[RESULT] Magazine is currently retented!");
                                    break;
                                }
                            case 3: // magazine could not be removed
                                {
                                    Console.WriteLine("[RESULT] Error! Magazine could not be removed (possibly it was not found)!");
                                    break;
                                }
                            default: // magazine removed
                                {
                                    Console.WriteLine("[RESULT] Magazine REMOVED!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 11:
                    {
                        string m_id, b_id;
                        Console.WriteLine("Borrow a book\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        Console.WriteLine("Insert book's ID:");
                        b_id = Console.ReadLine().ToString();
                        int borrow_book = Library.BorrowElement(m_id, b_id);
                        switch (borrow_book)
                        {
                            case 1:
                                {
                                    Console.WriteLine("[RESULT] Member already has a borrowed book!"); // member already has a borrowed book
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine("[RESULT] Member already has a retented book!"); // member already has a retented book
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("[RESULT] Book is already borrowed!"); // book is already borrowed
                                    break;
                                }
                            case 4:
                                {
                                    Console.WriteLine("[RESULT] Member has penalties - they must be payed before he can borrow another book!"); // member has penalties
                                    break;
                                }
                            default: // book borrowed
                                {
                                    Console.WriteLine("[RESULT] Book BORROWED!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 12:
                    {
                        string m_id;
                        Console.WriteLine("Return a book\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        int return_book = Library.ReturnElement(m_id);
                        Member mem = Library.Search_Member(m_id);
                        switch (return_book)
                        {
                            case -1: // book was only returned - has no retentions
                                {
                                    if (mem.Penalties > 0) Console.WriteLine("[RESULT] Book RETURNED late, penalties were applied (penalties: " + mem.Penalties + ")!");
                                    else Console.WriteLine("[RESULT] Book RETURNED on time (no penalties applied)!");
                                    break;
                                }
                            case -2: // member does not have a borrowed book
                                {
                                    Console.WriteLine("[RESULT] Member does not have any book borrowed at this moment!");
                                    break;
                                }
                            case -3: // book was returned - it had a retention - could not be borrowed to the person who retented it (ERROR: member has already a borrowed book)
                                {
                                    Console.WriteLine("[RESULT] Book RETURNED! [ERROR] It has a retention - could not be borrowed to the person who retented it (member has already a borrowed book)!");
                                    break;
                                }
                            case -4: // book was returned - it had a retention - could not be borrowed to the person who retented it (ERROR: member already has a retented book)
                                {
                                    Console.WriteLine("[RESULT] Book RETURNED! [ERROR] It has a retention - could not be borrowed to the person who retented it (member already has a retented book)!");
                                    break;
                                }
                            case -5: // book was returned - it had a retention - could not be borrowed to the person who retented it (ERROR: book is already borrowed)
                                {
                                    Console.WriteLine("[RESULT] Book RETURNED! [ERROR] It has a retention - could not be borrowed to the person who retented it (book is already borrowed)!");
                                    break;
                                }
                            default: // book was returned - it had a retention so it was borrowed to the person who retented it
                                {
                                    if (mem.Penalties > 0) Console.WriteLine("[RESULT] Book RETURNED late, penalties were applied double because the book have a retention (penalties: " + mem.Penalties + ")!");
                                    else Console.WriteLine("[RESULT] Book RETURNED on time (no penalties applied)!");
                                    Member borrow_to = Library.Search_Member(return_book.ToString());
                                    Console.WriteLine("[RESULT] Book had a retention so it was borrowed to the person who retented it (Member: [ID: " + borrow_to.ID + "] " + borrow_to.Name + ")!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 13:
                    {
                        string m_id, b_id;
                        Console.WriteLine("Retent a book\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        Console.WriteLine("Insert book's ID:");
                        b_id = Console.ReadLine().ToString();
                        int retent_book = Library.RetentElement(m_id, b_id);
                        switch (retent_book)
                        {
                            case 1: // member already has a book borrowed
                                {
                                    Console.WriteLine("[RESULT] Member already has a book borrowed!");
                                    break;
                                }
                            case 2: // member already has a book retented
                                {
                                    Console.WriteLine("[RESULT] Member already has a book retented!");
                                    break;
                                }
                            case 3: // book is not borrowed
                                {
                                    Console.WriteLine("[RESULT] Book is not currently borrowed!");
                                    break;
                                }
                            case 4: // book is already retented
                                {
                                    Console.WriteLine("[RESULT] Book is already retented!");
                                    break;
                                }
                            case 5: // member has penalties
                                {
                                    Console.WriteLine("[RESULT] Member has penalties - they must be payed before he can retent another book!");
                                    break;
                                }
                            default: // book can be retented -> PROCEED
                                {
                                    Console.WriteLine("[RESULT] Book RETENTED!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 14:
                    {
                        string m_id;
                        Console.WriteLine("Cancel book retention\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        int cancel_retent_book = Library.CancelRetentElement(m_id);
                        switch (cancel_retent_book)
                        {
                            case 1: // member does not have a retented book
                                {
                                    Console.WriteLine("[RESULT] Member does not have a retented book!");
                                    break;
                                }
                            default: // book retention canceled
                                {
                                    Console.WriteLine("[RESULT] Book retention CANCELED!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 15:
                    {
                        string m_id, mag_id;
                        Console.WriteLine("Borrow a magazine\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        Console.WriteLine("Insert magazine's ID:");
                        mag_id = Console.ReadLine().ToString();
                        int borrow_magazine = Library.BorrowElement(m_id, mag_id);
                        switch (borrow_magazine)
                        {
                            case 1:
                                {
                                    Console.WriteLine("[RESULT] Member already has a borrowed element!"); // member already has a borrowed element
                                    break;
                                }
                            case 2:
                                {
                                    Console.WriteLine("[RESULT] Member already has a retented element!"); // member already has a retented element
                                    break;
                                }
                            case 3:
                                {
                                    Console.WriteLine("[RESULT] Magazine is already borrowed!"); // magazine is already borrowed
                                    break;
                                }
                            case 4:
                                {
                                    Console.WriteLine("[RESULT] Member has penalties - they must be payed before he can borrow another element!"); // member has penalties
                                    break;
                                }
                            default: // magazine borrowed
                                {
                                    Console.WriteLine("[RESULT] Magazine BORROWED!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 16:
                    {
                        string m_id;
                        Console.WriteLine("Return a magazine\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        int return_magazine = Library.ReturnElement(m_id);
                        Member mem = Library.Search_Member(m_id);
                        switch (return_magazine)
                        {
                            case -1: // magazine was only returned - has no retentions
                                {
                                    if (mem.Penalties > 0) Console.WriteLine("[RESULT] Magazine RETURNED late, penalties were applied (penalties: " + mem.Penalties + ")!");
                                    else Console.WriteLine("[RESULT] Magazine RETURNED on time (no penalties applied)!");
                                    break;
                                }
                            case -2: // member does not have a borrowed element
                                {
                                    Console.WriteLine("[RESULT] Member does not have any element borrowed at this moment!");
                                    break;
                                }
                            case -3: // magazine was returned - it had a retention - could not be borrowed to the person who retented it (ERROR: member (2) has already a borrowed element)
                                {
                                    Console.WriteLine("[RESULT] Magazine RETURNED! [ERROR] It has a retention - could not be borrowed to the person who retented it (member has already a borrowed element)!");
                                    break;
                                }
                            case -4: // magazine was returned - it had a retention - could not be borrowed to the person who retented it (ERROR: member already has a retented element)
                                {
                                    Console.WriteLine("[RESULT] Magazine RETURNED! [ERROR] It has a retention - could not be borrowed to the person who retented it (member already has a retented element)!");
                                    break;
                                }
                            case -5: // magazine was returned - it had a retention - could not be borrowed to the person who retented it (ERROR: magazine is already borrowed)
                                {
                                    Console.WriteLine("[RESULT] Magazine RETURNED! [ERROR] It has a retention - could not be borrowed to the person who retented it (magazine is already borrowed)!");
                                    break;
                                }
                            default: // magazine was returned - it had a retention so it was borrowed to the person who retented it
                                {
                                    if (mem.Penalties > 0) Console.WriteLine("[RESULT] Magazine RETURNED late, penalties were applied double because the magazine has a retention (penalties: " + mem.Penalties + ")!");
                                    else Console.WriteLine("[RESULT] Magazine RETURNED on time (no penalties applied)!");
                                    Member borrow_to = Library.Search_Member(return_magazine.ToString());
                                    Console.WriteLine("[RESULT] Magazine had a retention so it was borrowed to the person who retented it (Member: [ID: " + borrow_to.ID + "] " + borrow_to.Name + ")!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 17:
                    {
                        string m_id, mag_id;
                        Console.WriteLine("Retent a magazine\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        Console.WriteLine("Insert book's ID:");
                        mag_id = Console.ReadLine().ToString();
                        int retent_magazine = Library.RetentElement(m_id, mag_id);
                        switch (retent_magazine)
                        {
                            case 1: // member already has an element borrowed
                                {
                                    Console.WriteLine("[RESULT] Member already has an element borrowed!");
                                    break;
                                }
                            case 2: // member already has an element retented
                                {
                                    Console.WriteLine("[RESULT] Member already has an element retented!");
                                    break;
                                }
                            case 3: // magazine is not borrowed
                                {
                                    Console.WriteLine("[RESULT] Magazine is not currently borrowed!");
                                    break;
                                }
                            case 4: // magazine is already retented
                                {
                                    Console.WriteLine("[RESULT] Magazine is already retented!");
                                    break;
                                }
                            case 5: // member has penalties
                                {
                                    Console.WriteLine("[RESULT] Member has penalties - they must be payed before he can retent another element!");
                                    break;
                                }
                            default: // magazine can be retented -> PROCEED -> magazine retented
                                {
                                    Console.WriteLine("[RESULT] Magazine RETENTED!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 18:
                    {
                        string m_id;
                        Console.WriteLine("Cancel magazine retention\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        int cancel_retent_magazine = Library.CancelRetentElement(m_id);
                        switch (cancel_retent_magazine)
                        {
                            case 1: // member does not have a retented magazine
                                {
                                    Console.WriteLine("[RESULT] Member does not have a retented magazine!");
                                    break;
                                }
                            default: // magazine retention canceled
                                {
                                    Console.WriteLine("[RESULT] Magazine retention CANCELED!");
                                    break;
                                }
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 19:
                    {
                        Console.WriteLine("|| Catalog ||");
                        Console.WriteLine("| ------------------------------------------------- |");
                        if (ElemList<AbstractElem, String>.ElementsList.Count == 0) Console.WriteLine("[RESULT] The library has no elements.");
                        else
                        {
                            //Console.WriteLine("[ID] | Title | Author | Borrowed by | Return date");
                            //Console.WriteLine("-------------------------------------------------");
                            //string mem_desc;
                            //Book bk;
                            /*for (int i = 0; i < ElemList<AbstractElem, String>.ElementsList.Count; i++)
                            {
                                if (ElemList<AbstractElem, String>.ElementsList[i] is Book)
                                {
                                    //bk = (Book)ElemList<AbstractElem, String>.ElementsList[i];
                                    if (bk.BorrowedBy == null) mem_desc = "No one";
                                    else mem_desc = "[ID: " + bk.BorrowedBy.ID + "] " + bk.BorrowedBy.Name;
                                    Console.WriteLine("[" + bk.ID + "] | " + bk.Title + " | " + bk.Author + " | " + mem_desc + " | " + (bk.BorrowedBy == null ? ("None") : ("" + bk.ReturnDate)));
                                    //bk.Accept(new DisplayFormat(), 3);
                                }
                            }*/
                            //Book bk;
                            //foreach(AbstractElem absElem in ElemList<AbstractElem, String>.ElementsList)
                            //{
                            /*if (elemTax.AbsElem is Book)
                            {
                                bk = (Book)elemTax.AbsElem;
                                Console.WriteLine(bk.Accept(new DisplayFormat(), 3));
                            }*/
                            //Console.WriteLine(absElem.Accept(new DisplayFormat(), 3));
                            //}
                            /*foreach (AbstractElem absElem in ElemList<AbstractElem, String>.ElementsList)
                            {
                                Console.WriteLine(absElem.Accept(new DisplayFormat(), 3));
                            }*/
                            Console.WriteLine(Library.ShowCatalog());
                        }
                        Console.WriteLine("\n");
                        Main();
                        break;
                    }
                case 20:
                    {
                        Console.WriteLine("Transactions list\n");
                        if (ElemList<Transaction, String>.ElementsList.Count == 0) Console.WriteLine("[RESULT] The library has no transactions.");
                        else
                        {
                            Console.WriteLine("[ID] | Date | Type | Member name | Element title");
                            Console.WriteLine("---------------------");
                            Console.WriteLine(Library.ShowTransactions());
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 21:
                    {
                        Console.WriteLine("Retentions list\n");
                        if (ElemList<Retention, String>.ElementsList.Count == 0) Console.WriteLine("[RESULT] The library has no retentions.");
                        else
                        {
                            Console.WriteLine("[ID] | Member | Book | Placed on date");
                            Console.WriteLine("---------------------");
                            Console.WriteLine(Library.ShowRetentions());
                        }
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 22:
                    {
                        int value = Library.GetPenaltyValue();
                        Console.WriteLine("[RESULT] Penalty value: " + value + " / day.\n");
                        Main();
                        break;
                    }
                case 23:
                    {
                        int p_val;
                        bool isNumeric = false;
                        Console.WriteLine("Set penalty value\n");
                        do
                        {
                            Console.WriteLine("Insert value (penalty / day) - between 1 and 10: ");
                            isOptionNumeric = int.TryParse(Console.ReadLine(), out p_val);
                            if (!isNumeric) Console.WriteLine("[ERROR] Value must be numeric!");
                            else
                            {
                                if (p_val < 1 || p_val > 10)
                                {
                                    Console.WriteLine("[ERROR] Penalty value must be between 1 and 10!");
                                }
                            }
                        }
                        while (!isNumeric || p_val < 1 || p_val > 10);
                        bool result = Library.SetPenaltyValue(p_val);
                        if (result) Console.WriteLine("[RESULT] Penalty value was set to [" + p_val + " / day]!");
                        else Console.WriteLine("[ERROR] Penalty value failed to be set, something went wrong!");
                        Main();
                        break;
                    }
                case 24:
                    {
                        string m_id;
                        Console.WriteLine("Show member penalties\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        Member mem = Library.Search_Member(m_id);
                        Console.WriteLine("[RESULT] Member: [ID: " + m_id + "] " + mem.Name + " - Penalties: " + mem.Penalties);
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                case 25:
                    {
                        string m_id;
                        Console.WriteLine("Pay member penalties\n");
                        Console.WriteLine("Insert member's ID:");
                        m_id = Console.ReadLine().ToString();
                        Member mem = Library.Search_Member(m_id);
                        Console.WriteLine("* Member: [ID: " + m_id + "] " + mem.Name + " - Penalties: " + mem.Penalties);
                        string response;
                        do
                        {
                            Console.WriteLine("Proceed with the payment ? (YES / NO)");
                            response = Console.ReadLine().ToString();
                            switch (response)
                            {
                                case "NO":
                                    {
                                        Console.WriteLine("[RESULT] Penalties paying canceled!");
                                        break;
                                    }
                                case "YES":
                                    {
                                        mem.Penalties = 0;
                                        Console.WriteLine("[RESULT] Penalties PAYED!");
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("[RESULT] Invalid answer!");
                                        break;
                                    }
                            }
                        }
                        while (response != "YES" && response != "NO");
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("[ERROR] Option number INVALID!");
                        Console.WriteLine("\n\n");
                        Main();
                        break;
                    }
            }
            return 1;
        }
    }
}
