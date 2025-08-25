using System.Diagnostics.CodeAnalysis;
using ApiDemo.Models.AccountModels;

namespace ApiDemo.DataBase.Interfaces;

public interface IAccountDataDB {
    public void AddAccount(Account account);
    public void UpdateAccount(Guid uuid, Account account);
    public Account? GetAccountDataEmail(Guid userUUID);
    public Account? GetAccountData(string username);
    public Account? GetAccountDataEmail(string accountEmail);
    public bool TryGetAccountData(Guid userUUID, [MaybeNullWhen(false)] out Account account);
    public bool TryGetAccountData(string accountUserUsername, [MaybeNullWhen(false)] out Account account);
    public bool TryGetAccountDataEmail(string accountEmail, [MaybeNullWhen(false)] out Account account);
}