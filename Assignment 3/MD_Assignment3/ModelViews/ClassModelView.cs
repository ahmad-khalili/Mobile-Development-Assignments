using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD_Assignment3.Models;

namespace MD_Assignment3.ModelViews
{

    public class ClassModelView
    {
        List<User> users = new List<User>();
        List<StaffUser> staffUsers = new List<StaffUser>();
        List<AdminUser> adminUsers = new List<AdminUser>();

        public delegate void UserDelegate(User user);
        public delegate void ExceptionDelegate(Exception exception);

        public UserDelegate OnAdd;
        public UserDelegate OnRemove;
        public UserDelegate OnUpdate;
        public ExceptionDelegate OnError;


        public void AddUser(User user)
        {
            Exception exception = null;

            if (String.IsNullOrEmpty(user.Id.ToString()) || String.IsNullOrEmpty(user.Name) || String.IsNullOrEmpty(user.Age.ToString()))
            {
                exception = new InvalidDataException("Invalid Data!");
            }

            if (users.Contains(user))
            {
                exception = new DuplicateUserException("User Already Exists!");
            }

            if(OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            if (OnAdd != null)
            {
                OnAdd(user);
            }


            users.Add(user);
        }

        public void AddUser(StaffUser staffUser)
        {
            Exception exception = null;

            if (String.IsNullOrEmpty(staffUser.Id.ToString()) || String.IsNullOrEmpty(staffUser.Name) || String.IsNullOrEmpty(staffUser.Age.ToString()) || String.IsNullOrEmpty(staffUser.Role.ToString()))
            {
                exception = new InvalidDataException("Invalid Data!");
            }

            if (staffUsers.Contains(staffUser))
            {
                exception = new DuplicateUserException("User Already Exists!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            if (OnAdd != null)
            {
                OnAdd(staffUser);
            }

            staffUsers.Add(staffUser);
        }

        public void AddUser(AdminUser adminUser)
        {
            Exception exception = null;

            if (String.IsNullOrEmpty(adminUser.Id.ToString()) || String.IsNullOrEmpty(adminUser.Name) || String.IsNullOrEmpty(adminUser.Age.ToString()) || adminUser.StaffUsers.Count.Equals(0))
            {
                exception = new InvalidDataException("Invalid Data!");
            }

            if (adminUsers.Contains(adminUser))
            {
                exception = new DuplicateUserException("User Already Exists!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            if (OnAdd != null)
            {
                OnAdd(adminUser);
            }

            adminUsers.Add(adminUser);
        }

        public void EditUser(User user, string name, int age)
        {
            Exception exception = null;

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(age.ToString()))
            {
                exception = new InvalidDataException("Invalid Data!");
            }

            if (!(users.Contains(user))){
                exception = new UserNotFoundException("User Was Not Found!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            users[users.IndexOf(user)].Name = name;
            users[users.IndexOf(user)].Age = age;

            if (OnUpdate != null)
            {
                OnUpdate(user);
            }
        }
        public void EditUser(StaffUser staffUser, string name, int age, StaffUser.Roles role)
        {
            Exception exception = null;

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(age.ToString()) || String.IsNullOrEmpty(role.ToString()))
            {
                exception = new InvalidDataException("Invalid Data!");
            }

            if (!(staffUsers.Contains(staffUser)))
            {
                exception = new UserNotFoundException("User Was Not Found!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            staffUsers[staffUsers.IndexOf(staffUser)].Name = name;
            staffUsers[staffUsers.IndexOf(staffUser)].Age = age;
            staffUsers[staffUsers.IndexOf(staffUser)].Role = role;

            if (OnUpdate != null)
            {
                OnUpdate(staffUser);
            }
        }
        public void EditUser(AdminUser adminUser, string name, int age, List<StaffUser> staffUsers)
        {
            Exception exception = null;

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(age.ToString()) || staffUsers.Count.Equals(0))
            {
                throw new InvalidDataException("Invalid Data!");
            }

            if (!(adminUsers.Contains(adminUser)))
            {
                throw new UserNotFoundException("User Was Not Found!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            adminUsers[adminUsers.IndexOf(adminUser)].Name = name;
            adminUsers[adminUsers.IndexOf(adminUser)].Age = age;
            adminUsers[adminUsers.IndexOf(adminUser)].StaffUsers = staffUsers;

            if (OnUpdate != null)
            {
                OnUpdate(adminUser);
            }
        }

        public void DeleteUser(User user)
        {
            Exception exception = null;

            if (!(users.Contains(user)))
            {
                throw new UserNotFoundException("User Was Not Found!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            if (OnRemove != null)
            {
                OnRemove(user);
            }

            users.Remove(user);
        }

        public void DeleteUser(StaffUser staffUser)
        {
            Exception exception = null;

            if (!(staffUsers.Contains(staffUser)))
            {
                throw new UserNotFoundException("User Was Not Found!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            if (OnRemove != null)
            {
                OnRemove(staffUser);
            }

            users.Remove(staffUser);
        }

        public void DeleteUser(AdminUser adminUser)
        {
            Exception exception = null;

            if (!(adminUsers.Contains(adminUser)))
            {
                throw new UserNotFoundException("User Was Not Found!");
            }

            if (OnError != null && exception != null)
            {
                OnError(exception);
                return;
            }

            if (OnRemove != null)
            {
                OnRemove(adminUser);
            }

            users.Remove(adminUser);
        }
    }
}
