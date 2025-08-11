using System.Text.Json.Serialization;
using ApiDemo.Models.Auth.Token;
using ApiDemo.TypesData;

namespace ApiDemo.Models;

public class TokenRequestResponseDataModel {
    public required bool Succeeded { get; set; }
    public required string Message { get; set; }

    public TokenDataModel? TokensModelData { get; set; }
}
