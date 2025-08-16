using System.Diagnostics.CodeAnalysis;
using ApiDemo.Models.AccountModels;
using ApiDemo.Models.TokenAuthorizationModels;

namespace ApiDemo.Mangers.Interfaces;

public interface ITokenAuthorizationManger {
    public TokenData GenerateUserDataRWToken(Account account);
    public bool IsAuthorized(Guid uuid, string accessToken, TokenPermissions requiredPermissions, out string response);
    public bool TryGetTokenData(string token, [MaybeNullWhen(false)] out TokenData tokenData, out string response);
}
public enum TokenPermissions {
    userDataRW
}