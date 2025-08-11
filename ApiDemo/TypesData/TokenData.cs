using ApiDemo.DataBase.Interfaces;

namespace ApiDemo.TypesData;

public class TokenData {
    public required string AccessToken;
    public required IAuthDataManger.Permission[] Permissions;
}