using ApiDemo.Core.Tokens;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.TypesData;
using Microsoft.AspNetCore.Mvc;
using ApiDemo.Models;
using ApiDemo.Models.Auth;

namespace ApiDemo.DataBase.Classes {
    public class UsersDataManger(ITokenGenerator _tokenGenerator) : IUsersDataManger, IAuthDataManger {
        private readonly LinkedList<UserData> _users = new();
        
        private bool tryGetUser(Guid uuid, out UserData userData) {
            var node = _users.First;
            while (node != null) {
                if (node.Value!.Uuid == uuid) {
                    userData = node.Value;
                    return true;
                }
                node = node.Next;
            }
            userData = null!;
            return false;
        }

        private bool tryGetUser(string username, out UserData userData) {
            var node = _users.First;
            while (node != null) {
                if (node.Value!.Username == username) {
                    userData = node.Value;
                    return true;
                }
                node = node.Next;
            }
            userData = null!;
            return false;
        }

        private bool tryGetUser(Email email, out UserData userData) {
            var node = _users.First;
            while (node != null) {
                if (node.Value!.Email == email) {
                    userData = node.Value;
                    return true;
                }
                node = node.Next;
            }
            userData = null!;
            return false;
        }

        public int GetCount() => _users.Count;

        public bool PasswordRest(PasswordRestModel passwordRest, out string response) {
            if (!tryGetUser(passwordRest.Uuid, out UserData? userData)) {
                response = "Invalid uuid";
                return false;
            }
            if (passwordRest.OldPassword != userData.Password) {
                response = "Incorrect old password";
                return false;
            }
            userData.Password = passwordRest.NewPassword;
            response = "Changed password";
            return true;
        }

        public bool Authorization(string accessToken, IAuthDataManger.Permission permission, out string response) {
            throw new NotImplementedException();
        }

        public bool Authorization(Guid uuid, string accessToken, IAuthDataManger.Permission permission, out string response) {
            
            if (!tryGetUser(uuid, out UserData userData)) {
                response = "Invalid uuid";
                return false;
            }

            response = "Valid AccessToken";
            return true;
        }

        public TokenRequestDataModel SignUpUser(SignUpModel signUpData) {
            UserData userData = new() {
                Uuid = Guid.NewGuid(),
                Username = signUpData.Username,
                Email = signUpData.Email,
                Password = signUpData.Password,
            };

            _users.AddLast(userData);

            return new TokenRequestDataModel() {
                Succeeded = true,
                Message = "Created new user",
                TokensModelData = generateTokenData(userData),
            };
        }

        public TokenRequestDataModel LoginUser(LoginModel loginModel) {
            UserData? userData = (loginModel.UsingUsername)
                ? (tryGetUser(loginModel.Username, out UserData dataE))
                    ? dataE
                    : null
                : tryGetUser((Email)loginModel.Email, out UserData dataU)
                    ? dataU
                    : null;

            if (userData == null)
                return new TokenRequestDataModel() {
                    Succeeded = false,
                    Message = "Failed to find user",
                    TokensModelData = null,
                };

            if (loginModel.Password != userData.Password)
                return new TokenRequestDataModel() {
                    Succeeded = false,
                    Message = "Incorrect password",
                    TokensModelData = null,
                };

            return new TokenRequestDataModel() {
                Succeeded = true,
                Message = "Login in successful. Generated new token",
                TokensModelData = generateTokenData(userData),
            };
        }

        public ActionResult<PublicUserDataModel> GetPublicUserData(Guid uuid) {
            if (!tryGetUser(uuid, out UserData userData)) {
                return new BadRequestResult();
            }
            return new PublicUserDataModel() {
                Username = userData.Username,
                Email = userData.Email,
            };
        }

        private bool verifyAccessToken(UserData userData, string token) => userData.HashedUserAccessTokens.Contains(_tokenGenerator.HashToken(token));

        public bool VerifyAccessToken(VerifyAccessTokenModel accessToken, out string response) {
            if (!tryGetUser(accessToken.Uuid, out UserData userData)) {
                response = "Failed to find user";
                return false;
            }
            response = "Valid AccessToken";
            return verifyAccessToken(userData, accessToken.Token);
        }

        private bool verifyRefreshToken(UserData userData, string refreshToken) {
            return userData.HashedRefreshTokens.Contains(_tokenGenerator.HashToken(refreshToken));
        }

        public bool VerifyRefreshToken(VerifyRefreshTokenModel refreshToken, out string response) {
            if (!tryGetUser(refreshToken.Uuid, out UserData userData)) {
                response = "Invalid uuid";
                return false;
            }
            response = "Valid RefreshToken";
            return verifyRefreshToken(userData, refreshToken.RefreshToken);
        }

        public TokenRequestDataModel getTokens(GetTokensModel getTokensData) {
            if (!tryGetUser(getTokensData.UUID, out var userData)) {
                return new TokenRequestDataModel() {
                    Succeeded = false,
                    Message = "Failed to find user",
                };
            }

            return (verifyAccessToken(userData, getTokensData.AccessToken) &&
                verifyRefreshToken(userData, getTokensData.RefreshToken))
                ? new TokenRequestDataModel() {
                    Succeeded = true,
                    Message = "Generated new Tokens",
                    TokensModelData = generateTokenData(userData),
                }
                : new TokenRequestDataModel {
                    Succeeded = false,
                    Message = "Invalid access token or refresh token for UUID",
                };
        }

        private TokensModelData generateTokenData(UserData userData) {
            TokensModelData tokensData = new() {
                AccessToken = _tokenGenerator.GenerateToken(),
                RefreshToken = _tokenGenerator.GenerateToken(),
                UUID = userData.Uuid,
            };
            userData.HashedUserAccessTokens.Add(_tokenGenerator.HashToken(tokensData.AccessToken));
            userData.HashedRefreshTokens.Add(_tokenGenerator.HashToken(tokensData.RefreshToken));
            return tokensData;
        }
    }
}