using System.Diagnostics.CodeAnalysis;
using ApiDemo.Models.AccountModels;
using ApiDemo.Models.TokenAuthorizationModels;

namespace ApiDemo.Mangers.Interfaces;

public interface ITokenAuthorizationManger {
    public TokenData GeneratePermissionLevelZeroToken(Account account);
    public bool IsAuthorized(string accessToken, string permission, out string response);
    public bool IsAuthorized(Guid uuid, string accessToken, string permission, out string response);
    public bool IsAuthorized(Guid uuid, string accessToken, PresetTokenPermissions requiredPermissions, out string response);
    public bool TryGetTokenData(string token, [MaybeNullWhen(false)] out TokenData tokenData, out string response);
    public bool GiveCustomAuthorizationLevelZero(Guid uuid, PresetTokenPermissions presetTokenPermission, string permission, out string response);
}
public enum PresetTokenPermissions {
    permissionsLevelZero,
}