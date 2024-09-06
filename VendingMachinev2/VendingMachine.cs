
// Class to manage the vending machine
public class VendingMachine
{
    private Dictionary<string, Product> slots; // Dictionary to map slots (A1, B1, etc.) to products, Used A1, B1, etc. Instead of 1, 2, 3 to emulate a vendingmachine
    private Money money;
    private Product selectedProduct; // Store the selected product

    public VendingMachine()
    {
        slots = new Dictionary<string, Product>();
        money = new Money();
        selectedProduct = null;
        FillMachine(); // Initialize the machine with some products
    }

    // Method to fill the machine with initial products
    private void FillMachine()
    {
        slots.Add("A1", new Product("Cola", 15, 10));
        slots.Add("A2", new Product("Fanta", 12, 8));
        slots.Add("B1", new Product("Pepsi", 14, 7));
        slots.Add("B2", new Product("Candy Bar", 10, 5));
        slots.Add("C1", new Product("Water", 10, 10));
        slots.Add("C2", new Product("Juice", 13, 6));
    }

    // Method to display the available products and their slots
    public void DisplayProducts()
    {
        Console.WriteLine("Available products:");
        foreach (var slot in slots)
        {
            Console.WriteLine($"{slot.Key}: {slot.Value}");
        }
    }

    // Method to insert coins into the machine
    public void InsertCoins(int amount)
    {
        try
        {
            money.Insert(amount);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }

    // Method to select a product by slot code (e.g., A1, B2)
    public void SelectProduct(string slotCode)
    {
        if (slots.ContainsKey(slotCode))
        {
            selectedProduct = slots[slotCode];

            if (selectedProduct.Stock == 0)
            {
                Console.WriteLine("Product is out of stock.");
                selectedProduct = null;
                return;
            }

            if (money.InsertedAmount >= selectedProduct.Price)
            {
                Console.WriteLine($"You have selected {selectedProduct.Name}. It costs {selectedProduct.Price} kr.");
            }
            else
            {
                Console.WriteLine($"Insufficient funds. {selectedProduct.Name} costs {selectedProduct.Price} kr.");
                selectedProduct = null;
            }
        }
        else
        {
            Console.WriteLine("Invalid slot code. Please try again.");
            selectedProduct = null;
        }
    }

    // Method to confirm the purchase
    public void ConfirmPurchase()
    {
        if (selectedProduct == null)
        {
            Console.WriteLine("No product has been selected.");
            return;
        }

        if (money.InsertedAmount >= selectedProduct.Price)
        {
            money.DeductAmount(selectedProduct.Price); // Deduct the price from inserted money
            selectedProduct.Stock--;
            Console.WriteLine($"You have purchased a {selectedProduct.Name}.");
            money.ReturnChange(); // Return any remaining change
        }
        else
        {
            Console.WriteLine($"Insufficient funds to purchase {selectedProduct.Name}.");
        }
        selectedProduct = null; // Reset the selected product after the transaction
    }

    // Admin menu for refilling products, removing money, and adjusting prices
    public void AdminMenu()
    {
        Console.WriteLine("Welcome to the admin menu.");
        bool adminActive = true;

        while (adminActive)
        {
            Console.WriteLine("\n1. Refill products");
            Console.WriteLine("2. Adjust product prices");
            Console.WriteLine("3. Remove money");
            Console.WriteLine("4. Exit admin menu");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RefillProducts();
                    break;
                case "2":
                    AdjustPrices();
                    break;
                case "3":
                    RemoveMoney();
                    break;
                case "4":
                    adminActive = false;
                    Console.WriteLine("Exiting admin menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    // Method to refill products
    private void RefillProducts()
    {
        foreach (var slot in slots)
        {
            Console.Write($"Enter the new stock for {slot.Value.Name} in {slot.Key} (current: {slot.Value.Stock}): ");
            if (int.TryParse(Console.ReadLine(), out int newStock) && newStock >= 0)
            {
                slot.Value.Stock = newStock;
                Console.WriteLine($"{slot.Value.Name} stock updated to {slot.Value.Stock}.");
            }
            else
            {
                Console.WriteLine("Invalid input. Stock not updated.");
            }
        }
    }

    // Method to adjust product prices
    private void AdjustPrices()
    {
        foreach (var slot in slots)
        {
            Console.Write($"Enter the new price for {slot.Value.Name} in {slot.Key} (current: {slot.Value.Price}): ");
            if (int.TryParse(Console.ReadLine(), out int newPrice) && newPrice > 0)
            {
                slot.Value.Price = newPrice;
                Console.WriteLine($"{slot.Value.Name} price updated to {slot.Value.Price} kr.");
            }
            else
            {
                Console.WriteLine("Invalid input. Price not updated.");
            }
        }
    }

    // Method to remove money from the machine
    private void RemoveMoney()
    {
        Console.WriteLine($"Removing {money.InsertedAmount} kr. from the machine.");
        money.ResetAmount();
    }
}
