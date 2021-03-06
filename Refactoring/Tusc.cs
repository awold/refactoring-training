﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class Tusc
    {
        private static User currentUser;
        public static void Start(User user, List<Product> prods, List<User> usrs)
        {
            currentUser = user;

            while (true)
            {
                ShowRemainingBalance();
                string choice = PromptProductList(prods);

                int productIndex;
                if (Int32.TryParse(choice, out productIndex))
                {
                    productIndex -= 1;
                    if (productIndex >= prods.Count)
                    {
                        SaveToFile(prods, usrs);

                        // Prevent console from closing
                        Console.WriteLine();
                        Console.WriteLine("Press Enter key to exit");
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("You want to buy: " + prods[productIndex].Name);
                        ShowRemainingBalance();

                        choice = ConsoleHelper.PromptForValue("Enter amount to purchase:");
                        int qty;
                        while(!Int32.TryParse(choice, out qty))
                        {
                            choice = ConsoleHelper.PromptForValue("Enter amount to purchase:");
                        }

                        if (currentUser.Balance - (prods[productIndex].Price * qty) < 0)
                        {
                            Console.Clear();
                            ConsoleHelper.WriteError("You do not have enough money to buy that.");
                            continue;
                        }

                        // Check if quantity is less than quantity
                        if (prods[productIndex].Quantity <= qty)
                        {
                            Console.Clear();
                            ConsoleHelper.WriteError("Sorry, " + prods[productIndex].Name + " is out of stock");
                            continue;
                        }

                        // Check if quantity is greater than zero
                        if (qty > 0)
                        {
                            // Balance = Balance - Price * Quantity
                            currentUser.Balance = currentUser.Balance - (prods[productIndex].Price * qty);

                            // Quanity = Quantity - Quantity
                            prods[productIndex].Quantity = prods[productIndex].Quantity - qty;

                            Console.Clear();
                            ConsoleHelper.WriteSuccess("You bought " + qty + " " + prods[productIndex].Name);
                            ConsoleHelper.WriteSuccess("Your new balance is " + currentUser.Balance.ToString("C"));
                        }
                        else
                        {
                            // Quantity is less than zero
                            Console.Clear();
                            ConsoleHelper.WriteWarning("Purchase cancelled");
                        }
                    }
                }
            }
        }

        private static void SaveToFile(List<Product> prods, List<User> usrs)
        {
            // Write out new balance
            string json = JsonConvert.SerializeObject(usrs, Formatting.Indented);
            File.WriteAllText(@"Data/Users.json", json);

            // Write out new quantities
            string json2 = JsonConvert.SerializeObject(prods, Formatting.Indented);
            File.WriteAllText(@"Data/Products.json", json2);
        }

        private static string PromptProductList(List<Product> prods)
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to buy?");
            for (int i = 0; i < prods.Count; i++)
            {
                Product prod = prods[i];
                Console.WriteLine(i + 1 + ": " + prod.Name + " (" + prod.Price.ToString("C") + ")");
            }
            Console.WriteLine(prods.Count + 1 + ": Exit");

            return ConsoleHelper.PromptForValue("Enter a number:");
        }

        private static void ShowRemainingBalance()
        {
            Console.WriteLine("Your balance is " + currentUser.Balance.ToString("C"));
        }
    }
}
