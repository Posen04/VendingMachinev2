
// Class to manage the coins in the machine
public class Money
{
    public int InsertedAmount { get; private set; }

    public Money()
    {
        InsertedAmount = 0;
    }

    // Method to insert money into the machine
    public void Insert(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("The amount must be greater than 0.");
        }
        InsertedAmount += amount;
        Console.WriteLine($"You have inserted {amount} kr. Total: {InsertedAmount} kr.");
    }

    // Method to deduct an amount (like when a product is bought)
    public void DeductAmount(int amount)
    {
        if (amount > InsertedAmount)
        {
            throw new InvalidOperationException("Insufficient funds to complete the transaction.");
        }
        InsertedAmount -= amount;
    }

    // Method to reset the inserted amount
    public void ResetAmount()
    {
        InsertedAmount = 0;
    }

    // Method to refund money to the customer
    public void RefundMoney()
    {
        Console.WriteLine($"You have been refunded {InsertedAmount} kr.");
        ResetAmount();
    }

    // Method to return change after a purchase
    public void ReturnChange()
    {
        if (InsertedAmount > 0)
        {
            Console.WriteLine($"Returning {InsertedAmount} kr. in change.");
            ResetAmount();
        }
    }
}
