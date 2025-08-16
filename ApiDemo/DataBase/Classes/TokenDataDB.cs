using System.Diagnostics.CodeAnalysis;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.TokenAuthorizationModels;
using Cassandra.Data.Linq;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class TokenDataDB : ITokenDataDB {
    private readonly Table<TokenData> _tokens;

    public TokenDataDB(ISession cassandraSession) {
        _tokens = new Table<TokenData>(cassandraSession); 
        _tokens.CreateIfNotExists();
    }

    public TokenData? GetTokenData(string accessToken) => _tokens.FirstOrDefault(t => t.AccessToken == accessToken).Execute();

    public void AddToken(TokenData token) {
        _tokens.Insert(token).Execute();
    }
}