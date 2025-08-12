using System.Linq.Expressions;
using System.Reflection.Metadata;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models;
using ApiDemo.Models.Account;
using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Classes;

public class AccountDataDB : IAccountDataDB {
    private readonly LinkedList<AccountDataModel> _accounts = [];

    public void AddAccount(AccountDataModel accountDataModel) {
        _accounts.AddLast(accountDataModel);
    }

    public AccountDataModel? GetAccountData(Guid userUUID) {
        return find(data => data.UUID, userUUID);
    }

    public AccountDataModel? GetAccountData(string accountUserUsername) {
        return find(data => data.UserUsername, accountUserUsername);
    }

    public AccountDataModel? GetAccountData(Email accountEmail) {
        return find<Email>(data => data.Email, accountEmail);
    }

    public bool TryGetAccountData(Guid userUUID, out AccountDataModel accountDataModel) {
        return tryFind(data => data.UUID, userUUID, out accountDataModel);
    }

    public bool TryGetAccountData(string accountUserUsername, out AccountDataModel accountDataModel) {
        return tryFind(data => data.UserUsername, accountUserUsername, out accountDataModel);
    }

    public bool TryGetAccountData(Email accountEmail, out AccountDataModel accountDataModel) {
        return tryFind<Email>(data => data.Email, accountEmail, out accountDataModel);
    }

    private bool tryFind<TField>(Expression<Func<AccountDataModel, TField>> fieldSelector, TField value, out AccountDataModel accountDataModel) {
        var getter = fieldSelector.Compile();
        foreach (var item in _accounts) {
            if (EqualityComparer<TField>.Default.Equals(getter(item), value)) {
                accountDataModel = item;
                return true;
            }
        }
        accountDataModel = null!;
        return false;
    }

    private AccountDataModel? find<TField>(Expression<Func<AccountDataModel, TField>> fieldSelector, TField value) {
        var getter = fieldSelector.Compile();
        foreach (var item in _accounts) {
            if (EqualityComparer<TField>.Default.Equals(getter(item), value))
                return item;
        }
        return null;
    }
}