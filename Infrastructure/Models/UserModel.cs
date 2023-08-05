namespace Infrastructure.Models;

public class UserModel
{
    public string? UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public string? OldPassword { get; set; }
    public string? PhoneNumber { get; set; }
    public string Role { get; set; }
}
