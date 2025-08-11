using ApiDemo.Models;
using ApiDemo.Models.Auth.Token;
using ApiDemo.TypesData;

namespace ApiDemo.DataBase.Interfaces;

public interface ITokenAutherizationManger {
    
    public enum Permissions {
        userDataRW
    }

    public TokenDataModel GenerateUserDataRWToken(AccountData accountData);
    public bool IsAuthorized(Guid uuid, string accessToken, ITokenAutherizationManger.Permissions permission, out string response);

}