using System.Diagnostics.CodeAnalysis;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.AccountModels;
using Cassandra.Data.Linq;
using Cassandra.Mapping;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class AccountDataDB : IAccountDataDB {
    private readonly Table<Account> _accounts;
    // private readonly IMapper _mapper;
    public AccountDataDB(ISession cassandraSession) {
        _accounts = new Table<Account>(cassandraSession);
        _accounts.CreateIfNotExistsAsync();
        // _mapper = mapper;
    }

    public void AddAccount(Account account) {
        _accounts.Insert(account).Execute();
    }

    public void UpdateAccount(Guid uuid, Account account) {
        var query = _accounts.Where(a => a.UUID == uuid);

        if (!string.IsNullOrEmpty(account.Email))
            query = query.Select(a => new Account { Email = account.Email });

        if (!string.IsNullOrEmpty(account.Password))
            query = query.Select(a => new Account { Password = account.Password });

        if (!string.IsNullOrEmpty(account.UserUsername))
            query = query.Select(a => new Account { UserUsername = account.UserUsername });

        if (account.HashedUserAccessTokens?.Any() == true)
            query = query.Select(a => new Account { HashedUserAccessTokens = account.HashedUserAccessTokens });

        if (account.HashedRefreshTokens?.Any() == true)
            query = query.Select(a => new Account { HashedRefreshTokens = account.HashedRefreshTokens });

        query.Update().Execute();
  
        // _accounts.Where(a => a.UUID == uuid).Select(a => account).Update().Execute();
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