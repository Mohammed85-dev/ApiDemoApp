using ApiDemo.Models;
using ApiDemo.Models.Auth;

namespace ApiDemo.DataBase.Interfaces;

public interface IAccountDataManger {
    public bool PasswordRest(PasswordRestModel passwordRest);
    public TokenRequestResponseDataModel SignUpUser(SignUpModel signUpModel);
    public TokenRequestResponseDataModel LoginUser(LoginModel loginModel);
    
    // public bool VerifyAccessToken(VerifyAccessTokenModel accessToken, out string response);
    // public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken, out string response);
    // public TokenRequestResponseDataModel getTokens(GetTokensModel getTokens);
}