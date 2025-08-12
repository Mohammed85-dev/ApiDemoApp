using ApiDemo.Models;
using ApiDemo.Models.Account;
using ApiDemo.Models.Auth;

namespace ApiDemo.DataBase.Interfaces;

public interface IAccountManger {
    public bool PasswordRest(PasswordChangeModel passwordChange, out string response);
    public TokenRequestResponseDataModel SignUpUser(SignUpModel signUpModel);
    public TokenRequestResponseDataModel LoginUser(LoginModel loginModel);
    
    // public bool VerifyAccessToken(VerifyAccessTokenModel accessToken, out string response);
    // public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken, out string response);
    // public TokenRequestResponseDataModel getTokens(GetTokensModel getTokens);
}