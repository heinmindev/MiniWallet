using MiniWallet.Dtos;

namespace MiniWallet.Services;

public class WalletService
{
    private readonly List<WalletDto> _wallets =
    [
        new WalletDto
        {
            Id = 1,
            OwnerName = "Alice",
            Balance = 1000m
        },
        new WalletDto
        {
            Id = 2,
            OwnerName = "Bob",
            Balance = 500m
        }
    ];

    public List<WalletDto> GetWallets()
    {
        return _wallets;
    }

    public WalletDto? GetWallet(int walletId)
    {
        return _wallets.FirstOrDefault(wallet => wallet.Id == walletId);
    }

    public TransactionDto Deposit(int walletId, decimal amount)
    {
        var wallet = GetWallet(walletId);

        if (wallet is null)
            return Fail("Wallet not found.");

        if (amount <= 0)
            return Fail("Amount must be greater than zero.");

        wallet.Balance += amount;

        return Success(
            $"Deposited {amount:N2} successfully.",
            wallet.Balance);
    }

    public TransactionDto Withdraw(int walletId, decimal amount)
    {
        var wallet = GetWallet(walletId);

        if (wallet is null)
            return Fail("Wallet not found.");

        if (amount <= 0)
            return Fail("Amount must be greater than zero.");

        if (wallet.Balance < amount)
            return Fail("Insufficient balance.");

        wallet.Balance -= amount;

        return Success(
            $"Withdrawn {amount:N2} successfully.",
            wallet.Balance);
    }

    public TransactionDto Transfer(
        int fromWalletId,
        int toWalletId,
        decimal amount)
    {
        if (fromWalletId == toWalletId)
            return Fail("Cannot transfer to the same wallet.");

        var sender = GetWallet(fromWalletId);
        var receiver = GetWallet(toWalletId);

        if (sender is null || receiver is null)
            return Fail("Source or destination wallet not found.");

        if (amount <= 0)
            return Fail("Amount must be greater than zero.");

        if (sender.Balance < amount)
            return Fail("Insufficient balance.");

        // Change balances only after all validations pass.
        sender.Balance -= amount;
        receiver.Balance += amount;

        return Success(
            $"Transferred {amount:N2} from {sender.OwnerName} " +
            $"to {receiver.OwnerName} successfully.",
            sender.Balance);
    }

    private static TransactionDto Success(
        string message,
        decimal balance)
    {
        return new TransactionDto
        {
            Success = true,
            Message = message,
            Balance = balance
        };
    }

    private static TransactionDto Fail(string message)
    {
        return new TransactionDto
        {
            Success = false,
            Message = message
        };
    }
}