using System.Diagnostics.CodeAnalysis;
using ApiDemo.Models.TokenAuthorizationModels;

namespace ApiDemo.DataBase.Interfaces;

public interface ITokenDataDB {
    public TokenData? GetTokenData(string accessToken);
    public void AddToken(TokenData token);
}