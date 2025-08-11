using ApiDemo.Models;
using ApiDemo.Models.Auth;

namespace ApiDemo.DataBase.Interfaces;

public interface IAuthDataManger {
    public bool PasswordRest(PasswordRestModel passwordRest);
    public bool Authorization(string accessToken);
    public TokenRequestDataModel SignUpUser(SignUpModel signUpModel);
    public TokenRequestDataModel LoginUser(LoginModel loginModel);
    
    public bool VerifyAccessToken(VerifyAccessTokenModel accessToken, out string response);
    public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken, out string response);
    public TokenRequestDataModel getTokens(GetTokensModel getTokens);
}