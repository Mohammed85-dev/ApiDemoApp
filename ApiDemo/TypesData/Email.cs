using System.ComponentModel.DataAnnotations;

namespace ApiDemo.TypesData;

public struct Email([EmailAddress] string email) {
    private string _email = email;
    public static implicit operator string(Email email) =>  email._email;
    public static implicit operator Email(string email) => new Email(email);
}