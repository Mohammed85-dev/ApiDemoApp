using ApiDemo.Models;

namespace ApiDemo.DataBase.Classes.Interfaces;

public interface IUsersDataManger {
    public int GetCount();
    public TokenRequestDataModel SignUpUser(SignUpModel signUpModel);
    public PublicUserDataModel GetPublicUserData(Guid uuid);
    public bool VerifyAccessToken(VerifyAccessTokenModel accessToken);
    public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken);
    public TokenRequestDataModel getTokens(GetTokensModel getTokens);
}