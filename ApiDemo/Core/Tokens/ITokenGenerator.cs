namespace ApiDemo.Core.Tokens;

public interface ITokenGenerator {
    public string GenerateToken();
    public string HashToken(string token);
}