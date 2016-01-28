﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Refactoring
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Load users from data file
            List<User> users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(@"Data/Users.json"));

            // Load products from data file
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(File.ReadAllText(@"Data/Products.json"));
            LoginManager loginManager = new LoginManager(users);

            Console.WriteLine("Welcome to TUSC");
            Console.WriteLine("---------------");

            User currentUser = loginManager.PromptLogin();

            if (currentUser != null)
            {
                Console.Clear();
                ConsoleHelper.WriteSuccess("Login successful! Welcome " + currentUser.UserName + "!");

                Tusc.Start(currentUser, products, users);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter key to exit");
                Console.ReadLine();
            }
        }
    }
}
