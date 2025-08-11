namespace ApiDemo.Models.Account;

public class AccountDataModel {
    public required Guid UUID { get; set; }
    public required string UserUsername { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public readonly List<string> HashedUserAccessTokens = [];
    public readonly List<string> HashedRefreshTokens = [];
}