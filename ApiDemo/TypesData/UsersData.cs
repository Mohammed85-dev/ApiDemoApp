using ApiDemo.Models;

namespace ApiDemo.TypesData {
    public class UserData {
        public required Guid Uuid {get; set;}
        public required string Username { get; set; }
        public required Email Email { get; set; }
        public required string Password { get; set; }
        public readonly List<string> HashedUserAccessTokens = [];
        public readonly List<string> HashedRefreshTokens = [];
    }
}