using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class User
{
    public string LastName { get; private set; }
    public string FirstName { get; private set; }
    public DateTime Birthdate { get; private set; }
    public DateTime TimeAdded { get; private set; }
    public string City { get; private set; }
    public string Address { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Gender { get; private set; }
    public string Email { get; private set; }

    public User(string name, string lastname, DateTime birthDate, DateTime created, string city, string address, string phone, string gender, string email)
    {
        FirstName = name;
        LastName = lastname;
        Birthdate = birthDate;
        TimeAdded = created;
        City = city;
        Address = address;
        PhoneNumber = phone;
        Gender = gender;
        Email = email;
    }
}
public sealed class AddressBook
{
    private static readonly Lazy<AddressBook> lazy = new Lazy<AddressBook>(() => new AddressBook());

    public static AddressBook Instance { get { return lazy.Value; } }

    public delegate void UserAdded(string firstname, string lastname, string act);
    public delegate void UserRemoved(string firstname, string lastname, string act);
    public delegate void UserListUsed(string act);
    public delegate void UserRemoveFail(string firstname, string lastname);
    public delegate void UserAddFail(string firstname, string lastname);

    public event UserAdded onUserAdd;
    public event UserRemoved onUserRemove;
    public event UserListUsed onUserListUse;
    public event UserRemoveFail onUserRemoveFail;
    public event UserAddFail onUserAddFail;

    List<User> UserList = new List<User>();

    public void AddUser(string name, string lastname, DateTime birthDate, DateTime created, string city, string address, string phone, string gender, string email)
    {
        onUserListUse("add");
        try
        {
            UserList.Add(new User(name, lastname, birthDate, created, city, address, phone, gender, email));
            onUserAdd(name, lastname, "added");
        }
        catch (Exception e)
        {
            onUserAddFail(name, lastname);
        }
    }

    public void RemoveUser(string name, string lastname, DateTime bithdate)
    {
        onUserListUse("remove");
        try
        {
            if (UserList.Remove(UserList.SingleOrDefault(x => x.FirstName == name && x.LastName == lastname && x.Birthdate == bithdate)))
            {
                onUserRemove(name, lastname, "removed");
            }
            else
            {
                Exception ex = new Exception();
                throw ex;
            }
        }
        catch (Exception e)
        {
            onUserRemoveFail(name, lastname);
        }
    }

    private AddressBook() { }
}

