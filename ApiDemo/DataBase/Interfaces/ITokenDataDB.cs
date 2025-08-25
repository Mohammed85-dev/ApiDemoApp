using System.Diagnostics.CodeAnalysis;
using ApiDemo.Models.TokenAuthorizationModels;

namespace ApiDemo.DataBase.Interfaces;

public interface ITokenDataDB {
    public bool TryGetTokenData(string accessToken, [MaybeNullWhen(false)] out TokenData tokenData);
    public TokenData? GetTokenData(string accessToken);
    public TokenData[] GetTokenDataList(Guid uuid);
    public void AddToken(TokenData token);
    public void UpdateToken(string accessToken, TokenData token);
    public void UpdateToken(string[] accessToken, TokenData[] tokens);
}