using ApiDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.DataBase.Classes.Interfaces;

public interface IUsersDataManger {
    public int GetCount();
    public bool PasswordRest(PasswordRestModel passwordRest);
    public TokenRequestDataModel SignUpUser(SignUpModel signUpModel);
    public TokenRequestDataModel LoginUser(LoginModel loginModel);
    public PublicUserDataModel GetPublicUserData(Guid uuid);
    public bool VerifyAccessToken(VerifyAccessTokenModel accessToken);
    public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken);
    public TokenRequestDataModel getTokens(GetTokensModel getTokens);
}