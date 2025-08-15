using System;
using System.Collections.Generic;

// Step 1: Record for Transactions
public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

// Step 2: Interface for Transaction Processing
public interface ITransactionProcessor
{
    void Process(Transaction transaction);
}

// Step 3: Implementations of Transaction Processors
public class BankTransferProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[Bank Transfer] Processed {transaction.Amount:C} for {transaction.Category} on {transaction.Date.ToShortDateString()}");
    }
}

public class MobileMoneyProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[Mobile Money] Processed {transaction.Amount:C} for {transaction.Category} on {transaction.Date.ToShortDateString()}");
    }
}

public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[Crypto Wallet] Processed {transaction.Amount:C} for {transaction.Category} on {transaction.Date.ToShortDateString()}");
    }
}

// Step 4: Base Account Class
public class Account
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; protected set; }

    public Account(string accountNumber, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        Balance = initialBalance;
    }

    public virtual void ApplyTransaction(Transaction transaction)
    {
        Balance -= transaction.Amount;
    }
}

// Step 5: Sealed SavingsAccount Class
public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance)
    {
    }

    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction.Amount > Balance)
        {
            Console.WriteLine("Insufficient funds");
        }
        else
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Transaction applied. New balance: {Balance:C}");
        }
    }
}

// Step 6: FinanceApp Simulation
public class FinanceApp
{
    private List<Transaction> _transactions = new();

    public void Run()
    {
        // Create a savings account with initial balance
        SavingsAccount account = new("ACC12345", 1000m);

        // Create sample transactions
        Transaction t1 = new(1, DateTime.Now, 150m, "Groceries");
        Transaction t2 = new(2, DateTime.Now, 200m, "Utilities");
        Transaction t3 = new(3, DateTime.Now, 300m, "Entertainment");

        // Process each transaction
        ITransactionProcessor mobileMoneyProcessor = new MobileMoneyProcessor();
        ITransactionProcessor bankTransferProcessor = new BankTransferProcessor();
        ITransactionProcessor cryptoWalletProcessor = new CryptoWalletProcessor();

        mobileMoneyProcessor.Process(t1);
        bankTransferProcessor.Process(t2);
        cryptoWalletProcessor.Process(t3);

        // Apply each transaction to the account
        account.ApplyTransaction(t1);
        account.ApplyTransaction(t2);
        account.ApplyTransaction(t3);

        // Add to transactions list
        _transactions.Add(t1);
        _transactions.Add(t2);
        _transactions.Add(t3);
    }
}

// Step 7: Main Entry Point
class Program
{
    static void Main()
    {
        FinanceApp app = new();
        app.Run();
    }
}
