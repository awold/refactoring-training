using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Refactoring
{
    public class LoginManager
    {
        private List<User> users;
        public LoginManager(List<User> users)
        {
            this.users = users;
        }

        public User PromptLogin()
        {
            string name = ConsoleHelper.PromptForValue("Enter Username: ");

            if (!string.IsNullOrEmpty(name))
            {
                if (UserExists(users, name))
                {
                    string pwd = ConsoleHelper.PromptForValue("Enter Password: ");

                    if (ValidatePassword(users, name, pwd))
                    {
                        return GetUser(name, pwd);
                    }
                    else
                    {
                        Console.Clear();
                        ConsoleHelper.WriteError("You entered an invalid password.");

                        return PromptLogin();
                    }
                }
                else
                {
                    Console.Clear();
                    ConsoleHelper.WriteError("You entered an invalid user.");

                    return PromptLogin();
                }
            }
            return null;
        }

        private User GetUser(string name, string pwd)
        {
            User targetUser = null;
            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                if (user.UserName == name && user.Password == pwd)
                {
                    targetUser = user;
                }
            }
            return targetUser;
        }

        private static bool ValidatePassword(List<User> users, string name, string pwd)
        {
            bool valPwd = false; // Is valid password?
            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];

                // Check that name and password match
                if (user.UserName == name && user.Password == pwd)
                {
                    valPwd = true;
                }
            }
            return valPwd;
        }

        private static bool UserExists(List<User> users, string name)
        {
            bool validUser = false;
            for (int i = 0; i < users.Count; i++)
            {
                User user = users[i];
                // Check that name matches
                if (user.UserName == name)
                {
                    validUser = true;
                }
            }
            return validUser;
        }

        
    }
}
