# Mini Wallet Console App

A simple C# console application demonstrating basic wallet operations using **DTOs** and a **service**.

This is a small learning project. It stores wallet data in memory, so balances reset when the application restarts. It is not intended for real financial transactions.

## Features

- View all wallets
- Check a wallet's balance
- Deposit money
- Withdraw money
- Transfer money between wallets
- Validate wallet IDs and transaction amounts
- Prevent withdrawals and transfers that exceed the available balance

## Requirements

- [.NET SDK](https://dotnet.microsoft.com/download) (use a version supported by your installed SDK)

Check your installation:

```powershell
dotnet --version
```

## Project structure

```text
MiniWallet/
├── Dtos/
│   ├── WalletDto.cs
│   └── TransactionDto.cs
├── Services/
│   └── WalletService.cs
└── Program.cs
```

### Responsibilities

| File | Responsibility |
|---|---|
| `Program.cs` | Displays the menu, reads input, and displays operation results |
| `Dtos/WalletDto.cs` | Holds a wallet's ID, owner name, and balance |
| `Dtos/TransactionDto.cs` | Returns operation success, message, and balance |
| `Services/WalletService.cs` | Implements wallet lookup, deposit, withdrawal, and transfer rules |

## Initial wallets

The sample service starts with two wallets:

| Wallet ID | Owner | Initial balance |
|---:|---|---:|
| 1 | Alice | 1,000.00 |
| 2 | Bob | 500.00 |

Balances are held in a `List<WalletDto>` in memory. Any changes are lost when the process exits.

## Menu

```text
=== MINI WALLET ===
1. View Wallets
2. Check Balance
3. Deposit
4. Withdraw
5. Transfer
0. Exit
Choose an option:
```

### Example: transfer money

```text
Choose an option: 5
From wallet ID: 1
To wallet ID: 2
Transfer amount: 200

SUCCESS: Transferred 200.00 from Alice to Bob successfully.
Remaining balance: 800.00
```

After the transfer, Alice's balance is `800.00` and Bob's balance is `700.00`.

## Business rules

- A wallet must exist before an operation can succeed.
- Deposit, withdrawal, and transfer amounts must be greater than zero.
- A wallet cannot transfer money to itself.
- A withdrawal or transfer fails when the sender's balance is insufficient.
- A transfer validates both wallets and the amount before updating either balance.
- Money is represented by C# `decimal`, which is generally preferable to `float` or `double` for monetary values.

## Design overview

```text
Program.cs
    |
    | calls
    v
WalletService
    |
    | reads/updates
    v
In-memory List<WalletDto>

WalletService returns TransactionDto
    |
    v
Program.cs displays the result
```
