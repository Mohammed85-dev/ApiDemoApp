using ApiDemo.Models;
using ApiDemo.Models.Account;
using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Interfaces;

public interface IAccountDataDB {
    public void AddAccount(AccountDataModel accountDataModel);
    public AccountDataModel? GetAccountDataEmail(Guid userUUID);
    public AccountDataModel? GetAccountData(string accountUserUsername);
    public AccountDataModel? GetAccountDataEmail(string accountEmail);
    public bool TryGetAccountData(Guid userUUID, out AccountDataModel accountDataModel);
    public bool TryGetAccountData(string accountUserUsername, out AccountDataModel accountDataModel);
    public bool TryGetAccountDataEmail(string accountEmail, out AccountDataModel accountDataModel);
}