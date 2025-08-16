using ApiDemo.Models.AccountModels;
using ApiDemo.Models.TokenAuthorizationModels;

namespace ApiDemo.Mangers.Interfaces;

public interface IAccountManger {
    public bool PasswordRest(PasswordChange passwordChangeChange, out string response);
    public TokenRequestResponse SignUpUser(SignUp signUp);
    public TokenRequestResponse LoginUser(Login login);

    // public bool VerifyAccessToken(VerifyAccessTokenModel accessToken, out string response);
    // public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken, out string response);
    // public TokenRequestResponseDataModel getTokens(GetTokensModel getTokens);
}