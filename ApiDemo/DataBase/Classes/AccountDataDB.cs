using System.Linq.Expressions;
using System.Reflection.Metadata;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Classes;

public class AccountDataDB : IAccountDataDB {
    private LinkedList<AccountData> accounts = new LinkedList<AccountData>();

    public void AddAccount(AccountData accountData) {
        accounts.AddLast(accountData);
    }

    public AccountData? GetAccountData(Guid userUUID) {
        return find(data => data.AccountUUID, userUUID);
    }

    public AccountData? GetAccountData(string accountUserUsername) {
        return find(data => data.AccountUserUsername, accountUserUsername);
    }

    public AccountData? GetAccountData(Email accountEmail) {
        return find<string>(data => data.AccountEmail, accountEmail);
    }

    public bool TryGetAccountData(Guid userUUID, out AccountData accountData) {
        return tryFind(data => data.AccountUUID, userUUID, out accountData);
    }

    public bool TryGetAccountData(string accountUserUsername, out AccountData accountData) {
        return tryFind(data => data.AccountUserUsername, accountUserUsername, out accountData);
    }

    public bool TryGetAccountData(Email accountEmail, out AccountData accountData) {
        return tryFind<string>(data => data.AccountEmail, accountEmail, out accountData);
    }

    private bool tryFind<TField>(Expression<Func<AccountData, TField>> fieldSelector, TField value, out AccountData accountData) {
        var getter = fieldSelector.Compile();
        foreach (var item in accounts) {
            if (EqualityComparer<TField>.Default.Equals(getter(item), value)) {
                accountData = item;
                return true;
            }
        }
        accountData = null;
        return false;
    }

    private AccountData? find<TField>(Expression<Func<AccountData, TField>> fieldSelector, TField value) {
        var getter = fieldSelector.Compile();
        foreach (var item in accounts) {
            if (EqualityComparer<TField>.Default.Equals(getter(item), value))
                return item;
        }
        return null;
    }
}