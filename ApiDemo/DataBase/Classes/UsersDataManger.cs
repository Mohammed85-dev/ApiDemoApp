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

            return new TokenRequestDataModel() {
                Succeeded = true,
                Message = "Created new user",
                TokensModelData = generateTokenData(userData.Uuid),
            };
        }

        public TokenRequestDataModel LoginUser(LoginModel loginModel) {
            Guid uuid = Guid.Empty;
            foreach (var keyValuePair in _users) {
                if (loginModel.UsingUsername) {
                    if (keyValuePair.Value.Username == loginModel.Username) {
                        uuid = keyValuePair.Key;
                    }
                }
                else {
                    if (keyValuePair.Value.Email == loginModel.Email) {
                        uuid = keyValuePair.Key;
                    }
                }
            }

            if (uuid == Guid.Empty)
                return new TokenRequestDataModel() {
                    Succeeded = false,
                    Message = "Failed to find user",
                    TokensModelData = null,
                };

            if (loginModel.Password != _users[uuid].Password)
                return new TokenRequestDataModel() {
                    Succeeded = false,
                    Message = "Incorrect password",
                    TokensModelData = null,
                };

            return new TokenRequestDataModel() {
                Succeeded = true,
                Message = "Login in successful. Generated new token",
                TokensModelData = generateTokenData(uuid),
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