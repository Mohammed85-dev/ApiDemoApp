using System.Diagnostics.CodeAnalysis;
using ApiDemo.Core.Tokens;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models.AccountModels;
using ApiDemo.Models.TokenAuthorizationModels;

namespace ApiDemo.Mangers.Classes;

public class TokenAuthorizationManger(ITokenDataDB tokenDB, IAccountDataDB accountDB, ITokenGenerator tokenGenerator) : ITokenAuthorizationManger {
    public TokenData GenerateUserDataRWToken(Account account) {
        TokenData tokenData = new() {
            AccessToken = tokenGenerator.GenerateToken(),
            RefreshToken = tokenGenerator.GenerateToken(),
            OwnerUUID = account.UUID,
            PermissionEnums = [TokenPermissions.userDataRW],
        };
        account.HashedUserAccessTokens.Add(tokenGenerator.HashToken(tokenData.AccessToken));
        account.HashedRefreshTokens.Add(tokenGenerator.HashToken(tokenData.RefreshToken));
        TokenData storedTokenData = new() {
            AccessToken = tokenGenerator.HashToken(tokenData.AccessToken),
            RefreshToken = tokenGenerator.HashToken(tokenData.RefreshToken),
            OwnerUUID = account.UUID,
            PermissionEnums = [TokenPermissions.userDataRW],
        };
        tokenDB.AddToken(storedTokenData);
        return tokenData;
    }

    public bool IsAuthorized(Guid uuid, string accessToken, TokenPermissions requiredPermissions, out string response) {
        if (!accountDB.TryGetAccountData(uuid, out var accountData)) {
            response = "Invalid uuid";
            return false;
        }
        if (!accountData.HashedUserAccessTokens.Contains(tokenGenerator.HashToken(accessToken))) {
            response = "Invalid access token";
        }
        var tokenData = tokenDB.GetTokenData(tokenGenerator.HashToken(accessToken));
        if (tokenData == null) {
            response = "Invalid token";
            return false;
        }
        else if (!tokenData.PermissionEnums.Contains(requiredPermissions)) {
            response = "Invalid permission";
            return false;
        }

        response = "Authorized";
        return true;
    }

    public bool TryGetTokenData(string token, [MaybeNullWhen(false)] out TokenData tokenData, out string response) {
        tokenData = tokenDB.GetTokenData(token); 
        if (tokenData == null) {
            response = "Token not found";
            return false;
        }
        response = "Token successfully retrieved";
        return true;
    }

    // public bool Authorization(string accessToken, IAccountDataManger.Permission permission, out string response) {
    //     throw new NotImplementedException();
    // }
    //
    // public bool Authorization(Guid uuid, string accessToken, IAccountDataManger.Permission permission, out string response) {
    //     if (!_usersDB.tryGetUser(uuid, out UserData userData)) {
    //         response = "Invalid uuid";
    //         return false;
    //     }
    //
    //     response = "Valid AccessToken";
    //     return true;
    // }

    // private bool verifyAccessToken(UserData userData, string token) => userData.HashedUserAccessTokens.Contains(_tokenGenerator.HashToken(token));

    // private bool verifyRefreshToken(UserData userData, string refreshToken) =>
    // userData.HashedRefreshTokens.Contains(_tokenGenerator.HashToken(refreshToken));

    // public bool VerifyAccessToken(VerifyAccessTokenModel accessToken, out string response) {
    //     if (!_usersDB.tryGetUser(accessToken.Uuid, out UserData userData)) {
    //         response = "Failed to find user";
    //         return false;
    //     }
    //     response = "Valid AccessToken";
    //     return verifyAccessToken(userData, accessToken.Token);
    // }

    // public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken, out string response) {
    //     if (!_usersDB.tryGetUser(refreshToken.Uuid, out UserData userData)) {
    //         response = "Invalid uuid";
    //         return false;
    //     }
    //     response = "Valid RefreshToken";
    //     return verifyRefreshToken(userData, refreshToken.RefreshToken);
    // }

    // public TokenRequestResponseDataModel getTokens(GetTokensModel getTokensData) {
    //     if (!_usersDB.tryGetUser(getTokensData.UUID, out var userData)) {
    //         return new TokenRequestResponseDataModel() {
    //             Succeeded = false,
    //             Message = "Failed to find user",
    //         };
    //     }
    //
    //     return (verifyAccessToken(userData, getTokensData.AccessToken) &&
    //         verifyRefreshToken(userData, getTokensData.RefreshToken))
    //         ? new TokenRequestResponseDataModel() {
    //             Succeeded = true,
    //             Message = "Generated new Tokens",
    //             TokensModelData = generateTokenData(userData),
    //         }
    //         : new TokenRequestResponseDataModel {
    //             Succeeded = false,
    //             Message = "Invalid access token or refresh token for UUID",
    //         };
}