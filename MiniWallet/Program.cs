using MiniWallet.Services;
using MiniWallet.Dtos;

var walletService = new WalletService();

while (true)
{
    Console.WriteLine("\n=== MINI WALLET ===");
    Console.WriteLine("1. View Wallets");
    Console.WriteLine("2. Check Balance");
    Console.WriteLine("3. Deposit");
    Console.WriteLine("4. Withdraw");
    Console.WriteLine("5. Transfer");
    Console.WriteLine("0. Exit");
    Console.Write("Choose an option: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            foreach (var wallet in walletService.GetWallets())
            {
                Console.WriteLine(
                    $"{wallet.Id}. {wallet.OwnerName} - " +
                    $"Balance: {wallet.Balance:N2}");
            }
            break;

        case "2":
            {
                int walletId = ReadInt("Wallet ID: ");
                var wallet = walletService.GetWallet(walletId);

                Console.WriteLine(wallet is null
                    ? "Wallet not found."
                    : $"{wallet.OwnerName}'s balance: {wallet.Balance:N2}");

                break;
            }

        case "3":
            {
                int walletId = ReadInt("Wallet ID: ");
                decimal amount = ReadAmount("Deposit amount: ");

                ShowResult(walletService.Deposit(walletId, amount));
                break;
            }

        case "4":
            {
                int walletId = ReadInt("Wallet ID: ");
                decimal amount = ReadAmount("Withdraw amount: ");

                ShowResult(walletService.Withdraw(walletId, amount));
                break;
            }

        case "5":
            {
                int fromId = ReadInt("From wallet ID: ");
                int toId = ReadInt("To wallet ID: ");
                decimal amount = ReadAmount("Transfer amount: ");

                ShowResult(walletService.Transfer(fromId, toId, amount));
                break;
            }

        case "0":
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}

static int ReadInt(string message)
{
    while (true)
    {
        Console.Write(message);

        if (int.TryParse(Console.ReadLine(), out int value))
            return value;

        Console.WriteLine("Please enter a valid integer.");
    }
}

static decimal ReadAmount(string message)
{
    while (true)
    {
        Console.Write(message);

        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            return amount;

        Console.WriteLine("Please enter a valid amount.");
    }
}

static void ShowResult(TransactionDto result)
{
    Console.WriteLine(
        $"{(result.Success ? "SUCCESS" : "FAILED")}: {result.Message}");

    if (result.Success)
        Console.WriteLine($"Remaining balance: {result.Balance:N2}");
}