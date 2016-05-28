using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AddressBook aBook = AddressBook.Instance;

            Logger log = new Logger(new PrintToConsole());

            aBook.onUserAdd += log.Info;
            aBook.onUserAddFail += log.Eror;
            aBook.onUserListUse += log.Debug;
            aBook.onUserRemove += log.Info;
            aBook.onUserRemoveFail += log.Warning;

            aBook.AddUser("John", "Doe", new DateTime(1989, 9, 17), DateTime.Now, "Krivoy Rog", "Katkova street", "224389", "male", "foo@foo.mail.com");
            aBook.RemoveUser("John", "Lio", new DateTime(1990, 9, 17));
            aBook.RemoveUser("John", "Doe", new DateTime(1989, 9, 17));

            Console.ReadKey();
        }
    }
}
