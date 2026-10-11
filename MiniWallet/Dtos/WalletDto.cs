namespace MiniWallet.Dtos;

public class WalletDto
{
    public int Id { get; set; }
    public string OwnerName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}