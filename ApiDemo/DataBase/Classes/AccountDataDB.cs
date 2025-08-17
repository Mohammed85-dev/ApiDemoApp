using System.Diagnostics.CodeAnalysis;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.AccountModels;
using Cassandra.Data.Linq;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class AccountDataDB : IAccountDataDB {
    private readonly Table<Account> _accounts;

    public AccountDataDB(ISession cassandraSession) {
        _accounts = new Table<Account>(cassandraSession);
        _accounts.CreateIfNotExistsAsync();
    }

    public void AddAccount(Account account) {
        _accounts.Insert(account).Execute();
    }

    public Account? GetAccountDataEmail(Guid userUUID) {
        return _accounts.FirstOrDefault(u => u.UUID == userUUID).Execute();
    }

    public Account? GetAccountData(string username) {
        return _accounts.FirstOrDefault(u => u.UserUsername == username).Execute();
    }

    public Account? GetAccountDataEmail(string accountEmail) {
        return _accounts.FirstOrDefault(u => u.Email == accountEmail).Execute();
    }

    public bool TryGetAccountData(Guid userUUID, [MaybeNullWhen(false)] out Account account) {
        account = GetAccountDataEmail(userUUID);
        return account != null;
    }


    public bool TryGetAccountData(string accountUserUsername, [MaybeNullWhen(false)] out Account account) {
        account = GetAccountDataEmail(accountUserUsername);
        return account != null;
    }

    public bool TryGetAccountDataEmail(string accountEmail, [MaybeNullWhen(false)] out Account account) {
        account = GetAccountDataEmail(accountEmail);
        return account != null;
    }
}