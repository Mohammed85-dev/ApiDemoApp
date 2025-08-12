using ApiDemo.DataBase.Interfaces;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models;
using ApiDemo.Models.Account;
using ApiDemo.Models.User;
using ApiDemo.TypesData;

namespace ApiDemo.Mangers.Classes;

public class AccountManger(IAccountDataDB accountDB, IUsersDataDB usersDB, ITokenAuthorizationManger tokenAuthorizationManger) : IAccountManger {
    public bool PasswordRest(PasswordRestModel passwordRest, out string response) {
        if (!accountDB.TryGetAccountData(passwordRest.Uuid, out AccountDataModel account)) {
            response = "Invalid uuid";
            return false;
        }
        if (passwordRest.OldPassword != account.Password) {
            response = "Incorrect old password";
            return false;
        }
        account.Password = passwordRest.NewPassword;
        response = "Changed password";
        return true;
    }

    /*public bool PasswordRest(PasswordRestModel passwordRest, out string response) {
        
    }*/
    
    public TokenRequestResponseDataModel SignUpUser(SignUpModel signUpData) {
        if (usersDB.tryGetUser(signUpData.Username, out var _) || accountDB.TryGetAccountData((Email)signUpData.Email, out var _))
            return new TokenRequestResponseDataModel() {
                Succeeded = false,
                Message = "A user with that username/email already exists",
                TokensModelData = null,
            };
        
        AccountDataModel accountDataModel = new() {
            UUID = Guid.NewGuid(),
            UserUsername = signUpData.Username,
            Email = (Email)signUpData.Email,
            Password = signUpData.Password,
        };

        usersDB.AddUser(new UserDataModel() { Uuid = accountDataModel.UUID, Username = accountDataModel.UserUsername });
        accountDB.AddAccount(accountDataModel);
        
        return new TokenRequestResponseDataModel() {
            Succeeded = true,
            Message = "Created new user",
            TokensModelData = tokenAuthorizationManger.GenerateUserDataRWToken(accountDataModel),
        };
    }

    public TokenRequestResponseDataModel LoginUser(LoginModel loginModel) {
        AccountDataModel? accountData = (loginModel.UsingUsername)
            ? (accountDB.TryGetAccountData(loginModel.Username, out AccountDataModel dataU))
                ? dataU
                : null
            : accountDB.TryGetAccountData((Email)loginModel.Email, out AccountDataModel dataE)
                ? dataE
                : null;

        if (accountData == null)
            return new TokenRequestResponseDataModel() {
                Succeeded = false,
                Message = "Failed to find user",
                TokensModelData = null,
            };

        if (loginModel.Password != accountData.Password)
            return new TokenRequestResponseDataModel() {
                Succeeded = false,
                Message = "Incorrect password",
                TokensModelData = null,
            };

        return new TokenRequestResponseDataModel() {
            Succeeded = true,
            Message = "Login in successful. Generated new token",
            TokensModelData = tokenAuthorizationManger.GenerateUserDataRWToken(accountData),
        };
    }
}