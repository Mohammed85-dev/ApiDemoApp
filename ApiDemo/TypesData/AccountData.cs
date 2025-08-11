namespace ApiDemo.TypesData;

public class AccountData {
    public required Guid AccountUUID { get; set; }
    public required string AccountUserUsername { get; set; }
    public required string AccountEmail { get; set; }
    public required string AccountPassword { get; set; }
    public readonly List<string> HashedUserAccessTokens = [];
    public readonly List<string> HashedRefreshTokens = [];
}