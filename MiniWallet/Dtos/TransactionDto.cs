namespace MiniWallet.Dtos;

public class TransactionDto
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}