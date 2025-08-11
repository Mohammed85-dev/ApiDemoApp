using ApiDemo.Models.Account;
using ApiDemo.Models.Auth.Token;

namespace ApiDemo.Mangers.Interfaces;

public interface ITokenAuthorizationManger {
    public TokenDataModel GenerateUserDataRWToken(AccountDataModel accountDataModel);
    public bool IsAuthorized(Guid uuid, string accessToken, TokenPermissions requiredPermissions, out string response);
}
public enum TokenPermissions {
    userDataRW
}