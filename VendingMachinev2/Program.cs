using System;
using System.Collections.Generic;

// Main program class
public class Program
{ 
    
    public static void Main(string[] args)
    {
        VendingMachine vendingMachine = new VendingMachine();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n1. Insert money");
            Console.WriteLine("2. Select product");
            Console.WriteLine("3. Confirm purchase");
            Console.WriteLine("4. Admin menu");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter the amount to insert: ");
                    if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
                    {
                        vendingMachine.InsertCoins(amount);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount.");
                    }
                    break;
                case "2":
                    vendingMachine.DisplayProducts();
                    Console.Write("Enter the product slot (e.g., A1, B2): ");
                    string slotCode = Console.ReadLine();
                    vendingMachine.SelectProduct(slotCode);
                    break;
                case "3":
                    vendingMachine.ConfirmPurchase();
                    break;
                case "4":
                    vendingMachine.AdminMenu();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Exiting the program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            
        }

    }
}
