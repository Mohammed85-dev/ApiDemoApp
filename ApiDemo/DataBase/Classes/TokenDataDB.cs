using System.Diagnostics.CodeAnalysis;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Models.TokenAuthorizationModels;
using Cassandra.Data.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ISession = Cassandra.ISession;

namespace ApiDemo.DataBase.Classes;

public class TokenDataDB : ITokenDataDB {
    private readonly Table<TokenData> _tokens;

    public TokenDataDB(ISession cassandraSession) {
        _tokens = new Table<TokenData>(cassandraSession);
        _tokens.CreateIfNotExists();
    }

    public bool TryGetTokenData(string accessToken, [MaybeNullWhen(false)] out TokenData tokenData) {
        tokenData = _tokens.FirstOrDefault(t => t.AccessToken == accessToken).Execute();
        return tokenData != null;
    }

    public TokenData? GetTokenData(string accessToken) {
        return _tokens.FirstOrDefault(t => t.AccessToken == accessToken).Execute();
    }

    public TokenData[] GetTokenDataList(Guid uuid) {
        return _tokens.Where(t => t.OwnerUUID == uuid).Execute().ToArray();
    }

    public void AddToken(TokenData token) {
        _tokens.Insert(token).Execute();
    }

    public void UpdateToken(string accessToken, TokenData token) {
        _tokens.Where(t => t.AccessToken == accessToken).Select(t => token).Update().Execute();
    }

    public void UpdateToken(string[] accessToken, TokenData[] tokens) {
        if (accessToken.Length != tokens.Length)
            throw new ArgumentException("accessToken and tokens arrays must have the same length");

        var batch = _tokens.GetSession().CreateBatch();

        for (int i = 0; i < tokens.Length; i++) {
            if (i == tokens.Length)
                throw new UnsupportedContentTypeException("WTF this is impossible unlesss some dark magic is happening");
            var a = accessToken[i];
            var t = tokens[i];

            // Base query filtered by primary key
            var query = _tokens.Where(x => x.AccessToken == a);

            // Conditionally update fields (only if not default)
            if (t.OwnerUUID != Guid.Empty)
                query = query.Select(x => new TokenData { OwnerUUID = t.OwnerUUID });

            if (!string.IsNullOrEmpty(t.RefreshToken))
                query = query.Select(x => new TokenData { RefreshToken = t.RefreshToken });

            if (t.ExpiresAT != DateTime.MaxValue)
                query = query.Select(x => new TokenData { ExpiresAT = t.ExpiresAT });

            if (t.customPermissions.Count > 0)
                query = query.Select(x => new TokenData { customPermissions = t.customPermissions });

            if (t.PresetPermissions.Count > 0)
                query = query.Select(x => new TokenData { PresetPermissions = t.PresetPermissions });

            // Only append if we actually set something to update
            if (query != null)
                batch.Append(query.Update());
        }

        batch.Execute();
    }
}