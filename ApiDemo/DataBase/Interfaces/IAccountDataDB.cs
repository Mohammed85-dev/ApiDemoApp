using ApiDemo.Models;
using ApiDemo.Models.AccountModels;
using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Interfaces;

public interface IAccountDataDB {
    public void AddAccount(Account account);
    public Account? GetAccountDataEmail(Guid userUUID);
    public Account? GetAccountData(string username);
    public Account? GetAccountDataEmail(string accountEmail);
    public bool TryGetAccountData(Guid userUUID, out Account account);
    public bool TryGetAccountData(string accountUserUsername, out Account account);
    public bool TryGetAccountDataEmail(string accountEmail, out Account account);
}