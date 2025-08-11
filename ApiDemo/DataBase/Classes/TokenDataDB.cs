using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.Auth.Token;

namespace ApiDemo.DataBase.Classes;

public class TokenDataDB : ITokenDataDB {
    private readonly LinkedList<TokenDataModel> _tokens = [];

    public TokenDataModel getTokenData(string accessToken) {
        var node = _tokens.First;
        while (node != null) {
            if (node.Value!.AccessToken == accessToken) {
                return node.Value;
            }
            node = node.Next;
        }
        return null!;
    }

    public void addToken(TokenDataModel token) {
        _tokens.AddLast(token);
    }
}