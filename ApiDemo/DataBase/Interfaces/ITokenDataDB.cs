using ApiDemo.Models.TokenAuthorizationModels;

namespace ApiDemo.DataBase.Interfaces;

public interface ITokenDataDB {
    public TokenData getTokenData(string accessToken);
    public void addToken(TokenData token);
}