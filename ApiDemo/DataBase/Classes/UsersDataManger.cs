using ApiDemo.Core.Tokens;
using ApiDemo.DataBase.Classes.Interfaces;
using ApiDemo.TypesData;
using ApiDemo.Models;

namespace ApiDemo.DataBase.Classes {
    public class UsersDataManger(ITokenGenerator _tokenGenerator) : IUsersDataManger {
        private readonly Dictionary<Guid, UserData> _users = new();

        public int GetCount() => _users.Count;

        public TokenRequestDataModel SignUpUser(SignUpModel signUpData) {
            UserData userData = new UserData() {
                Uuid = Guid.NewGuid(),
                Username = signUpData.Username,
                Email = signUpData.Email,
                Password = signUpData.Password,
            };

            _users.Add(userData.Uuid, userData);

            TokensModelData tokensData = generateTokenData(userData.Uuid);

            return new TokenRequestDataModel() {
                Succeeded = true,
                Message = "Created new user",
                TokensModelData = tokensData,
            };
        }

        public PublicUserDataModel GetPublicUserData(Guid uuid) {
            return new PublicUserDataModel() {
                Username = _users[uuid].Username,
                Email = _users[uuid].Email,
            };
        }

        private bool verifyAccessToken(Guid uuid, string token) => _users[uuid].HashedUserAccessTokens.Contains(_tokenGenerator.HashToken(token));

        public bool VerifyAccessToken(VerifyAccessTokenModel accessToken) => verifyAccessToken(accessToken.Uuid, accessToken.Token);

        private bool verifyRefreshToken(Guid uuid, string refreshToken) =>
            _users[uuid].HashedRefreshTokens.Contains(_tokenGenerator.HashToken(refreshToken));

        public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken) => verifyRefreshToken(refreshToken.Uuid, refreshToken.RefreshToken);

        public TokenRequestDataModel getTokens(GetTokensModel getTokensData) =>
            (verifyAccessToken(getTokensData.UUID, getTokensData.AccessToken) &&
                verifyRefreshToken(getTokensData.UUID, getTokensData.RefreshToken))
                ? new TokenRequestDataModel() {
                    Succeeded = true,
                    Message = "Generated new Tokens",
                    TokensModelData = generateTokenData(getTokensData.UUID),
                }
                : new TokenRequestDataModel {
                    Succeeded = false,
                    Message = "Invalid access token or refresh token for UUID",
                };
        
        private TokensModelData generateTokenData(Guid uuid) {
            TokensModelData tokensData = new() {
                AccessToken = _tokenGenerator.GenerateToken(),
                RefreshToken = _tokenGenerator.GenerateToken(),
                UUID = uuid,
            };
            _users[uuid].HashedUserAccessTokens.Add(_tokenGenerator.HashToken(tokensData.AccessToken));
            _users[uuid].HashedRefreshTokens.Add(_tokenGenerator.HashToken(tokensData.RefreshToken));
            return tokensData;
        }
    }
}