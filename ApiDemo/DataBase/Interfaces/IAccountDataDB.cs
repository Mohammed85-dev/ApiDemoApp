using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Interfaces;

public interface IAccountDataDB {
    public void AddAccount(AccountData accountData);
    public AccountData? GetAccountData(Guid userUUID);
    public AccountData? GetAccountData(string accountUserUsername);
    public AccountData? GetAccountData(Email accountEmail);
    public bool TryGetAccountData(Guid userUUID, out AccountData accountData);
    public bool TryGetAccountData(string accountUserUsername, out AccountData accountData);
    public bool TryGetAccountData(Email accountEmail, out AccountData accountData);
}