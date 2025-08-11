using ApiDemo.Models;
using ApiDemo.Models.Auth;

namespace ApiDemo.DataBase.Interfaces;

public interface IAuthDataManger {
    public enum Permission {
        userAdmin
    }
    
    public bool PasswordRest(PasswordRestModel passwordRest, out string response);
    public bool Authorization(string accessToken, Permission permission, out string response);
    public TokenRequestDataModel SignUpUser(SignUpModel signUpModel);
    public TokenRequestDataModel LoginUser(LoginModel loginModel);
    
    public bool VerifyAccessToken(VerifyAccessTokenModel accessToken, out string response);
    public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken, out string response);
    public TokenRequestDataModel getTokens(GetTokensModel getTokens);
}