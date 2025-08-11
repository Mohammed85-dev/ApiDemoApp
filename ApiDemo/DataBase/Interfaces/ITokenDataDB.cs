using ApiDemo.Models.Auth.Token;

namespace ApiDemo.DataBase.Interfaces;

public interface ITokenDataDB {
    public TokenDataModel getTokenData(string accessToken);
    public void addToken(TokenDataModel token);
}