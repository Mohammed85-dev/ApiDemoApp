using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.Auth.Token;

namespace ApiDemo.DataBase.Classes;

public class TokenDataDB : ITokenDataDB {
    private LinkedList<TokenDataModel> tokens = new LinkedList<TokenDataModel>();

    public TokenDataModel getTokenData(string accessToken) {
        var node = tokens.First;
        while (node != null) {
            if (node.Value!.AccessToken == accessToken) {
                return node.Value;
            }
            node = node.Next;
        }
        return null!;
    }

    public void addToken(TokenDataModel token) {
        tokens.AddLast(token);
    }
}