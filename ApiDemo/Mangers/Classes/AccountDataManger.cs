using ApiDemo.Core.Tokens;
using ApiDemo.DataBase.Interfaces;
using ApiDemo.TypesData;
using ApiDemo.Models;
using ApiDemo.Models.Auth;


namespace ApiDemo.DataBase.Classes;

public class AccountDataManger(IAccountDataDB accountDB, IUsersDataDB usersDB, ITokenAutherizationManger iTokenAutherizationManger) : IAccountDataManger {
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
            TokensModelData = iTokenAutherizationManger.GenerateUserDataRWToken(accountData),
        };
    }

    public TokenRequestResponseDataModel LoginUser(LoginModel loginModel) {
        throw new NotImplementedException();
    }

    /*public TokenRequestResponseDataModel LoginUser(LoginModel loginModel) {
        UserData? userData = (loginModel.UsingUsername)
            ? (usersDB.tryGetUser(loginModel.Username, out UserData dataE))
                ? dataE
                : null
            : usersDB.tryGetUser(loginModel.Email, out UserData dataU)
                ? dataU
                : null;

        if (userData == null)
            return new TokenRequestResponseDataModel() {
                Succeeded = false,
                Message = "Failed to find user",
                TokensModelData = null,
            };

        if (loginModel.Password != userData.Password)
            return new TokenRequestResponseDataModel() {
                Succeeded = false,
                Message = "Incorrect password",
                TokensModelData = null,
            };

        return new TokenRequestResponseDataModel() {
            Succeeded = true,
            Message = "Login in successful. Generated new token",
            TokensModelData = generateTokenData(userData),
        };
    }*/
}