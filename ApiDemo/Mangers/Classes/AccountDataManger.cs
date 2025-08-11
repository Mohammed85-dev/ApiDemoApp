using ApiDemo.Core.Tokens;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.TypesData;
using ApiDemo.Models;
using ApiDemo.Models.Auth;


namespace ApiDemo.DataBase.Classes;

public class AccountDataManger(IAccountDataDB accountDB, IUsersDataDB usersDB, ITokenAutherizationManger tokenAutherizationManger) : IAccountDataManger {
    public bool PasswordRest(PasswordRestModel passwordRest) {
        throw new NotImplementedException();
    }

    /*public bool PasswordRest(PasswordRestModel passwordRest, out string response) {
        if (!_usersDB.tryGetUser(passwordRest.Uuid, out UserData? userData)) {
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
    }*/
    
    public TokenRequestResponseDataModel SignUpUser(SignUpModel signUpData) {
        AccountData accountData = new() {
            AccountUUID = Guid.NewGuid(),
            AccountUserUsername = signUpData.Username,
            AccountEmail = (Email)signUpData.Email,
            AccountPassword = signUpData.Password,
        };

        usersDB.AddUser(new UserData() { Uuid = accountData.AccountUUID, Username = accountData.AccountUserUsername });
        accountDB.AddAccount(accountData);
        
        return new TokenRequestResponseDataModel() {
            Succeeded = true,
            Message = "Created new user",
            TokensModelData = tokenAutherizationManger.GenerateUserDataRWToken(accountData),
        };
    }

    public TokenRequestResponseDataModel LoginUser(LoginModel loginModel) {
        AccountData? accountData = (loginModel.UsingUsername)
            ? (accountDB.TryGetAccountData(loginModel.Username, out AccountData dataE))
                ? dataE
                : null
            : accountDB.TryGetAccountData((Email)loginModel.Email, out AccountData dataU)
                ? dataU
                : null;

        if (accountData == null)
            return new TokenRequestResponseDataModel() {
                Succeeded = false,
                Message = "Failed to find user",
                TokensModelData = null,
            };

        if (loginModel.Password != accountData.AccountPassword)
            return new TokenRequestResponseDataModel() {
                Succeeded = false,
                Message = "Incorrect password",
                TokensModelData = null,
            };

        return new TokenRequestResponseDataModel() {
            Succeeded = true,
            Message = "Login in successful. Generated new token",
            TokensModelData = tokenAutherizationManger.GenerateUserDataRWToken(accountData),
        };
    }
}