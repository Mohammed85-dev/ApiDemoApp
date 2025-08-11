using ApiDemo.Models;
using ApiDemo.Models.Account;
using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Interfaces;

public interface IAccountDataDB {
    public void AddAccount(AccountDataModel accountDataModel);
    public AccountDataModel? GetAccountData(Guid userUUID);
    public AccountDataModel? GetAccountData(string accountUserUsername);
    public AccountDataModel? GetAccountData(Email accountEmail);
    public bool TryGetAccountData(Guid userUUID, out AccountDataModel accountDataModel);
    public bool TryGetAccountData(string accountUserUsername, out AccountDataModel accountDataModel);
    public bool TryGetAccountData(Email accountEmail, out AccountDataModel accountDataModel);
}