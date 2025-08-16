using ApiDemo.DataBase.Interfaces;
using ApiDemo.Mangers.Interfaces;
using ApiDemo.Models.AccountModels;
using ApiDemo.Models.TokenAuthorizationModels;
using ApiDemo.Models.UserModels;

namespace ApiDemo.Mangers.Classes;

public class AccountManger(IAccountDataDB accountDB, IUsersDataDB usersDB, ITokenAuthorizationManger tokenAuthorizationManger) : IAccountManger {
    public bool PasswordRest(PasswordChange passwordChangeChange, out string response) {
        if (!accountDB.TryGetAccountData(passwordChangeChange.Uuid, out var account)) {
            response = "Invalid uuid";
            return false;
        }
        if (passwordChangeChange.OldPassword != account.Password) {
            response = "Incorrect old password";
            return false;
        }
        account.Password = passwordChangeChange.NewPassword;
        response = "Changed password";
        return true;
    }

    /*public bool PasswordRest(PasswordRestModel passwordRest, out string response) {

    }*/

    public TokenRequestResponse SignUpUser(SignUp signUpData) {
        if (usersDB.tryGetUser(signUpData.Username).success || accountDB.TryGetAccountDataEmail(signUpData.Email, out _))
            return new TokenRequestResponse {
                Succeeded = false,
                Message = "A user with that username/email already exists",
                TokensModelData = null,
            };

        Account account = new() {
            UUID = Guid.NewGuid(),
            UserUsername = signUpData.Username,
            Email = signUpData.Email,
            Password = signUpData.Password,
        };

        usersDB.AddUser(new User { Uuid = account.UUID, Username = account.UserUsername, });
        accountDB.AddAccount(account);

        return new TokenRequestResponse {
            Succeeded = true,
            Message = "Created new user",
            TokensModelData = tokenAuthorizationManger.GenerateUserDataRWToken(account),
        };
    }

    public TokenRequestResponse LoginUser(Login login) {
        Account? accountData =
            (login.UsingUsername)
                ? accountDB.GetAccountData(login.Username!)
                : accountDB.GetAccountDataEmail(login.Email!);

        if (accountData == null)
            return new TokenRequestResponse {
                Succeeded = false,
                Message = "Failed to find user",
                TokensModelData = null,
            };

        if (login.Password != accountData.Password)
            return new TokenRequestResponse {
                Succeeded = false,
                Message = "Incorrect password",
                TokensModelData = null,
            };

        return new TokenRequestResponse {
            Succeeded = true,
            Message = "Login in successful. Generated new token",
            TokensModelData = tokenAuthorizationManger.GenerateUserDataRWToken(accountData),
        };
    }
}